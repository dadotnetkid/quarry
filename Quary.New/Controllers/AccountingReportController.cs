using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using Helpers;
using Models;
using Models.Repository;
using Models.ViewModels;

namespace Quary.New.Controllers
{
    public class AccountingReportController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        [Route("report/accounting-report")]
        // GET: AccountingReport
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AccountingReport()
        {
            return PartialView(new AccountingReportViewModel());
        }
        public ActionResult AccountingReportPartial([ModelBinder(typeof(DevExpressEditorsBinder))]AccountingReportViewModel item)
        {

            var dateFrom = new DateTime(item.Year, item.Month, 1);
            var dateTo = new DateTime(item.Year, item.Month, DateTime.DaysInMonth(item.Year, item.Month)).AddHours(23).AddMinutes(59).AddSeconds(59);

            List<AccountingReportViewModel> accountingReportViewModels = new List<AccountingReportViewModel>();
            UnitOfWork unitOfWork = new UnitOfWork();
            var permittees = unitOfWork.PermiteesRepo.Fetch(includeProperties: "Productions");

            var res = permittees.Where(x => x.Productions.Any(m => (m.DateCreated >= dateFrom && m.DateCreated <= dateTo)));
            var type = item.PermitteeType.Split(',')?.Select(x => new { Id = x.ToInt() }).Select(x => x.Id).ToList();
            res = res.Where(x => type.Contains(x.PermiteeTypeId ?? 0));
            if (item.PermitteeId != 0)
            {
                res = res.Where(x => x.Id == item.PermitteeId);
            }
            decimal prodTotal = 0;
            foreach (var permittee in res.ToList())
            {
                permittee.Productions = unitOfWork.ProductionsRepo.Get(m => (m.DateCreated >= dateFrom && m.DateCreated <= dateTo) && m.Transactions.PermiteeId == permittee.Id).ToList();
                // var totalExtraction = permittee.Productions.Sum(x => (x.Quantity ?? 0) * (x.Sags.UnitCost ?? 0));


                //for accounting
                var transaction = unitOfWork.TransactionsRepo.Fetch(m =>
                    m.Productions.Any(x => x.DateCreated >= dateFrom && x.DateCreated <= dateTo) && m.PermiteeId == permittee.Id);
                foreach (var t in transaction.ToList())
                {
                    var quarry = unitOfWork.ProductionsRepo.Fetch()
                        .Where(x => (x.DateCreated >= dateFrom && x.DateCreated <= dateTo) &&
                                    x.Transactions.PermiteeId == permittee.Id && x.TransactionId == t.Id);


                    if (item.QuarryId != 0)
                    {
                        quarry = quarry.Where(x => x.Id == item.QuarryId);
                    }

                    var _quarry = quarry.GroupBy(x => x.QuarriesId).ToList();
                    foreach (var q in _quarry)
                    {
                        var quarryId = q.Key?.ToInt();
                        var accountingReportViewModel = new AccountingReportViewModel()
                        {
                            Permittee = t.Permitees,
                            OfficialReceipt = t.OfficialReceipt,
                            Amount = t.SagSubTotal.ToDecimal()
                        };
                        var prod = unitOfWork.ProductionsRepo.Fetch()
                            .Where(x => (x.DateCreated >= dateFrom && x.DateCreated <= dateTo) &&
                                        x.Transactions.PermiteeId == permittee.Id && x.QuarriesId == quarryId && x.TransactionId == t.Id);

                        accountingReportViewModel.Extraction = prod?.Sum(x => (x.Quantity ?? 0) * (x.Sags.UnitCost ?? 0)) ?? 0;

                        accountingReportViewModel.Quarries = unitOfWork.QuarriesRepo.Find(m => m.Id == quarryId);
                        accountingReportViewModels.Add(accountingReportViewModel);
                    }
                }
                //x.QuarriesId == quarryId && x.TransactionId == t.Id).ToList();

            }
            rptAccountingReports reports = new rptAccountingReports()
            {
                DataSource = accountingReportViewModels
            };
            return PartialView(reports);
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
    }
}