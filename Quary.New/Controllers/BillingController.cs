using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Repository;

namespace Quary.New.Controllers
{
    public class BillingController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Billing
        public ActionResult Index()
        {
            return View();
        }

     
        public ActionResult AddEditBillingPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? billingId)
        {
            return PartialView(unitOfWork.BillingsRepo.Find(m => m.Id == billingId));
        }
        [ValidateInput(false)]
        public ActionResult BillingGridViewPartial()
        {
            var model = unitOfWork.BillingsRepo.Get();
            return PartialView("_BillingGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult BillingGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Billings item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    var transaction = unitOfWork.TransactionsRepo.Find(m => m.Id == item.TransactionId);
                    transaction.OfficialReceipt = item.OfficialReceipt;
                    item.LastModifiedBy = User.Identity.GetFullName();
                    item.DateCreated = DateTime.Now;
                    item.DateModified = DateTime.Now;
                    item.CreatedBy = User.Identity.GetFullName();
                    transaction.Billings.Add(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.BillingsRepo.Get();
            return PartialView("_BillingGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult BillingGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Billings item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var billing = unitOfWork.BillingsRepo.Find(m => m.Id == item.Id);
                    billing.Transactions.OfficialReceipt = item.OfficialReceipt;
                    billing.Amount = item.Amount;
                    billing.LastModifiedBy = User.Identity.GetFullName();
                    billing.DateModified = DateTime.Now;
                    unitOfWork.Save();
                    // Insert here a code to update the item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.BillingsRepo.Get();
            return PartialView("_BillingGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult BillingGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))] System.Int32 Id)
        {

            if (Id >= 0)
            {
                try
                {
                    var transaction = unitOfWork.TransactionsRepo.Find(m => m.Billings.Any(x => x.Id == Id));
                    transaction.OfficialReceipt = null;
                    unitOfWork.BillingsRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.BillingsRepo.Get();
            return PartialView("_BillingGridViewPartial", model);
        }

        public ActionResult PrintOfficialReceipt([ModelBinder(typeof(DevExpressEditorsBinder))] System.Int32 billingId)
        {
            var rpt = new rptOfficialReceipt()
            {
                DataSource=unitOfWork.BillingsRepo.Get(m=>m.Id== billingId)
            };
            return PartialView(rpt);
        }
    }
}