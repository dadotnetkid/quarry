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
using Models;
using Models.Repository;
using Models.ViewModels;

namespace Quary.New.Controllers
{
    [RoutePrefix("reports")]
    public class ReportsController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }
        [Route("production-report")]
        public ActionResult ProductionReport()
        {
            return View();
        }

        public ActionResult ProductionReportPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? quarriesId)
        {

            var report = new List<ProductionReport>();
            foreach (var permittee in unitOfWork.PermiteesRepo.Fetch())
            {

                var deliveries = unitOfWork.ProductionsRepo.Fetch(m => m.QuarriesId == quarriesId && m.PermiteeId == permittee.Id);
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
            return PartialView(quarriesDeliveryReport);
        }

        public ActionResult ProductionLedger()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult ProductionLedgerGridViewPartial([ModelBinder(typeof(DevExpressEditorsBinder))]ProductionLedgerReport item)
        {

            if (item.isSubmitted != true)
                return PartialView("_ProductionLedgerGridViewPartial", Session["ProductionLedgerReport"]);


            var _permittee = unitOfWork.PermiteesRepo.Fetch();
            if (item.PermitteeId > 0)
                _permittee = _permittee.Where(m => m.Id == item.PermitteeId);
            if (item.PermitteeTypeId != null)
            {
                //  var permitteeTypes = item.PermitteeTypeId.Select(x => new UnitOfWork().PermiteeTypesRepo.Find(m => m.PermiteeTypeName == x)?.Id).ToList();
                _permittee = _permittee.Where(m => item.PermitteeTypeId.Contains(m.PermiteeTypeId));
            }


            ViewBag.PermitteeId = item.PermitteeId;





            var sag = unitOfWork.SagsRepo.Fetch();
            var __permittee = _permittee.ToList();
            foreach (var permittee in __permittee)
            {
                List<ProductionLedgerReport> reports = new List<ProductionLedgerReport>();
                foreach (var i in permittee.Quarries)
                {
                    /* var res = unitOfWork.ProductionsRepo
                         .Fetch(m => m.PermiteeId == permittee.Id && m.QuarriesId == i.Id )
                         .Select(m => SqlFunctions.DatePart("month", m.ProductionDate)).ToList();


                     Debug.Write($"Month {res}");
                     Debug.Write("sum"+unitOfWork.ProductionsRepo
                         .Fetch(m => m.PermiteeId == permittee.Id && m.QuarriesId == i.Id && m.SagId == 4)
                         .Where(m => SqlFunctions.DatePart("year", m.ProductionDate) == item.Year &&
                                     SqlFunctions.DatePart("month", m.ProductionDate) == item.Month)
                         .Sum(m => m.Sags.UnitCost * m.Quantity));*/
                    var r = new ProductionLedgerReport()
                    {
                        OrdinaryEarth = unitOfWork.TransactionSagsRepo.Fetch(m =>
                                m.Transactions.PermiteeId == permittee.Id &&
                                m.Transactions.Permitees.Quarries.Any(x => x.Id == i.Id) && m.SagId == 1)

                            .Sum(m => m.Quantity),

                        OrdinaryEarthAmount = unitOfWork.TransactionSagsRepo.Fetch(m =>
                                 m.Transactions.PermiteeId == permittee.Id &&
                                 m.Transactions.Permitees.Quarries.Any(x => x.Id == i.Id) && m.SagId == 1)
                            .Sum(m => m.UnitCost * m.Quantity),

                        Mixed = unitOfWork.TransactionSagsRepo.Fetch(m =>
                                m.Transactions.PermiteeId == permittee.Id &&
                                m.Transactions.Permitees.Quarries.Any(x => x.Id == i.Id) && m.SagId == 4)
                            .Sum(m => m.Quantity),

                        MixedAmount = unitOfWork.TransactionSagsRepo.Fetch(m =>
                                 m.Transactions.PermiteeId == permittee.Id &&
                                 m.Transactions.Permitees.Quarries.Any(x => x.Id == i.Id) && m.SagId == 4)
                            .Sum(m => m.UnitCost * m.Quantity),

                        Fined = unitOfWork.TransactionSagsRepo.Fetch(m =>
                                m.Transactions.PermiteeId == permittee.Id &&
                                m.Transactions.Permitees.Quarries.Any(x => x.Id == i.Id) && m.SagId == 5)
                            .Sum(m => m.Quantity),

                        FinedAmount = unitOfWork.TransactionSagsRepo.Fetch(m =>
                                m.Transactions.PermiteeId == permittee.Id &&
                                m.Transactions.Permitees.Quarries.Any(x => x.Id == i.Id) && m.SagId == 5)
                            .Sum(m => m.UnitCost * m.Quantity),

                        Coarse = unitOfWork.TransactionSagsRepo.Fetch(m =>
                                m.Transactions.PermiteeId == permittee.Id &&
                                m.Transactions.Permitees.Quarries.Any(x => x.Id == i.Id) && m.SagId == 8)
                            .Sum(m => m.Quantity),
                        CoarseAmount = unitOfWork.TransactionSagsRepo.Fetch(m =>
                                m.Transactions.PermiteeId == permittee.Id &&
                                m.Transactions.Permitees.Quarries.Any(x => x.Id == i.Id) && m.SagId == 8)
                            .Sum(m => m.UnitCost * m.UnitCost),

                        Boulders = unitOfWork.TransactionSagsRepo.Fetch(m =>
                                m.Transactions.PermiteeId == permittee.Id &&
                                m.Transactions.Permitees.Quarries.Any(x => x.Id == i.Id) && m.SagId == 6)
                            .Sum(m => m.Quantity),
                        BouldersAmount = unitOfWork.TransactionSagsRepo.Fetch(m =>
                                m.Transactions.PermiteeId == permittee.Id &&
                                m.Transactions.Permitees.Quarries.Any(x => x.Id == i.Id) && m.SagId == 6)
                            .Sum(m => m.Quantity * m.Quantity),

                        Crushed = unitOfWork.TransactionSagsRepo.Fetch(m =>
                                m.Transactions.PermiteeId == permittee.Id &&
                                m.Transactions.Permitees.Quarries.Any(x => x.Id == i.Id) && m.SagId == 7)
                            .Sum(m => m.Quantity),
                        CrushedAmount = unitOfWork.TransactionSagsRepo.Fetch(m =>
                                m.Transactions.PermiteeId == permittee.Id &&
                                m.Transactions.Permitees.Quarries.Any(x => x.Id == i.Id) && m.SagId == 7)
                            .Sum(m => m.UnitCost * m.Quantity),

                        ProductionOrdinaryEarth = unitOfWork.ProductionsRepo
                            .Fetch(m => m.PermiteeId == permittee.Id && m.QuarriesId == i.Id && m.SagId == 1)
                            .Where(m => SqlFunctions.DatePart("year", m.DateCreated) == item.Year && SqlFunctions.DatePart("month", m.DateCreated) == item.Month)
                            .Sum(m => m.Quantity),

                        ProductionOrdinaryEarthAmount = unitOfWork.ProductionsRepo
                            .Fetch(m => m.PermiteeId == permittee.Id && m.QuarriesId == i.Id && m.SagId == 1)
                            .Where(m => SqlFunctions.DatePart("year", m.DateCreated) == item.Year && SqlFunctions.DatePart("month", m.DateCreated) == item.Month)
                            .Sum(m => m.Sags.UnitCost * m.Quantity),


                        ProductionMixed = unitOfWork.ProductionsRepo
                            .Fetch(m => m.PermiteeId == permittee.Id && m.QuarriesId == i.Id && m.SagId == 4)
                            .Where(m => SqlFunctions.DatePart("year", m.DateCreated) == item.Year && SqlFunctions.DatePart("month", m.DateCreated) == item.Month)
                            .Sum(m => m.Quantity),

                        ProductionMixedAmount = unitOfWork.ProductionsRepo
                            .Fetch(m => m.PermiteeId == permittee.Id && m.QuarriesId == i.Id && m.SagId == 4)
                            .Where(m => SqlFunctions.DatePart("year", m.DateCreated) == item.Year && SqlFunctions.DatePart("month", m.DateCreated) == item.Month)
                            .Sum(m => m.Sags.UnitCost * m.Quantity),


                        ProductionFined = unitOfWork.ProductionsRepo
                            .Fetch(m => m.PermiteeId == permittee.Id && m.QuarriesId == i.Id && m.SagId == 5)
                            .Sum(m => m.Quantity),

                        ProductionFinedAmount = unitOfWork.ProductionsRepo
                            .Fetch(m => m.PermiteeId == permittee.Id && m.QuarriesId == i.Id && m.SagId == 5)
                            .Where(m => SqlFunctions.DatePart("year", m.DateCreated) == item.Year && SqlFunctions.DatePart("month", m.DateCreated) == item.Month)
                            .Sum(m => m.Sags.UnitCost * m.Quantity),


                        ProductionCoarse = unitOfWork.ProductionsRepo
                            .Fetch(m => m.PermiteeId == permittee.Id && m.QuarriesId == i.Id && m.SagId == 8)
                            .Sum(m => m.Quantity),

                        ProductionCoarseAmount = unitOfWork.ProductionsRepo
                            .Fetch(m => m.PermiteeId == permittee.Id && m.QuarriesId == i.Id && m.SagId == 8)
                            .Where(m => SqlFunctions.DatePart("year", m.DateCreated) == item.Year && SqlFunctions.DatePart("month", m.DateCreated) == item.Month)
                            .Sum(m => m.Sags.UnitCost * m.Quantity),

                        ProductionBoulders = unitOfWork.ProductionsRepo
                            .Fetch(m => m.PermiteeId == permittee.Id && m.QuarriesId == i.Id && m.SagId == 6)
                            .Where(m => SqlFunctions.DatePart("year", m.DateCreated) == item.Year && SqlFunctions.DatePart("month", m.DateCreated) == item.Month)
                            .Sum(m => m.Quantity),

                        ProductionBouldersAmount = unitOfWork.ProductionsRepo
                            .Fetch(m => m.PermiteeId == permittee.Id && m.QuarriesId == i.Id && m.SagId == 6)
                            .Where(m => SqlFunctions.DatePart("year", m.DateCreated) == item.Year && SqlFunctions.DatePart("month", m.DateCreated) == item.Month)
                            .Sum(m => m.Sags.UnitCost * m.Quantity),

                        ProductionCrushed = unitOfWork.ProductionsRepo
                            .Fetch(m => m.PermiteeId == permittee.Id && m.QuarriesId == i.Id && m.SagId == 7)
                            .Where(m => SqlFunctions.DatePart("year", m.DateCreated) == item.Year && SqlFunctions.DatePart("month", m.DateCreated) == item.Month)
                            .Sum(m => m.Quantity),


                        ProductionCrushedAmount = unitOfWork.ProductionsRepo
                            .Fetch(m => m.PermiteeId == permittee.Id && m.QuarriesId == i.Id && m.SagId == 7)
                            .Where(m => SqlFunctions.DatePart("year", m.DateCreated) == item.Year && SqlFunctions.DatePart("month", m.DateCreated) == item.Month)
                            .Sum(m => m.Sags.UnitCost * m.Quantity),

                        Permitees = permittee,
                        Quarries = i,
                        ReportDate = DateTime.Now.ToLongDateString()

                    };


                    reports.Add(r);
                }

                var OrdinaryEarth = reports.Sum(m => m.ProductionOrdinaryEarth).ToInt();
                var Mixed = reports.Sum(m => m.ProductionMixed).ToInt();
                var Fined = reports.Sum(m => m.ProductionFined).ToInt();
                var Coarse = reports.Sum(m => m.ProductionCoarse).ToInt();
                var Boulders = reports.Sum(m => m.ProductionBoulders).ToInt();
                var Crushed = reports.Sum(m => m.ProductionCrushed).ToInt();

                reports.ForEach(x =>
                {
                    x.OrdinaryEarth = x.OrdinaryEarth.ToInt() - OrdinaryEarth;
                    x.Mixed = x.Mixed.ToInt() - Mixed;
                    x.Fined = x.Fined.ToInt() - Fined;
                    x.Coarse = x.Coarse.ToInt() - Coarse;
                    x.Boulders = x.Boulders.ToInt() - Boulders;
                    x.Crushed = x.Crushed.ToInt() - Crushed;
                });
                permittee.ProductionLedgerReports = reports;
            }


            //    Session["ProductionLedgerReport"] = reports;

            SummaryLedgerReportByRange summaryLedgerReport = new SummaryLedgerReportByRange() { DataSource = new List<SummaryProductionReport>() { new SummaryProductionReport() { Permitees = __permittee, Month = item.Month, Year = item.Year } } };
            return PartialView("_ProductionLedgerGridViewPartial", summaryLedgerReport);
        }
        [Route("report/summary-production-report-per-Permittee")]
        public ActionResult SummaryProductionReportPerPermittee([ModelBinder(typeof(DevExpressEditorsBinder))]ProductionLedgerReport item)
        {
            return View();
        }
        public ActionResult SummaryProductionReportPerPermitteePartial([ModelBinder(typeof(DevExpressEditorsBinder))]ProductionLedgerReport item)
        {
            if (item.isSubmitted != true)
            {

                return PartialView("SummaryProductionReportPerPermitteePartial", new SummaryProductionReports());
            }


            var _permittee = unitOfWork.PermiteesRepo.Fetch();





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
                                                (m.ReceiptDate >= item.DateFrom && m.ReceiptDate <= item.DateTo) &&
                                                m.Quarries.Id == i.Id && m.SagId == 1)
                           .Sum(m => m.Quantity) ?? 0,
                        OrdinaryEarthAmount = unitOfWork.ProductionsRepo.Fetch(m =>
                                                      m.PermiteeId == permittee.Id &&
                                                      (m.ReceiptDate >= item.DateFrom && m.ReceiptDate <= item.DateTo) &&
                                                      m.Quarries.Id == i.Id && m.SagId == 1)
                           .Sum(m => m.Quantity * m.Sags.UnitCost) ?? 0,
                        Mixed = unitOfWork.ProductionsRepo.Fetch(m =>
                                        m.PermiteeId == permittee.Id &&
                                        (m.ReceiptDate >= item.DateFrom && m.ReceiptDate <= item.DateTo) &&
                                        m.Quarries.Id == i.Id && m.SagId == 4)
                           .Sum(m => m.Quantity) ?? 0,
                        MixedAmount = unitOfWork.ProductionsRepo.Fetch(m =>
                                              m.PermiteeId == permittee.Id &&
                                              (m.ReceiptDate >= item.DateFrom && m.ReceiptDate <= item.DateTo) &&
                                              m.Quarries.Id == i.Id && m.SagId == 4)
                           .Sum(m => m.Quantity * m.Sags.UnitCost) ?? 0,
                        Fined = unitOfWork.ProductionsRepo.Fetch(m =>
                                        m.PermiteeId == permittee.Id &&
                                        (m.ReceiptDate >= item.DateFrom && m.ReceiptDate <= item.DateTo) &&
                                        m.Quarries.Id == i.Id && m.SagId == 5)
                           .Sum(m => m.Quantity) ?? 0,
                        FinedAmount = unitOfWork.ProductionsRepo.Fetch(m =>
                                              m.PermiteeId == permittee.Id &&
                                              (m.ReceiptDate >= item.DateFrom && m.ReceiptDate <= item.DateTo) &&
                                              m.Quarries.Id == i.Id && m.SagId == 5)
                           .Sum(m => m.Quantity * m.Sags.UnitCost) ?? 0,
                        Coarse = unitOfWork.ProductionsRepo.Fetch(m =>
                                         m.PermiteeId == permittee.Id &&
                                         (m.ReceiptDate >= item.DateFrom && m.ReceiptDate <= item.DateTo) &&
                                         m.Quarries.Id == i.Id && m.SagId == 8)
                           .Sum(m => m.Quantity) ?? 0,
                        CoarseAmount = unitOfWork.ProductionsRepo.Fetch(m =>
                                               m.PermiteeId == permittee.Id &&
                                               (m.ReceiptDate >= item.DateFrom && m.ReceiptDate <= item.DateTo) &&
                                               m.Quarries.Id == i.Id && m.SagId == 8).Sum(m => m.Quantity * m.Sags.UnitCost) ?? 0,
                        Boulders = unitOfWork.ProductionsRepo.Fetch(m =>
                                           m.PermiteeId == permittee.Id &&
                                           (m.ReceiptDate >= item.DateFrom && m.ReceiptDate <= item.DateTo) &&
                                           m.Quarries.Id == i.Id && m.SagId == 6)
                           .Sum(m => m.Quantity) ?? 0,
                        BouldersAmount = unitOfWork.ProductionsRepo.Fetch(m =>
                                                 m.PermiteeId == permittee.Id &&
                                                 (m.ReceiptDate >= item.DateFrom && m.ReceiptDate <= item.DateTo) &&
                                                 m.Quarries.Id == i.Id && m.SagId == 6)
                           .Sum(m => m.Quantity) ?? 0,
                        Crushed = unitOfWork.ProductionsRepo.Fetch(m =>
                                          m.PermiteeId == permittee.Id &&
                                          (m.ReceiptDate >= item.DateFrom && m.ReceiptDate <= item.DateTo) &&
                                          m.Quarries.Id == i.Id && m.SagId == 7)
                           .Sum(m => m.Quantity) ?? 0,
                        CrushedAmount = unitOfWork.ProductionsRepo.Fetch(m =>
                                m.PermiteeId == permittee.Id &&
                                (m.ReceiptDate >= item.DateFrom && m.ReceiptDate <= item.DateTo) &&
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

            SummaryProductionReports summary = new SummaryProductionReports()
            {
                DataSource = reports
            };



            return PartialView("SummaryProductionReportPerPermitteePartial", summary);
        }






        [OnUserAuthorization(ActionName = "Permittee List")]
        [Route("permittee-list")]
        public ActionResult PermitteeListReport()
        {
            return View();
        }

        public ActionResult PermitteeListReportPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? permitteeTypeId, [ModelBinder(typeof(DevExpressEditorsBinder))]int? year, [ModelBinder(typeof(DevExpressEditorsBinder))]int? month)
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
        public ActionResult VehicleMasterListPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? permitteeId)
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
        public ActionResult DetailProductionsPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? permitteeId, [ModelBinder(typeof(DevExpressEditorsBinder))]int? year, [ModelBinder(typeof(DevExpressEditorsBinder))] int? month)
        {
            var permittee = unitOfWork.PermiteesRepo.Fetch();
            if (permitteeId != 0)
                permittee = permittee.Where(x => x.Id == permitteeId);
            var res = permittee.Where(x => x.Productions.Any(m => SqlFunctions.DatePart("month", m.DateCreated) == month && SqlFunctions.DatePart("year", m.DateCreated) == year)).ToList();

            var rpt = new rptProductionLedgerList()
            {
                DataSource = res
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