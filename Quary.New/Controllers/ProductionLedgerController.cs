using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using Helpers;
using Helpers.Reports;
using Models;
using Models.Repository;
using Models.ViewModels;

namespace Quary.New.Controllers
{
    public class ProductionLedgerController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: ProductionLedger
        public ActionResult Index()
        {
            return View();
        }
        [Route("report/production-ledger")]
        public ActionResult ProductionLedger()
        {
            return View();
        }

        public ActionResult CboQuarriesInPermittee([ModelBinder(typeof(DevExpressEditorsBinder))]AccountingReportViewModel item)
        {
            return PartialView(item);
        }


        public ActionResult cboMonthPartial(int? year, int? permitteeId)
        {
            var months = unitOfWork.ProductionsRepo.Fetch();
            if (permitteeId != 0)
            {
                months = months.Where(x => x.PermiteeId == permitteeId);
            }
            var ret = months.Where(x => SqlFunctions.DatePart("year", x.DateCreated) == year).ToList().Select(x => new
            {
                month = x.DateCreated?.Month,
            }).GroupBy(x => x.month).Select(x => new
            {
                month = x.Key,
                monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(x.Key ?? 1)
            }).ToList();
            return PartialView(ret);
        }
        public ActionResult cboYearPartial(int? permitteeId)
        {
            var months = unitOfWork.ProductionsRepo.Fetch();
            if (permitteeId != 0)
                months = months.Where(x => x.PermiteeId == permitteeId);
            var item = months.ToList().Select(x => new { year = x.DateCreated?.Year }).ToList().GroupBy(x => x.year).Select(x => new { year = x.Key }).ToList();
            return PartialView(item);
        }
        public ActionResult cboPermitteePartial(string permitteeTypeId)
        {
            var list = new List<Permitees>() { new Permitees() { FirstName = "ALL", CompanyName = "", Id = 0 } };
            if (!string.IsNullOrEmpty(permitteeTypeId))
                foreach (var i in permitteeTypeId.Split(','))
                {
                    var id = i.ToInt();
                    var model = unitOfWork.PermiteesRepo.Get(x => x.PermiteeTypeId == id);
                    list.AddRange(model);
                }

            return PartialView(list);
        }

        public ActionResult CboPermitteeTypePartial()
        {
            return PartialView();
        }
        [ValidateInput(false)]
        public ActionResult ProductionLedgerGridViewPartial([ModelBinder(typeof(DevExpressEditorsBinder))]ProductionLedgerReport item)
        {

            var permittee = unitOfWork.PermiteesRepo.Fetch(includeProperties: "Productions");
            if (item.PermitteeId != 0)
                permittee = permittee.Where(x => x.Id == item.PermitteeId);
            var datefrom = new DateTime(item.Year.ToInt(), item.Month.ToInt(), 1);
            var dateTo = new DateTime(item.Year.ToInt(), item.Month.ToInt(), DateTime.DaysInMonth(item.Year.ToInt(), item.Month.ToInt())).AddHours(23);

            var res = permittee.ToList().Where(x => x.Productions.Any(m => (m.DateCreated >= datefrom && m.DateCreated <= dateTo)));

            foreach (var i in res)
            {
                i.Productions = unitOfWork.ProductionsRepo.Get(m => (m.DateCreated >= datefrom && m.DateCreated <= dateTo) && m.PermiteeId == i.Id).ToList();
            }
            var rpt = new rptDetailProductionPivot()
            {
                DataSource = res
            };
            foreach (var i in res)
            {
                List<ProductionLedgerReport> reports = new List<ProductionLedgerReport>();

                foreach (var quarry in i.Quarries)
                {
                    var transaction = unitOfWork.TransactionSagsRepo.Fetch(m =>
                        m.Transactions.PermiteeId == i.Id).Where(x => x.Transactions.Permitees.Quarries.Any(m => m.Id == quarry.Id));
                    var r = new ProductionLedgerReport()
                    {
                        Boulders = transaction.Where(x => x.SagId == 6).Sum(m => m.Quantity)??0,
                        BouldersAmount= transaction.Where(x => x.SagId == 6).Sum(m => m.Quantity*m.Sags.UnitCost)??0,
                        Coarse= transaction.Where(x => x.SagId == 8).Sum(m => m.Quantity)??0,
                        CoarseAmount= transaction.Where(x => x.SagId == 8).Sum(m => m.Quantity * m.Sags.UnitCost)??0,
                        Crushed = transaction.Where(x => x.SagId == 7).Sum(m => m.Quantity)??0,
                        CrushedAmount= transaction.Where(x => x.SagId == 7).Sum(m => m.Quantity * m.Sags.UnitCost)??0,
                        Fined = transaction.Where(x => x.SagId == 5).Sum(m => m.Quantity)??0,
                        FinedAmount= transaction.Where(x => x.SagId == 5).Sum(m => m.Quantity * m.Sags.UnitCost)??0,
                        Mixed = transaction.Where(x => x.SagId == 4).Sum(m => m.Quantity)??0,
                        MixedAmount= transaction.Where(x => x.SagId == 4).Sum(m => m.Quantity * m.Sags.UnitCost)??0,
                        OrdinaryEarth = transaction.Where(x => x.SagId == 1).Sum(m => m.Quantity)??0,
                        OrdinaryEarthAmount= transaction.Where(x => x.SagId == 1).Sum(m => m.Quantity * m.Sags.UnitCost)??0,
                        ProductionBoulders = i.Productions.Where(x => x.SagId == 6 && x.QuarriesId == quarry.Id).Sum(x => x.Quantity) ?? 0,
                        ProductionBouldersAmount= i.Productions.Where(x => x.SagId == 6 && x.QuarriesId == quarry.Id).Sum(x => x.Quantity * x.Sags.UnitCost) ?? 0,
                        ProductionCoarse= i.Productions.Where(x => x.SagId == 8 && x.QuarriesId == quarry.Id).Sum(x => x.Quantity) ?? 0,
                        ProductionCoarseAmount= i.Productions.Where(x => x.SagId == 8 && x.QuarriesId == quarry.Id).Sum(x => x.Quantity * x.Sags.UnitCost) ?? 0,
                        ProductionCrushed= i.Productions.Where(x => x.SagId == 7 && x.QuarriesId == quarry.Id).Sum(x => x.Quantity) ?? 0,
                        ProductionCrushedAmount= i.Productions.Where(x => x.SagId == 7 && x.QuarriesId == quarry.Id).Sum(x => x.Quantity * x.Sags.UnitCost) ?? 0,
                        ProductionMixed= i.Productions.Where(x => x.SagId == 4 && x.QuarriesId == quarry.Id).Sum(x => x.Quantity) ?? 0,
                        ProductionMixedAmount= i.Productions.Where(x => x.SagId == 4 && x.QuarriesId == quarry.Id).Sum(x => x.Quantity * x.Sags.UnitCost) ?? 0,
                        ProductionOrdinaryEarth= i.Productions.Where(x => x.SagId == 1 && x.QuarriesId == quarry.Id).Sum(x => x.Quantity) ?? 0,
                        ProductionOrdinaryEarthAmount= i.Productions.Where(x => x.SagId == 1 && x.QuarriesId == quarry.Id).Sum(x => x.Quantity * x.Sags.UnitCost) ?? 0,
                        ProductionFinedAmount = i.Productions.Where(x => x.SagId == 5 && x.QuarriesId == quarry.Id).Sum(x => x.Quantity * x.Sags.UnitCost),
                        ProductionFined = i.Productions.Where(x => x.SagId == 5 && x.QuarriesId == quarry.Id).Sum(x => x.Quantity) ?? 0,
                        Quarries = quarry,
                        Permitees = i,


                    };
                    reports.Add(r);
                }

                i.ProductionLedgerReports = reports;
            }


            /*   var OrdinaryEarth = reports.Sum(m => m.ProductionOrdinaryEarth).ToInt();
                   var Mixed = reports.Sum(m => m.ProductionMixed).ToInt();
                   var Fined = reports.Sum(m => m.ProductionFined).ToInt();
                   var Coarse = reports.Sum(m => m.ProductionCoarse).ToInt();
                   var Boulders = reports.Sum(m => m.ProductionBoulders).ToInt();
                   var Crushed = reports.Sum(m => m.ProductionCrushed).ToInt();
                   *
                   //reports.ForEach(x =>
                   //{
                   //    x.OrdinaryEarth = x.OrdinaryEarth.ToInt() - OrdinaryEarth;
                   //    x.Mixed = x.Mixed.ToInt() - Mixed;
                   //    x.Fined = x.Fined.ToInt() - Fined;
                   //    x.Coarse = x.Coarse.ToInt() - Coarse;
                   //    x.Boulders = x.Boulders.ToInt() - Boulders;
                   //    x.Crushed = x.Crushed.ToInt() - Crushed;
                   //});
                   permittee.ProductionLedgerReports = reports;*/



            //    Session["ProductionLedgerReport"] = reports;

            SummaryLedgerReportByRange summaryLedgerReport = new SummaryLedgerReportByRange() { DataSource = new List<SummaryProductionReport>() { new SummaryProductionReport() { Permitees = res.ToList(), Month = item.Month, Year = item.Year } } };
            return PartialView("_ProductionLedgerGridViewPartial", summaryLedgerReport);
        }
    }
}