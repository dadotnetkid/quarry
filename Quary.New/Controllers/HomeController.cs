using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using DevExpress.Web.Mvc;
using System.Web.Mvc;
using Helpers;
using Models.Repository;

namespace Quary.New.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        [OnUserAuthorization(ActionName = "dashboard")]
        public ActionResult Index()
        {
            return View();
        }


        public PartialViewResult DashboardPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? year)
        {
            ViewBag.Year = year;

            return PartialView();
        }

        public ActionResult TabYearPartial()
        {
            return PartialView();
        }

        [ValidateInput(false)]
        public ActionResult UnPaidTransactionGridViewPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? year)
        {
            var dateFrom = Convert.ToDateTime("01/01/" + year);
            var dateTo = Convert.ToDateTime("12/31/" + year);
            ViewBag.Year = year;
            var model = unitOfWork.TransactionsRepo.Get(m => m.OfficialReceipt != null && (m.TransactionDate >= dateFrom && m.TransactionDate <= dateTo));
            return PartialView("_UnPaidTransactionGridViewPartial", model);
        }

        [ValidateInput(false)]
        public ActionResult RecentTransactionGridViewPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? year)
        {
            ViewBag.Year = year;
            var model = unitOfWork.TransactionsRepo.Fetch(m => DbFunctions.DiffDays(m.TransactionDate, DateTime.Now) <= 1).ToList();
            return PartialView("_RecentTransactionGridViewPartial", model);
        }

        public ActionResult InformationsPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? year)
        {
            ViewBag.Year = year;
            var dateFrom = Convert.ToDateTime("01/01/" + year);
            var dateTo = Convert.ToDateTime("12/31/" + year);
            ViewBag.UnPaid = unitOfWork.TransactionsRepo.Fetch(m => m.OfficialReceipt == null && (m.TransactionDate >= dateFrom && m.TransactionDate <= dateTo))
                .Sum(m => m.TransactionTotal)?.ToString("#,#.##");
            ViewBag.Paid = unitOfWork.TransactionsRepo.Fetch(m => m.OfficialReceipt != null && (m.TransactionDate >= dateFrom && m.TransactionDate <= dateTo))
                .Sum(m => m.TransactionTotal)?.ToString("#,#.##");
            ViewBag.TotalTransaction =
                unitOfWork.TransactionsRepo.Fetch(m => (m.TransactionDate >= dateFrom && m.TransactionDate <= dateTo)).Sum(m => m.TransactionTotal)?.ToString("#,#.##");

            ViewBag.RecentTransaction = unitOfWork.TransactionsRepo
                .Fetch(m => DbFunctions.DiffDays(m.TransactionDate, DateTime.Now) <= 1).Sum(m => m.TransactionTotal)
                ?.ToString("#,#.##");
            return PartialView();
        }
    }
}