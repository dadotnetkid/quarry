using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helpers;
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
        [Route("delivery-receipts")]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "DeliveryReceipts")]
        public ActionResult DeliveryReceipts()
        {
            return View();
        }

        #region Transaction
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
                         TotalAdditionalDR = x.TransactionDetails.Where(m => m.ItemId == 15).Sum(m => m.Quantity),
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


        #endregion

        #region Delivery Receipts

        [ValidateInput(false)]

        public ActionResult DeliveryReceiptGridViewPartial()
        {
            var model = unitOfWork.DeliveryReceiptsRepo.Fetch(includeProperties: "Transactions.Permitees");
            return PartialView("_DeliveryReceiptGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "Add Delivery Receipts")]
        public ActionResult DeliveryReceiptGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.DeliveryReceipts item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var transaction = unitOfWork.TransactionsRepo.Find(m => m.Id == item.TransactionId, includeProperties: "TransactionDetails");
                    var deliveryQTY = transaction.TransactionDetails
                        .FirstOrDefault(m => m.Items.ItemName.ToLower().Contains("delivery"))?.Quantity;
                    deliveryQTY = deliveryQTY - 1;
                    var receiptNo = item.ReceiptNumber.ToInt() - 1;
                    for (var i = 1; i <= deliveryQTY; i++)
                    {
                        var receiptNumber = receiptNo;
                        receiptNumber = receiptNumber + i;
                        unitOfWork.DeliveryReceiptsRepo.Insert(new DeliveryReceipts()
                        {
                            ReceiptNumber = receiptNumber.ToString(),
                            TransactionId = transaction.Id
                        });
                    }

                    unitOfWork.Save();
                    // Insert here a code to insert the new item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.DeliveryReceiptsRepo.Fetch(includeProperties: "Transactions.Permitees");
            return PartialView("_DeliveryReceiptGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "Delete Delivery Receipts")]
        public ActionResult DeliveryReceiptGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))] int? Id)
        {
            if (Id >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    unitOfWork.DeliveryReceiptsRepo.Delete(x => x.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.DeliveryReceiptsRepo.Fetch(includeProperties: "Transactions.Permitees");
            return PartialView("_DeliveryReceiptGridViewPartial", model);
        }

        public ActionResult AddEditDeliveryReceiptPartial()
        {
            return PartialView();
        }

        public PartialViewResult MultipleDeleteDeliveryReceiptsPopUpPartial()
        {
            return PartialView();
        }
        public PartialViewResult MultipleDeleteDeliveryReceiptsPartial()
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult DeleteMultipleDeleteDeliveryReceiptsPartial()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult DeleteMultipleDeleteDeliveryReceiptsPartial([ModelBinder(typeof(DevExpressEditorsBinder))] string[] deliveryReceipts)
        {
            if (deliveryReceipts.Any())
                foreach (var i in deliveryReceipts)
                {
                    var dr = unitOfWork.DeliveryReceiptsRepo.Find(x => x.ReceiptNumber == i);
                    if (dr == null)
                        continue;
                    unitOfWork.DeletedDeliveryReceiptsRepo.Insert(new Models.DeletedDeliveryReceipts()
                    {
                        Id = dr.Id,
                        CreatedBy = dr.CreatedBy,
                        ReceiptNumber = dr.ReceiptNumber,
                        Remarks = dr.Remarks,
                        TransactionId = dr.TransactionId,

                    });
                    unitOfWork.DeliveryReceiptsRepo.Delete(x => x.Id == dr.Id);
                    unitOfWork.Save();
                }
            return PartialView();
        }
        #endregion

        public ActionResult TransactionsWithBalance()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult TransactionWithBalanceGridViewPartial()
        {
            var model = unitOfWork.TransactionsRepo.Fetch();
            var res = model.Select(x => new
            {
                x.Id,
                x.TransactionDate,
                CompanyName = x.Permitees.CompanyName,
                x.TransactionNumber,
                x.TransactionTotal,
                x.OfficialReceipt,
                Balance = x.TransactionTotal - x.Productions.Sum(m => (m.Quantity ?? 0) * (m.Sags.UnitCost ?? 0)) ?? 0,
                RemainingDR = (x.TransactionDetails.Where(m => m.ItemId == 15).Sum(m => m.Quantity) - x.DeliveryReceipts.Count()),
                OrdinaryEarth = x.TransactionSags.Where(m => m.SagId == 1).Sum(m => m.Quantity) - x.Productions.Where(m => m.SagId == 1).Sum(m => m.Quantity) ?? 0,
                Mixed = x.TransactionSags.Where(m => m.SagId == 4).Sum(m => m.Quantity) - x.Productions.Where(m => m.SagId == 4).Sum(m => m.Quantity) ?? 0,
                Fine = x.TransactionSags.Where(m => m.SagId == 5).Sum(m => m.Quantity) - x.Productions.Where(m => m.SagId == 5).Sum(m => m.Quantity) ?? 0,
                Boulder = x.TransactionSags.Where(m => m.SagId == 6).Sum(m => m.Quantity) - x.Productions.Where(m => m.SagId == 6).Sum(m => m.Quantity) ?? 0,
                Crushed = x.TransactionSags.Where(m => m.SagId == 7).Sum(m => m.Quantity) - x.Productions.Where(m => m.SagId == 7).Sum(m => m.Quantity) ?? 0,
                Screen = x.TransactionSags.Where(m => m.SagId == 8).Sum(m => m.Quantity) - x.Productions.Where(m => m.SagId == 8).Sum(m => m.Quantity) ?? 0,


            }).Where(x => x.Balance > 0 && !string.IsNullOrEmpty(x.OfficialReceipt));
            return PartialView("_TransactionWithBalanceGridViewPartial", res.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TransactionWithBalanceGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Transactions item)
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
            return PartialView("_TransactionWithBalanceGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TransactionWithBalanceGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Transactions item)
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
            return PartialView("_TransactionWithBalanceGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TransactionWithBalanceGridViewPartialDelete(System.String Id)
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
            return PartialView("_TransactionWithBalanceGridViewPartial", model);
        }
    }
}