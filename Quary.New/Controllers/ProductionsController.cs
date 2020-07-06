using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using Helpers;
using Microsoft.AspNet.Identity;
using Models;
using Models.Repository;

namespace Quary.New.Controllers
{
    public class ProductionsController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Production
        public ActionResult Index()
        {
            return View();
        }


        #region Production

        [OnUserAuthorization(ActionName = "Productions")]
        public ActionResult Productions()
        {
            return View();
        }

        public ActionResult CboQuarryPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            int? permitteeId, [ModelBinder(typeof(DevExpressEditorsBinder))]
            int? quarryId)
        {
            var model = unitOfWork.QuarriesRepo.Get(m => m.Permitees.Any(x => x.Id == permitteeId));
            ViewBag.QuarryId = quarryId;
            ViewBag.PermitteeId = permitteeId;
            return PartialView(model);
        }

        public ActionResult CboDeliveryReceiptPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            int? permitteeId, [ModelBinder(typeof(DevExpressEditorsBinder))]int? deliveryReceiptNo)
        {
            var model = unitOfWork.DeliveryReceiptsRepo.Get(m => m.Transactions.PermiteeId == permitteeId);
            ViewBag.deliveryReceiptNo = deliveryReceiptNo;
            ViewBag.PermitteeId = permitteeId;
            return PartialView(model);
        }

        public ActionResult CboVehiclesPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            int? permitteeId, [ModelBinder(typeof(DevExpressEditorsBinder))]int? vehicleId)
        {
            var model = unitOfWork.VehiclesRepo.Get(m => m.PermiteeId == permitteeId);
            ViewBag.VehicleId = vehicleId;
            ViewBag.PermitteeId = permitteeId;
            return PartialView(model);
        }
        [ValidateInput(false)]
        public ActionResult ProductionsGridViewPartial([ModelBinder(typeof(DevExpressEditorsBinder))]Productions item, string filterText = "")
        {
            ViewBag.filterText = filterText;

            var model = unitOfWork.ProductionsRepo.Get(includeProperties: "Permitees,Vehicles,Sags");
            if (!string.IsNullOrEmpty(filterText))
                model = model.Where(x => x.Permitees != null).Where(x => x.Permitees.CompanyName.Contains(filterText)).ToList();
            if (item.ReceiptNo != null)
            {
                var permittee = unitOfWork.DeliveryReceiptsRepo.Find(m => m.ReceiptNumber == item.ReceiptNo)
                    ?.Transactions?.Permitees;
                var totalSagQuantity = unitOfWork.TransactionSagsRepo.Get(m => m.Transactions.PermiteeId == permittee.Id && m.SagId == item.SagId)
                    .Sum(m => m.Quantity);
                var orderedSagQuantity =
                    unitOfWork.ProductionsRepo.Get(m => m.PermiteeId == permittee.Id && m.SagId == item.SagId).Sum(m => m.Quantity);

                item.RemainingSagQuantity = (totalSagQuantity ?? 0) - (orderedSagQuantity ?? 0);

            }
            ViewData["Model"] = item;
            var receipts = unitOfWork.ProductionsRepo.Get().Select(x => x.ReceiptNo);
            ViewBag.ReceiptNo = unitOfWork.DeliveryReceiptsRepo.Get().Where(m => !receipts.Contains(m.ReceiptNumber));
            return PartialView("_ProductionGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "Add Productions")]
        public ActionResult ProductionsGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Productions item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    item.CreatedBy = User.Identity.GetUserId();
                    item.DateCreated = DateTime.Now;
                    item.TransactionId = unitOfWork.DeliveryReceiptsRepo.Find(m => m.ReceiptNumber == m.ReceiptNumber)
                        ?.TransactionId;
                    item.PermiteeId = unitOfWork.TransactionsRepo.Find(m => m.Id == item.TransactionId)?.PermiteeId;
                    // Insert here a code to insert the new item in your model
                    unitOfWork.ProductionsRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.ProductionsRepo.Get(includeProperties: "Permitees,Vehicles,Sags");
            return PartialView("_ProductionGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "Update Productions")]
        public ActionResult ProductionsGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Productions item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model

                    var delivery = unitOfWork.ProductionsRepo.Find(m => m.Id == item.Id);
                    delivery.PermiteeId = item.PermiteeId;
                    delivery.SagId = item.SagId;
                    delivery.VehicleId = item.VehicleId;
                    delivery.Quantity = item.Quantity ?? delivery.Quantity;
                    delivery.OriginId = item.OriginId;
                    delivery.DestinationId = item.DestinationId;
                    delivery.ReceiptNo = item.ReceiptNo;

                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.ProductionsRepo.Get(includeProperties: "Permitees,Vehicles,Sags");
            return PartialView("_ProductionGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "Delete Productions")]
        public ActionResult ProductionsGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]int? Id)
        {

            if (Id >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    unitOfWork.ProductionsRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.ProductionsRepo.Get(includeProperties: "Permitees,Vehicles,Sags");
            return PartialView("_ProductionGridViewPartial", model);
        }

        public ActionResult AddEditProductionPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? deliveryId, [ModelBinder(typeof(DevExpressEditorsBinder))]int? permiteeId)
        {
            var model = unitOfWork.ProductionsRepo.Find(m => m.Id == deliveryId);
            permiteeId = permiteeId ?? model?.PermiteeId;

            ViewBag.Vehicles = unitOfWork.VehiclesRepo.Get(m => m.PermiteeId == permiteeId);
            var receipts = unitOfWork.ProductionsRepo.Get().Select(x => x.ReceiptNo);
            ViewBag.ReceiptNo = unitOfWork.DeliveryReceiptsRepo.Get().Where(m => !receipts.Contains(m.ReceiptNumber));
            return PartialView(model);
        }


        public ActionResult BatchEditingProductionsGridViewPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            MVCxGridViewBatchUpdateValues<Productions, int> updateValues)
        {
            foreach (var i in updateValues.Insert)
            {
                var deliveryReceipts = unitOfWork.DeliveryReceiptsRepo.Find(x => x.ReceiptNumber == i.ReceiptNo);
                i.TransactionId = deliveryReceipts?.TransactionId;

                i.DateCreated = DateTime.Now;
                unitOfWork.ProductionsRepo.Insert(i);
            }
            foreach (var i in updateValues.Update)
            {
                var deliveryReceipts = unitOfWork.DeliveryReceiptsRepo.Find(x => x.ReceiptNumber == i.ReceiptNo);

                var ret = unitOfWork.ProductionsRepo.Find(x => x.Id == i.Id);
                ret.TransactionId = deliveryReceipts.TransactionId;
                ret.PermiteeId = i.PermiteeId;
                ret.QuarriesId = i.QuarriesId;
                ret.SagId = i.SagId;
                ret.Quantity = i.Quantity;
                ret.VehicleId = i.VehicleId;
                ret.OriginId = i.OriginId;
                ret.ReceiptNo = i.ReceiptNo;
                ret.ProductionDate = i.ProductionDate;
                ret.ProgramOfWorkId = i.ProgramOfWorkId;
                ret.Destination = i.Destination;
                ret.BundleCode = i.BundleCode;
            
            }
            foreach (var i in updateValues.DeleteKeys)
            {

                unitOfWork.ProductionsRepo.Delete(x => x.Id == i);
            }

            unitOfWork.Save();
            return PartialView("_ProductionGridViewPartial", unitOfWork.ProductionsRepo.Get(includeProperties: "Permitees,Vehicles,Sags"));
        }
        #endregion


        public ActionResult UploadControlUpload()
        {
            var files = UploadControlExtension.GetUploadedFiles("UploadControl", TransactionsControllerUploadControlSettings.UploadValidationSettings, TransactionsControllerUploadControlSettings.FileUploadComplete);

            return null;
        }
    }
    public class TransactionsControllerUploadControlSettings
    {
        public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".xls", ".xlsx" },
            MaxFileSize = 4000000
        };
        public static void FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {

                var path = HttpContext.Current.Server.MapPath("~/content/excel/" + Guid.NewGuid().ToString() + ".xlsx");
                e.UploadedFile.SaveAs(path);
                HttpContext.Current.Session["excel"] = new ExcelHelper(path).ExecuteReader();
                System.IO.File.Delete(path);
            }
        }
    }
}
