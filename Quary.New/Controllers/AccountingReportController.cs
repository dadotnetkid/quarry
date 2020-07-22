using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
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
            item.DateFrom = new DateTime(item.Year, item.Month, 1);
            item.DateTo = new DateTime(item.Year, item.Month, DateTime.DaysInMonth(item.Year, item.Month)).AddHours(23).AddMinutes(59).AddSeconds(59);
            var transaction = unitOfWork.TransactionsRepo.Fetch(m =>
                  m.TransactionDate >= item.DateFrom && m.TransactionDate <= item.DateTo);
            if (item.PermitteeId != 0)
            {
                transaction = transaction.Where(m => m.PermiteeId == item.PermitteeId);
            }

            List<AccountingReportViewModel> accountingReportViewModels = new List<AccountingReportViewModel>();


            foreach (var t in transaction.ToList())
            {
                //var dr = t.DeliveryReceipts.Select(x => x.ReceiptNumber).ToList();
                //var production =unitOfWork.ProductionsRepo.Get(m => (m.ReceiptDate >= item.DateFrom && m.ReceiptDate <= item.DateTo)).Where(m => dr.Contains(m.ReceiptNo.ToInt())).ToList();
                //
                var quarries = t.Productions.GroupBy(m => m.QuarriesId);
                if (item.QuarryId != 0)
                {
                    quarries = quarries.Where(x => x.Key == item.QuarryId);
                }
                foreach (var i in quarries)
                {
                    if (i.Key != null)
                    {
                        var accountingReportViewModel = new AccountingReportViewModel()
                        {
                            Permittee = t.Permitees,
                            OfficialReceipt = t.OfficialReceipt,
                            Amount = t.SagSubTotal.ToDecimal()
                        };
                        var quarryId = i.Key.ToInt();
                        accountingReportViewModel.Extraction = t.Productions.Where(m => m.QuarriesId == quarryId)
                            .Sum(m => m.Quantity * m.Sags.UnitCost).ToDecimal();
                        accountingReportViewModel.Quarries = unitOfWork.QuarriesRepo.Find(m => m.Id == quarryId);
                        accountingReportViewModels.Add(accountingReportViewModel);
                    }

                }
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