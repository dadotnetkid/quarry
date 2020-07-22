using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Models;
using Models.Repository;

namespace Quary.New.Controllers
{
    public class DeliveryReceiptsController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: DeliveryReceipts
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult TransactionGridViewPartial()
        {
            //this.TransactionDetails.Where(x => x.ItemId == 15).Sum(x => x.Quantity);
            var model = unitOfWork.TransactionsRepo.Fetch(
                     ).Select(x => new
                     {
                         x.Id,
                         x.TransactionTypeId,
                         DeliveryReceiptCount = x.DeliveryReceipts.Count(),
                         TotalAdditionalDR= x.TransactionDetails.Where(m => m.ItemId == 15).Sum(m => m.Quantity),
                         DeliveryReceiptTotal = x.TransactionDetails.Where(m => m.ItemId == 15).Sum(m => m.Quantity) - x.DeliveryReceipts.Count(),
                         x.TransactionNumber,
                         CompanyName = x.Permitees.CompanyName,
                         x.TransactionDate
                     })
                .Where(x => x.TransactionTypeId == 1 || x.TransactionTypeId == 2 || x.TransactionTypeId == 3 || x.TransactionTypeId == 4 ||
                            x.TransactionTypeId == 5 ||
                            x.TransactionTypeId == 6).
                            Where(x => x.DeliveryReceiptCount < x.TotalAdditionalDR)
                .ToList();

            return PartialView("_TransactionGridViewPartial", model);
        }

        public ActionResult AddDeliveryReceiptPopupPartial([ModelBinder(typeof(DevExpressEditorsBinder))] string transactionId)
        {
            ViewBag.TransactionId = transactionId;
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult AddDeliveryReceiptContentPartial([ModelBinder(typeof(DevExpressEditorsBinder))] string transactionId)
        {
            ViewBag.TransactionId = transactionId;
            return PartialView();
        }
        [HttpPost]
        public ActionResult AddDeliveryReceiptContentPartial([ModelBinder(typeof(DevExpressEditorsBinder))] DeliveryReceipts item)
        {
            try
            {
                ViewBag.TransactionId = item.TransactionId;
                if (unitOfWork.DeliveryReceiptsRepo.Fetch(x => x.ReceiptNumber == item.ReceiptNumber).Any())
                {
                    ViewBag.ReturnStatus = "Failed to add DR, Existing DR is inputted";
                    return PartialView();
                }
                item.CreatedBy = User.Identity.GetUserId();
                item.Remarks = "Added from Delivery Receipt Controller";
                unitOfWork.DeliveryReceiptsRepo.Insert(item);
                unitOfWork.Save();
                ViewBag.ReturnStatus = "Successfully added dr to transaction";
            }
            catch (Exception e)
            {
            }
            return PartialView();
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult TransactionGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Transactions item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_TransactionGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TransactionGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Transactions item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_TransactionGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TransactionGridViewPartialDelete(System.String Id)
        {
            var model = new object[0];
            if (Id != null)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_TransactionGridViewPartial", model);
        }
    }
}