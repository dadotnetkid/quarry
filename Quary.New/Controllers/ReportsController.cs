using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using DevExpress.XtraReports.UI;
using Helpers;
using Helpers.Reports;
using Helpers.Services;
using Models;
using Models.Repository;
using Models.ViewModels;

namespace Quary.New.Controllers
{
    [RoutePrefix("reports")]
    public class ReportsController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private readonly BIRReportService _birReportService;
        private readonly PTOReportService _ptoReportService;
        private string BIRReportPath => "~/Views/Reports/BIRReport/{0}.cshtml";
        private string PTOReportPath => "/Views/Reports/PTOReport/{0}.cshtml";
        public ReportsController()
        {
            _birReportService = BIRReportService.Create();
            _ptoReportService = PTOReportService.Create();
        }


        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        #region bir-report
        [Route("bir-report")]
        public ActionResult BIRReport()
        {
            return View($"~/views/reports/birreport/{nameof(BIRReport)}.cshtml");
        }

        public ActionResult BIRReportPartial([ModelBinder(typeof(DevExpressEditorsBinder))] BIRParamsViewModel vmParams)
        {
            try
            {

                var report = new List<ProductionReport>();
                var permittees = unitOfWork.PermiteesRepo.Fetch();
                if (vmParams.Permittee > 0)
                    permittees = permittees.Where(x => x.Id == vmParams.Permittee);
                if (vmParams.PermitteeType > 0)
                    permittees = permittees.Where(x => x.PermiteeTypeId == vmParams.PermitteeType);

                foreach (var permittee in permittees)
                {

                    var deliveries = unitOfWork.ProductionsRepo.Fetch(m => m.PermiteeId == permittee.Id);
                    if (vmParams.QuarriesId > 0)
                    {
                        deliveries = deliveries.Where(m => m.QuarriesId == vmParams.QuarriesId);
                    }
                    foreach (var i in deliveries.GroupBy(x => x.VehicleId))
                    {
                        int? vehicleId = i.Key;
                        var quantity = deliveries.Where(m => m.VehicleId == vehicleId).Sum(m => m.Quantity);
                        report.Add(new ProductionReport()
                        {
                            Permittees = permittee,
                            DrNo = deliveries.Count(m => m.VehicleId == vehicleId),
                            Vehicles = unitOfWork.VehiclesRepo.Find(m => m.Id == vehicleId),
                            Quantity = quantity,
                            OrdinaryEarth = deliveries.Where(m => m.VehicleId == vehicleId && m.SagId == 1).Sum(m => m.Quantity),
                            Mixed = deliveries.Where(m => m.VehicleId == vehicleId && m.SagId == 4).Sum(m => m.Quantity),
                            ScreenFine = deliveries.Where(m => m.VehicleId == vehicleId && m.SagId == 5).Sum(m => m.Quantity),
                            ScreenCoarse = deliveries.Where(m => m.VehicleId == vehicleId && m.SagId == 8).Sum(m => m.Quantity),
                            Boulders = deliveries.Where(m => m.VehicleId == vehicleId && m.SagId == 6).Sum(m => m.Quantity),
                            Crushed = deliveries.Where(m => m.VehicleId == vehicleId && m.SagId == 7).Sum(m => m.Quantity),



                        });

                    }

                }

                BIRQuarriesDeliveryReport quarriesDeliveryReport = new BIRQuarriesDeliveryReport()
                {
                    DataSource = report
                };
                return PartialView($"/views/reports/birreport/{nameof(BIRReportPartial)}.cshtml", quarriesDeliveryReport);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public PartialViewResult CboQuarryBIRReport()
        {

            return PartialView(string.Format(BIRReportPath, nameof(CboQuarryBIRReport)), _birReportService.GetQuarries());
        }

        public PartialViewResult CboPermitteeTypeBIRReport(int? quarryId)
        {
            var model = _birReportService.GetAvailablePermitteeType(quarryId);
            return PartialView(string.Format(BIRReportPath, nameof(CboPermitteeTypeBIRReport)), model);
        }

        public PartialViewResult CboPermitteeBIRReport(int? permitteeTypeId)
        {
            var model = _birReportService.GetAvailablePermittee(permitteeTypeId);

            return PartialView(string.Format(BIRReportPath, nameof(CboPermitteeBIRReport)), model);
        }

        public PartialViewResult CboAvailableYearBIRReport(int? permitteeId)
        {
            return PartialView(string.Format(BIRReportPath, nameof(CboAvailableYearBIRReport)), _birReportService.GetAvailableYear(permitteeId));
        }


        public PartialViewResult CboAvailableMonthByYear(int? year, int? quarryid, int? permitteeId)
        {
            return PartialView(string.Format(BIRReportPath, nameof(CboAvailableMonthByYear)), _birReportService.GetAvailableMonths(year, permitteeId, quarryid));
        }

        #endregion

        #region pto-report

        [Route("pto-report")]
        public ActionResult PTOReport([ModelBinder(typeof(DevExpressEditorsBinder))] ProductionLedgerReport item)
        {
            return View(string.Format(PTOReportPath, nameof(PTOReport)));
        }
        public ActionResult PTOReportPartial([ModelBinder(typeof(DevExpressEditorsBinder))] ProductionLedgerReport item,
            [ModelBinder(typeof(DevExpressEditorsBinder))] PTOReportParams reportParams)
        {
            if (item.isSubmitted != true)
            {

                return base.PartialView(string.Format(PTOReportPath, nameof(PTOReportPartial)), new Helpers.PTOReport());
            }

            if (reportParams != null)
            {
                if (reportParams.Year > 0 && item.Month > 0)
                {
                    var date = new DateTime(reportParams.Year, item.Month, 1);
                    item.DateFrom = date;
                    item.DateTo = date.AddMonths(1).AddDays(-1);
                }
                else
                {
                    var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    item.DateFrom = date;
                    item.DateTo = date.AddMonths(1).AddDays(-1);
                }
           
            }

            var _permittee = unitOfWork.PermiteesRepo.Fetch();
            if (reportParams.PermitteeId > 0)
                _permittee = _permittee.Where(x => x.Id == reportParams.PermitteeId);
            if (reportParams.PermitteeTypeId > 0)
                _permittee = _permittee.Where(x => x.PermiteeTypeId == reportParams.PermitteeTypeId);



            List<SummaryProductionReport> reports = new List<SummaryProductionReport>();
            List<Permitees> permittees = new List<Permitees>();
            var sag = unitOfWork.SagsRepo.Fetch();

            foreach (var permittee in _permittee)
            {
                var productionLedgerReports = new List<ProductionLedgerReport>();
                foreach (var i in permittee.Quarries)
                {
                    var r = new ProductionLedgerReport()
                    {

                        OrdinaryEarth = unitOfWork.ProductionsRepo.Fetch(m =>
                                                m.PermiteeId == permittee.Id &&
                                                (m.ProductionDate >= item.DateFrom && m.ProductionDate <= item.DateTo) &&
                                                m.Quarries.Id == i.Id && m.SagId == 1)
                           .Sum(m => m.Quantity) ?? 0,
                        OrdinaryEarthAmount = unitOfWork.ProductionsRepo.Fetch(m =>
                                                      m.PermiteeId == permittee.Id &&
                                                      (m.ProductionDate >= item.DateFrom && m.ProductionDate <= item.DateTo) &&
                                                      m.Quarries.Id == i.Id && m.SagId == 1)
                           .Sum(m => m.Quantity * m.Sags.UnitCost) ?? 0,
                        Mixed = unitOfWork.ProductionsRepo.Fetch(m =>
                                        m.PermiteeId == permittee.Id &&
                                        (m.ProductionDate >= item.DateFrom && m.ProductionDate <= item.DateTo) &&
                                        m.Quarries.Id == i.Id && m.SagId == 4)
                           .Sum(m => m.Quantity) ?? 0,
                        MixedAmount = unitOfWork.ProductionsRepo.Fetch(m =>
                                              m.PermiteeId == permittee.Id &&
                                              (m.ProductionDate >= item.DateFrom && m.ProductionDate <= item.DateTo) &&
                                              m.Quarries.Id == i.Id && m.SagId == 4)
                           .Sum(m => m.Quantity * m.Sags.UnitCost) ?? 0,
                        Fined = unitOfWork.ProductionsRepo.Fetch(m =>
                                        m.PermiteeId == permittee.Id &&
                                        (m.ProductionDate >= item.DateFrom && m.ProductionDate <= item.DateTo) &&
                                        m.Quarries.Id == i.Id && m.SagId == 5)
                           .Sum(m => m.Quantity) ?? 0,
                        FinedAmount = unitOfWork.ProductionsRepo.Fetch(m =>
                                              m.PermiteeId == permittee.Id &&
                                              (m.ProductionDate >= item.DateFrom && m.ProductionDate <= item.DateTo) &&
                                              m.Quarries.Id == i.Id && m.SagId == 5)
                           .Sum(m => m.Quantity * m.Sags.UnitCost) ?? 0,
                        Coarse = unitOfWork.ProductionsRepo.Fetch(m =>
                                         m.PermiteeId == permittee.Id &&
                                         (m.ProductionDate >= item.DateFrom && m.ProductionDate <= item.DateTo) &&
                                         m.Quarries.Id == i.Id && m.SagId == 8)
                           .Sum(m => m.Quantity) ?? 0,
                        CoarseAmount = unitOfWork.ProductionsRepo.Fetch(m =>
                                               m.PermiteeId == permittee.Id &&
                                               (m.ProductionDate >= item.DateFrom && m.ProductionDate <= item.DateTo) &&
                                               m.Quarries.Id == i.Id && m.SagId == 8).Sum(m => m.Quantity * m.Sags.UnitCost) ?? 0,
                        Boulders = unitOfWork.ProductionsRepo.Fetch(m =>
                                           m.PermiteeId == permittee.Id &&
                                           (m.ProductionDate >= item.DateFrom && m.ProductionDate <= item.DateTo) &&
                                           m.Quarries.Id == i.Id && m.SagId == 6)
                           .Sum(m => m.Quantity) ?? 0,
                        BouldersAmount = unitOfWork.ProductionsRepo.Fetch(m =>
                                                 m.PermiteeId == permittee.Id &&
                                                 (m.ProductionDate >= item.DateFrom && m.ProductionDate <= item.DateTo) &&
                                                 m.Quarries.Id == i.Id && m.SagId == 6)
                           .Sum(m => m.Quantity) ?? 0,
                        Crushed = unitOfWork.ProductionsRepo.Fetch(m =>
                                          m.PermiteeId == permittee.Id &&
                                          (m.ProductionDate >= item.DateFrom && m.ProductionDate <= item.DateTo) &&
                                          m.Quarries.Id == i.Id && m.SagId == 7)
                           .Sum(m => m.Quantity) ?? 0,
                        CrushedAmount = unitOfWork.ProductionsRepo.Fetch(m =>
                                m.PermiteeId == permittee.Id &&
                                (m.ProductionDate >= item.DateFrom && m.ProductionDate <= item.DateTo) &&
                                m.Quarries.Id == i.Id && m.SagId == 7)
                           .Sum(m => m.Quantity * m.Sags.UnitCost) ?? 0,


                        Permitees = permittee,
                        Quarries = i,
                        ReportDate = DateTime.Now.ToLongDateString()

                    };


                    productionLedgerReports.Add(r);



                }

                permittee.ProductionLedgerReports = productionLedgerReports;
                permittees.Add(permittee);
            }

            reports.Add(new SummaryProductionReport()
            {
                Permitees = permittees
            });

            Helpers.PTOReport summary = new Helpers.PTOReport()
            {
                DataSource = reports
            };
            return PartialView($"/views/reports/birreport/{nameof(BIRReportPartial)}.cshtml", summary);
        }


        public PartialViewResult CboPermitteeTypePTOReport(int? quarryId)
        {
            var model = _birReportService.GetAvailablePermitteeType(quarryId);
            return PartialView(string.Format(PTOReportPath, nameof(CboPermitteeTypePTOReport)), model);
        }

        public PartialViewResult CboPermitteePTOReport(int? permitteeTypeId)
        {
            var model = _ptoReportService.GetPermittees(permitteeTypeId);

            return PartialView(string.Format(PTOReportPath, nameof(CboPermitteePTOReport)), model);
        }

        public PartialViewResult CboAvailableYearPTOReport(int? permitteeId)
        {
            return PartialView(string.Format(PTOReportPath, nameof(CboAvailableYearPTOReport)), _ptoReportService.GetAvailableYear(permitteeId));
        }


        public PartialViewResult CboAvailableMonthByYearPTOReport(int? year, int? permitteeId)
        {
            return PartialView(string.Format(PTOReportPath, nameof(CboAvailableMonthByYearPTOReport)), _ptoReportService.GetAvailableMonth(permitteeId, year));
        }




        #endregion



        [OnUserAuthorization(ActionName = "Permittee List")]
        [Route("permittee-list")]
        public ActionResult PermitteeListReport()
        {
            return View();
        }

        public ActionResult PermitteeListReportPartial([ModelBinder(typeof(DevExpressEditorsBinder))] int? permitteeTypeId, [ModelBinder(typeof(DevExpressEditorsBinder))] int? year, [ModelBinder(typeof(DevExpressEditorsBinder))] int? month)
        {
            year = year ?? DateTime.Now.Year;
            var dateFrom = Convert.ToDateTime("01/01/" + year);
            var dateTo = Convert.ToDateTime("12/31/" + year);
            if (month != null)
            {
                dateFrom = Convert.ToDateTime($"{month}/01/" + year);
                dateTo = dateFrom.AddMonths(1).AddDays(-1);
            }
            PermitteeListReports permitteeListReports = new PermitteeListReports()
            {
                DataSource = permitteeTypeId == null ? unitOfWork.PermiteesRepo.Get() : unitOfWork.PermiteesRepo.Get(m => m.PermiteeTypeId == permitteeTypeId && m.Transactions.Any(x => (x.FilingDate >= dateFrom && x.FilingDate <= dateTo)))
            };
            return PartialView(permitteeListReports);
        }

        public ActionResult ProgramOfWorkReport()
        {
            return View();
        }
        #region Vehicle Master List
        [OnUserAuthorization(ActionName = "Vehicle Master List")]
        [Route("vehicle-master-list")]
        public ActionResult VehicleMasterList()
        {
            return View();
        }
        public ActionResult VehicleMasterListPartial([ModelBinder(typeof(DevExpressEditorsBinder))] int? permitteeId)
        {
            var model = new Permitees();
            XtraReport xtraReport = new XtraReport();
            if (permitteeId != null)
            {
                model = unitOfWork.PermiteesRepo.Find(m => m.Id == permitteeId, includeProperties: "Vehicles");
                xtraReport = new VehicleMasterListReport()
                {
                    DataSource = new List<Permitees>() { model }
                };
            }






            return PartialView(xtraReport);
        }
        #endregion


        #region Summary of Productions

        [Route("detail-productions")]
        public ActionResult DetailProductions()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DetailProductionsPartial([ModelBinder(typeof(DevExpressEditorsBinder))] string permitteeType, [ModelBinder(typeof(DevExpressEditorsBinder))] int? permitteeId, [ModelBinder(typeof(DevExpressEditorsBinder))] int? year, [ModelBinder(typeof(DevExpressEditorsBinder))] int? month)
        {
            var permittee = unitOfWork.PermiteesRepo.Fetch(includeProperties: "Productions");
            if (permitteeId != 0)
                permittee = permittee.Where(x => x.Id == permitteeId);
            var datefrom = new DateTime(year.ToInt(), month.ToInt(), 1);
            var dateTo = new DateTime(year.ToInt(), month.ToInt(), DateTime.DaysInMonth(year.ToInt(), month.ToInt())).AddHours(23).AddMinutes(59).AddSeconds(59);

            var res = permittee.Where(x => x.Productions.Any(m => (m.DateCreated >= datefrom && m.DateCreated <= dateTo)));
            var type = permitteeType.Split(',')?.Select(x => new { Id = x.ToInt() }).Select(x => x.Id).ToList();
            res = res.Where(x => type.Contains(x.PermiteeTypeId ?? 0));
            var permittees = res.ToList();
            foreach (var i in permittees)
            {
                i.Productions = unitOfWork.ProductionsRepo.Get(m => (m.DateCreated >= datefrom && m.DateCreated <= dateTo) && m.Transactions.PermiteeId == i.Id).ToList();
#if (DEBUG)
                Debug.Write($"ptl {i.Productions.Count(x => x.Vehicles.VehicleName.Contains("ptl"))}");
#endif
            }

            Debug.Write($"OE:{res.Sum(x => x.Productions.Where(m => m.SagId == 1).Sum(m => m.Quantity))}");
            Debug.Write($"Mixed:{res.Sum(x => x.Productions.Where(m => m.SagId == 4).Sum(m => m.Quantity))}");
            Debug.Write($"Fined:{res.Sum(x => x.Productions.Where(m => m.SagId == 5).Sum(m => m.Quantity))}");
            Debug.Write($"boulder:{res.Sum(x => x.Productions.Where(m => m.SagId == 6).Sum(m => m.Quantity))}");
            Debug.Write($"crushed:{res.Sum(x => x.Productions.Where(m => m.SagId == 7).Sum(m => m.Quantity))}");
            Debug.Write($"S1:{res.Sum(x => x.Productions.Where(m => m.SagId == 8).Sum(m => m.Quantity))}");
            var rpt = new rptDetailProductionPivot()
            {
                DataSource = permittees
            };
            rpt.lblHeader.Text = $"Summary of Productions(as of { CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month.ToInt())} {year})";
            return PartialView(rpt);
        }

        public ActionResult cboMonthPartial(int year, int permitteeId)
        {
            var months = unitOfWork.ProductionsRepo.Fetch();
            if (permitteeId != 0)
                months = months.Where(x => x.PermiteeId == permitteeId);
            var res = months.Where(x => SqlFunctions.DatePart("year", x.DateCreated) == year).ToList().Select(x => new
            {
                month = x.DateCreated?.Month,
            }).GroupBy(x => x.month).Select(x => new
            {
                month = x.Key,
                monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(x.Key ?? 1)
            }).ToList();
            return PartialView(res);
        }
        public ActionResult cboYearPartial(int permitteeId)
        {
            var months = unitOfWork.ProductionsRepo.Fetch();
            if (permitteeId != 0)
                months = months.Where(x => x.PermiteeId == permitteeId);
            var res = months.ToList().Select(x => new { year = x.DateCreated?.Year }).ToList().GroupBy(x => x.year).Select(x => new { year = x.Key }).ToList();
            return PartialView(res);
        }
        public ActionResult cboPermitteePartial(string permitteeTypeId)
        {
            var list = new List<Permitees>() { new Permitees() { FirstName = "All", Id = 0 } };
            if (!string.IsNullOrEmpty(permitteeTypeId))
                foreach (var i in permitteeTypeId.Split(','))
                {
                    var id = i.ToInt();
                    var model = unitOfWork.PermiteesRepo.Get(x => x.PermiteeTypeId == id);
                    list.AddRange(model);
                }

            return PartialView(list);
        }
        #endregion

    }
}