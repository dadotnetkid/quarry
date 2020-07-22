using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helpers;
using Microsoft.AspNet.Identity;
using Models.Repository;
using Helpers.Reports.BillingStatement;
using Models;
using Models.ViewModels;

namespace Quary.New.Controllers
{
    [Authorize]
    public partial class TransactionsController : Controller
    {

        public string UserId => User.Identity.GetUserId();

        // GET: Transactions
        private UnitOfWork unitOfWork = new UnitOfWork();

        [OnUserAuthorization(ActionName = "Transactions")]
        public ActionResult Index()
        {

            return View();
        }

        #region transaction-grid

        public ActionResult PopupSignatoriesPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            string transactionId)
        {
            ViewBag.TransactionId = transactionId;

            return PartialView();
        }
        public ActionResult SignatoriesPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            string transactionId)
        {
            var model = unitOfWork.TransactionsRepo.Find(m => m.Id == transactionId);
            return PartialView("SignatoriesPartial", model);
        }
        public ActionResult UpdateSignatoriesPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            Transactions item)
        {
            var model = unitOfWork.TransactionsRepo.Find(m => m.Id == item.Id); if (model == null) return PartialView("SignatoriesPartial", model);
            model.Signatory = item.Signatory;
            unitOfWork.Save();
            return PartialView("SignatoriesPartial", model);
        }
        public ActionResult TokenBoxProgramOfWorksPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            TransactionViewModel item)
        {

            var ProgramOfWorksRepo = unitOfWork.TransactionsRepo.Find(x => x.Id == item.TransactionId)?.ProgramOfWorks;
            // unitOfWork.ProgramOfWorksRepo.Get(m => m.Transactions.Any(x => x.Id == item.TransactionId));


            ViewBag.ProgramOfWorksRepo = unitOfWork.ProgramOfWorksRepo.Get();

            return PartialView(ProgramOfWorksRepo);
        }
        public ActionResult TokenBoxQuarriesPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            TransactionViewModel item)
        {

            /*  var model =
                  unitOfWork.QuarriesRepo.Get(m => m.Transactions.Any(x => x.Id == item.TransactionId));

      */
            ViewBag.QuarriesRepo = unitOfWork.QuarriesRepo.Get();

            return PartialView();
        }

        public ActionResult AddEditTransactionPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            TransactionViewModel item)
        {
            var model = unitOfWork.TransactionsRepo.Find(m => m.Id == item.TransactionId);
            ViewBag.TransactionTypeId = item?.TransactionTypeId ?? model?.TransactionTypeId;
            var TransactionTypeId = item?.TransactionTypeId ?? model?.TransactionTypeId;
            if (item.TransactionId == null)
                item.TransactionId = Guid.NewGuid().ToString();
            Debug.Write($"TransactionId {item.TransactionId}");
            ViewBag.TransactionId = item.TransactionId; //Guid.NewGuid().ToString();
            ViewBag.PermiteeId = model?.PermiteeId ?? item.PermiteeId;
            var _p = (model?.PermiteeId ?? item.PermiteeId);
            ViewBag.Quaries = unitOfWork.PermiteesRepo.Find(m => m.Id == _p)?._QuarySites;


            ViewBag.TransactionType = unitOfWork.TransactionTypesRepo.Find(m => m.Id == TransactionTypeId)
                ?.TransactionType;
            ViewBag.IsDelivery = item?.isDelivery;
            return PartialView(model);
        }

        [ValidateInput(false)]
        public ActionResult TransactionsGridViewPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
           TransactionViewModel item)
        {
            ViewBag.TransactionId = item?.TransactionId; //Guid.NewGuid().ToString();
            ViewBag.PermiteeId = item?.PermiteeId;
            ViewBag.TransactionTypeId = item?.TransactionTypeId;
            ViewBag.IsDelivery = item?.isDelivery;
            var model = unitOfWork.TransactionsRepo.Get();

            return PartialView("_TransactionsGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "Add Transactions")]
        public ActionResult TransactionsGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))]
            Models.Transactions item, [ModelBinder(typeof(DevExpressEditorsBinder))]
            TransactionViewModel viewModel)
        {
            item.FilingDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    item.TransactionDate = DateTime.Now;
                    item.TransactionNumber = new TransactionHelper().TransactionNumber;
                    var vehiclesCost = unitOfWork.TransactionVehiclesRepo.Fetch(m => m.TransactionId == viewModel.TransactionId)
                        .Sum(m => m.Cost);
                    var itemCost = unitOfWork.TransactionDetailsRepo.Fetch(m => m.TransactionId == viewModel.TransactionId)
                        .ToList()
                        .Sum(m => m.TotalCost);
                    var facilitiesCost = unitOfWork.TransactionFacilitiesRepo
                        .Fetch(m => m.TransactionId == viewModel.TransactionId)
                        .Sum(m => m.Cost);
                    var sagCost = unitOfWork.TransactionSagsRepo.Get(m => m.TransactionId == viewModel.TransactionId)
                        .Sum(m => m.TotalCost);
                    var subTotal = (vehiclesCost ?? 0) + (itemCost ?? 0) + (facilitiesCost ?? 0);

                    if (item.TransactionTypeId == (int)TransactionType.Renew)
                    {
                        var deadLine = new DateTime(DateTime.Now.Year, 1, 21);
                        if (item.FilingDate >= deadLine)
                        {
                            /* itemCost = unitOfWork.TransactionDetailsRepo.Fetch(m => m.TransactionId == item.Id && m.Items.Categories.Id == 2).ToList()
                                 .Sum(m => m.TotalCost);

                             subTotal = (vehiclesCost ?? 0) + (itemCost ?? 0) + (facilitiesCost ?? 0);
                             decimal? surcharge = (subTotal * 0.25M).ToDecimal();
                             var interest = (surcharge * (0.02M * item.FilingDate?.Month)).ToDecimal();
                             item.TransactionTotal = surcharge + subTotal + interest + (sagCost);
                             item.Surcharge = surcharge;
                             item.Interest = interest;*/
                            itemCost = unitOfWork.TransactionDetailsRepo.Fetch(m => m.TransactionId == viewModel.TransactionId && m.Items.IsIncludedSurcharge == true).ToList()
                                 .Sum(m => m.TotalCost);
                            var _itemCost = unitOfWork.TransactionDetailsRepo.Fetch(m => m.TransactionId == viewModel.TransactionId && m.Items.IsIncludedSurcharge != true).ToList()
                                  .Sum(m => m.TotalCost);

                            subTotal = (vehiclesCost ?? 0) + (itemCost ?? 0) + (facilitiesCost ?? 0);
                            var surcharge = (subTotal * 0.25M);
                            var interest = surcharge * (0.02M * item.FilingDate?.Month);
                            var penaltiesCost = unitOfWork.TransactionPenaltiesRepo.Get(m => m.TransactionId == viewModel.TransactionId)
                                .Sum(m => m.Amount);
                            item.TransactionTotal = surcharge + interest + subTotal + (sagCost) + (_itemCost ?? 0) + (penaltiesCost ?? 0);
                            item.Interest = interest;
                            item.Surcharge = surcharge;
                        }
                        else
                        {

                            var penaltiesCost = unitOfWork.TransactionPenaltiesRepo.Get(m => m.TransactionId == viewModel.TransactionId)
                                .Sum(m => m.Amount);
                            item.TransactionTotal = subTotal + sagCost + (penaltiesCost ?? 0);
                        }

                    }

                    else
                    {
                        item.TransactionTotal = subTotal + (sagCost);
                    }

                    if (viewModel.ProgramOfWorkIds != null)
                    {
                        foreach (var i in viewModel.ProgramOfWorkIds)
                        {
                            int id = i.ToInt();
                            item.ProgramOfWorks.Add(unitOfWork.ProgramOfWorksRepo.Find(m => m.Id == id));
                        }
                    }


                    item.Id = viewModel.TransactionId;
                    item.EntryBy = UserId;
                    item.LastEditedBy = UserId;
                    item.TransactionDate = DateTime.Now;
                    unitOfWork.TransactionsRepo.Insert(item);
                    unitOfWork.Save();
                    //insert delivery receipt
                    if (item.TransactionDetails.Any())
                    {
                        var deliveryQTY = item.TransactionDetails.FirstOrDefault(m => m.Items.ItemName.ToLower().Contains("delivery"))?.Quantity;
                        if (item.DeliveryReceipt != null)
                            for (var i = 1; i <= deliveryQTY; i++)
                            {
                                unitOfWork.DeliveryReceiptsRepo.Insert(new DeliveryReceipts()
                                {
                                    ReceiptNumber = (item.DeliveryReceipt.ToInt() + i).ToString(),
                                    TransactionId = item.Id
                                });
                            }
                    }
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";

            var model = unitOfWork.TransactionsRepo.Get(includeProperties: "Permitees,Permitees.PermiteeTypes");
            return PartialView("_TransactionsGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "Update Transactions")]
        public ActionResult TransactionsGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))]
            Models.Transactions item, [ModelBinder(typeof(DevExpressEditorsBinder))]
            TransactionViewModel viewModel)
        {
            //var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.TransactionsRepo.TrackModifiedEntities(m => m.Id == item.Id, item);

                    var transaction = unitOfWork.TransactionsRepo.Find(m => m.Id == item.Id, includeProperties: "ProgramOfWorks");
                    transaction.FilingDate = item.FilingDate;
                    transaction.OfficialReceipt = item.OfficialReceipt;
                    transaction.TransactionTypeId = item.TransactionTypeId;
                    var vehiclesCost = unitOfWork.TransactionVehiclesRepo.Fetch(m => m.TransactionId == item.Id)
                        .Sum(m => m.Cost);
                    var itemCost = unitOfWork.TransactionDetailsRepo.Fetch(m => m.TransactionId == item.Id).ToList()
                        .Sum(m => m.TotalCost);
                    var facilitiesCost = unitOfWork.TransactionFacilitiesRepo.Fetch(m => m.TransactionId == item.Id)
                        .Sum(m => m.Cost);
                    var sagCost = unitOfWork.TransactionSagsRepo.Get(m => m.TransactionId == viewModel.TransactionId)
                        .Sum(m => m.TotalCost);

                    var subTotal = (vehiclesCost ?? 0) + (itemCost ?? 0) + (facilitiesCost ?? 0);
                    //    var surcharge = (subTotal * 0.25M);
                    //   var interest = surcharge * .02M;
                    //  transaction.TransactionTotal = surcharge + subTotal + interest + (sagCost);
                    transaction.LastEditedBy = UserId;
                    if (item.TransactionTypeId == (int)TransactionType.Renew)
                    {
                        //      var subTotal = (vehiclesCost ?? 0) + (itemCost ?? 0) + (facilitiesCost ?? 0);
                        var deadLine = new DateTime(DateTime.Now.Year, 1, 21);

                        if (item.FilingDate >= deadLine)
                        {
                            itemCost = unitOfWork.TransactionDetailsRepo.Fetch(m => m.TransactionId == viewModel.TransactionId && m.Items.IsIncludedSurcharge == true).ToList()
                                  .Sum(m => m.TotalCost);
                            var _itemCost = unitOfWork.TransactionDetailsRepo.Fetch(m => m.TransactionId == viewModel.TransactionId && m.Items.IsIncludedSurcharge != true).ToList()
                                .Sum(m => m.TotalCost);

                            subTotal = (vehiclesCost ?? 0) + (itemCost ?? 0) + (facilitiesCost ?? 0);
                            var surcharge = (subTotal * 0.25M);
                            var interest = surcharge * (0.02M * item.FilingDate?.Month);
                            var penaltiesCost = unitOfWork.TransactionPenaltiesRepo.Get(m => m.TransactionId == viewModel.TransactionId)
                                .Sum(m => m.Amount);
                            transaction.TransactionTotal = surcharge + interest + subTotal + (sagCost) + (_itemCost ?? 0) + (penaltiesCost ?? 0);
                            transaction.Interest = interest;
                            transaction.Surcharge = surcharge;

                        }
                        else
                        {

                            var penaltiesCost = unitOfWork.TransactionPenaltiesRepo.Get(m => m.TransactionId == viewModel.TransactionId)
                                .Sum(m => m.Amount);
                            transaction.TransactionTotal = subTotal + sagCost + (penaltiesCost ?? 0);
                        }

                    }
                    else
                    {
                        var penaltiesCost = unitOfWork.TransactionPenaltiesRepo.Get(m => m.TransactionId == viewModel.TransactionId)
                            .Sum(m => m.Amount);
                        transaction.TransactionTotal = subTotal + sagCost + (penaltiesCost ?? 0);
                    }

                    if (viewModel.ProgramOfWorkIds != null)
                    {
                        transaction.ProgramOfWorks.Clear();
                        foreach (var i in viewModel.ProgramOfWorkIds)
                        {
                            int id = i.ToInt();
                            transaction.ProgramOfWorks.Add(unitOfWork.ProgramOfWorksRepo.Find(m => m.Id == id));
                        }
                    }




                    ///add delivery receipt
                    if (transaction.TransactionDetails.Any())
                    {
                        var deliveryQTY = item.TransactionDetails.FirstOrDefault(m => m.Items.ItemName.ToLower().Contains("delivery"))?.Quantity;
                        if (item.DeliveryReceipt != null)
                            transaction.DeliveryReceipts.Clear();
                        for (var i = 1; i <= deliveryQTY; i++)
                        {
                            transaction.DeliveryReceipts.Add(new DeliveryReceipts()
                            {
                                ReceiptNumber = (item.DeliveryReceipt.ToInt() + i).ToString(),
                                TransactionId = item.Id
                            });
                        }
                    }
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

            var model = unitOfWork.TransactionsRepo.Get(includeProperties: "Permitees,Permitees.PermiteeTypes");
            return PartialView("_TransactionsGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "Delete Transactions")]
        public ActionResult TransactionsGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]
            string Id)
        {
            //var model = new object[0];
            if (Id != null)
            {
                try
                {
                    unitOfWork.TransactionsRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }

            var model = unitOfWork.TransactionsRepo.Get(includeProperties: "Permitees,Permitees.PermiteeTypes");
            return PartialView("_TransactionsGridViewPartial", model);
        }



        #endregion

        #region transaction-detail

        public ActionResult AddEditTransactionDetailsPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            TransactionViewModel        item)
        {
            try
            {
                var model = unitOfWork.TransactionDetailsRepo.Find(m => m.Id == item.TransactionDetailId);
                ViewBag.TransactionId = item?.TransactionId;
                ViewBag.PermiteeId = item?.PermiteeId;
                ViewBag.Items = unitOfWork.ItemsRepo.Find(m => m.Id == item.ItemId);

                return PartialView(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return PartialView();
        }


        [ValidateInput(false)]
        public ActionResult TransactionDetailsGridViewPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            TransactionViewModel        item)
        {
            var model = unitOfWork.TransactionDetailsRepo.Get(m => m.TransactionId == item.TransactionId,
                includeProperties: "Items");
            ViewBag.TransactionId = item.TransactionId;
            ViewBag.PermiteeId = item.PermiteeId;
            ViewBag.Items = unitOfWork.ItemsRepo.Find(m => m.Id == item.ItemId);
            ViewBag.Transactions = unitOfWork.TransactionsRepo.Find(m => m.Id == item.TransactionId);
            return PartialView("_TransactionDetailsGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "Add Transaction Details")]
        public ActionResult TransactionDetailsGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))]
            Models.TransactionDetails item, [ModelBinder(typeof(DevExpressEditorsBinder))]
            TransactionViewModel        vm)
        {
            //var model = new object[0];
            ViewBag.PermiteeId = vm.PermiteeId;
            if (ModelState.IsValid)
            {
                try
                {
                    item.EntryBy = UserId;
                    item.LastEditedBy = UserId;
                    unitOfWork.TransactionDetailsRepo.Insert(item);
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

            var model = unitOfWork.TransactionDetailsRepo.Get(m => m.TransactionId == vm.TransactionId,
                includeProperties: "Items");
            ViewBag.TransactionId = vm.TransactionId;
            return PartialView("_TransactionDetailsGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "Update Transaction Details")]
        public ActionResult TransactionDetailsGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))]
            Models.TransactionDetails item, [ModelBinder(typeof(DevExpressEditorsBinder))]
            TransactionViewModel        vm)
        {
            //var model = new object[0];
            ViewBag.PermiteeId = vm.PermiteeId;
            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.TransactionDetailsRepo.TrackModifiedEntities(m => m.Id == item.Id, item);
                    unitOfWork.TransactionDetailsRepo.Update(item);
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

            var model = unitOfWork.TransactionDetailsRepo.Get(m => m.TransactionId == vm.TransactionId,
                includeProperties: "Items");
            ViewBag.TransactionId = vm.TransactionId;
            return PartialView("_TransactionDetailsGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "Delete Transaction Details")]
        public ActionResult TransactionDetailsGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]
            int? Id, [ModelBinder(typeof(DevExpressEditorsBinder))]
            TransactionViewModel        vm)
        {
            ViewBag.PermiteeId = vm.PermiteeId;
            if (Id >= 0)
            {
                try
                {
                    unitOfWork.TransactionDetailsRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }

            var model = unitOfWork.TransactionDetailsRepo.Get(m => m.TransactionId == vm.TransactionId,
                includeProperties: "Items");
            ViewBag.TransactionId = vm.TransactionId;
            return PartialView("_TransactionDetailsGridViewPartial", model);
        }


        #endregion

        public ActionResult PopupAddEditPermiteePartial()
        {
            return PartialView();
        }




        #region Transaction Vehicles

        public ActionResult AddEditTransactionVehiclesPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            TransactionViewModel item)
        {
            var model = unitOfWork.TransactionVehiclesRepo.Find(m => m.Id == item.TransactionVehicleId);
            ViewBag.TransactionId = item.TransactionId;
            ViewBag.PermiteeId = item.PermiteeId;
            ViewBag.Vehicle = unitOfWork.VehiclesRepo.Find(m => m.Id == item.VehicleId);
            return PartialView(model);
        }

        [ValidateInput(false)]
        public ActionResult VehicleGridViewPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            TransactionViewModel item)
        {
            var model = unitOfWork.TransactionVehiclesRepo.Get(m => m.TransactionId == item.TransactionId,
                includeProperties: "Vehicles");
            ViewBag.TransactionId = item.TransactionId;
            ViewBag.PermiteeId = item.PermiteeId;
            ViewBag.Vehicle = unitOfWork.VehiclesRepo.Find(m => m.Id == item.VehicleId);
            ViewBag.Transactions = unitOfWork.TransactionsRepo.Find(m => m.Id == item.TransactionId);
            return PartialView("_VehicleGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "Add Transaction Vehicles")]
        public ActionResult VehicleGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))]
            Models.TransactionVehicles item, [ModelBinder(typeof(DevExpressEditorsBinder))]
            TransactionViewModel vm)
        {
            //var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    item.EntryBy = UserId;
                    item.EntryModifiedBy = UserId;
                    unitOfWork.TransactionVehiclesRepo.Insert(item);
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

            var model = unitOfWork.TransactionVehiclesRepo.Get(m => m.TransactionId == vm.TransactionId,
                includeProperties: "Vehicles");
            ViewBag.TransactionId = vm.TransactionId;
            ViewBag.PermiteeId = vm.PermiteeId;
            return PartialView("_VehicleGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "Update Transaction Vehicles")]
        public ActionResult VehicleGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))]
            Models.TransactionVehicles item, [ModelBinder(typeof(DevExpressEditorsBinder))]
          TransactionViewModel vm)
        {
            //  var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.TransactionVehiclesRepo.TrackModifiedEntities(m => m.Id == item.Id, item);
                    item.EntryModifiedBy = UserId;
                    unitOfWork.TransactionVehiclesRepo.Update(item);
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

            var model = unitOfWork.TransactionVehiclesRepo.Get(m => m.TransactionId == vm.TransactionId,
                includeProperties: "Vehicles");
            ViewBag.TransactionId = vm.TransactionId;
            ViewBag.PermiteeId = vm.PermiteeId;
            return PartialView("_VehicleGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "Delete Transaction Vehicles")]
        public ActionResult VehicleGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]
            int? Id, [ModelBinder(typeof(DevExpressEditorsBinder))]
            TransactionViewModel item)
        {
            // var model = new object[0];
            if (Id >= 0)
            {
                try
                {
                    unitOfWork.TransactionVehiclesRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }

            var model = unitOfWork.TransactionVehiclesRepo.Get(m => m.TransactionId == item.TransactionId,
                includeProperties: "Vehicles");
            ViewBag.TransactionId = item.TransactionId;
            ViewBag.PermiteeId = item.PermiteeId;
            return PartialView("_VehicleGridViewPartial", model);
        }


        #endregion

        #region Transaction Facilities

        public PartialViewResult AddEditTransactionFacilitiesPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
         TransactionViewModel item)
        {
            var model = unitOfWork.TransactionFacilitiesRepo.Find(m => m.Id == item.TransactionFacilitiesId,
                includeProperties: "Facilities");
            ViewBag.TransactionId = item.TransactionId;
            ViewBag.PermiteeId = item.PermiteeId;
            ViewBag.Facilities = unitOfWork.FacilitiesRepo.Get();
            ViewBag.facility = unitOfWork.FacilitiesRepo.Find(m => m.Id == item.FacilitiesId);
            return PartialView(model);
        }

        [ValidateInput(false)]
        public ActionResult FacilitiesGridViewPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            TransactionViewModel item)
        {
            var model = unitOfWork.TransactionFacilitiesRepo.Get(m => m.TransactionId == item.TransactionId,
                includeProperties: "Facilities");
            ViewBag.TransactionId = item.TransactionId;
            ViewBag.PermiteeId = item.PermiteeId;
            ViewBag.Transactions = unitOfWork.TransactionsRepo.Find(m => m.Id == item.TransactionId);
            return PartialView("_FacilitiesGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult FacilitiesGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))]
            Models.TransactionFacilities item, [ModelBinder(typeof(DevExpressEditorsBinder))]
            TransactionViewModel vm)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    item.EntryBy = UserId;
                    item.EntryModifiedBy = UserId;
                    unitOfWork.TransactionFacilitiesRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";

            var model = unitOfWork.TransactionFacilitiesRepo.Get(m => m.TransactionId == vm.TransactionId,
                includeProperties: "Facilities");
            ViewBag.TransactionId = vm.TransactionId;
            ViewBag.PermiteeId = vm.PermiteeId;
            return PartialView("_FacilitiesGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult FacilitiesGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))]
            Models.TransactionFacilities item, [ModelBinder(typeof(DevExpressEditorsBinder))]
            TransactionViewModel vm)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.TransactionFacilitiesRepo.TrackModifiedEntities(m => m.Id == item.Id, item);
                    item.EntryModifiedBy = UserId;
                    unitOfWork.TransactionFacilitiesRepo.Update(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";

            var model = unitOfWork.TransactionFacilitiesRepo.Get(m => m.TransactionId == vm.TransactionId,
                includeProperties: "Facilities");
            ViewBag.TransactionId = vm.TransactionId;
            ViewBag.PermiteeId = vm.PermiteeId;
            return PartialView("_FacilitiesGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult FacilitiesGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]
            int Id, [ModelBinder(typeof(DevExpressEditorsBinder))]
            TransactionViewModel item)
        {

            if (Id >= 0)
            {
                try
                {
                    unitOfWork.TransactionFacilitiesRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }

            var model = unitOfWork.TransactionFacilitiesRepo.Get(m => m.TransactionId == item.TransactionId,
                includeProperties: "Facilities");
            ViewBag.TransactionId = item.TransactionId;
            ViewBag.PermiteeId = item.PermiteeId;
            return PartialView("_FacilitiesGridViewPartial", model);
        }


        #endregion

        #region Transaction Sag

        public ActionResult AddEditTransactionSagPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            TransactionViewModel viewModel)
        {
            var model = unitOfWork.TransactionSagsRepo.Find(m => m.Id == viewModel.TransactionSagId, includeProperties: "Sags");
            ViewBag.TransactionId = viewModel.TransactionId;
            ViewBag.PermiteeId = viewModel.PermiteeId;
            ViewBag.Sags = unitOfWork.SagsRepo.Get();
            ViewBag.Sag = unitOfWork.SagsRepo.Find(m => m.Id == viewModel.SagId);
            return PartialView(model);
        }

        [ValidateInput(false)]
        public ActionResult TransactionSagGridViewPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            TransactionViewModel viewModel)
        {
            var model = unitOfWork.TransactionSagsRepo.Get(m => m.TransactionId == viewModel.TransactionId,
                includeProperties: "Sags");
            ViewBag.TransactionId = viewModel.TransactionId;
            ViewBag.PermiteeId = viewModel.PermiteeId;
            ViewBag.Transactions = unitOfWork.TransactionsRepo.Find(m => m.Id == viewModel.TransactionId);
            return PartialView("_TransactionSagGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TransactionSagGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))]
            Models.TransactionSags item, [ModelBinder(typeof(DevExpressEditorsBinder))]
            TransactionViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    unitOfWork.TransactionSagsRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";

            var model = unitOfWork.TransactionSagsRepo.Get(m => m.TransactionId == viewModel.TransactionId,
                includeProperties: "Sags");
            ViewBag.TransactionId = viewModel.TransactionId;
            ViewBag.PermiteeId = viewModel.PermiteeId;
            return PartialView("_TransactionSagGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TransactionSagGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))]
            Models.TransactionSags item, [ModelBinder(typeof(DevExpressEditorsBinder))]
            TransactionViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.TransactionSagsRepo.TrackModifiedEntities(m => m.Id == item.Id, item);
                    // Insert here a code to update the item in your model
                    unitOfWork.TransactionSagsRepo.Update(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";

            var model = unitOfWork.TransactionSagsRepo.Get(m => m.TransactionId == viewModel.TransactionId,
                includeProperties: "Sags");
            ViewBag.TransactionId = viewModel.TransactionId;
            ViewBag.PermiteeId = viewModel.PermiteeId;
            return PartialView("_TransactionSagGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TransactionSagGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]
            int? Id, [ModelBinder(typeof(DevExpressEditorsBinder))]
          TransactionViewModel viewModel)
        {

            if (Id >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    unitOfWork.TransactionSagsRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }

            var model = unitOfWork.TransactionSagsRepo.Get(m => m.TransactionId == viewModel.TransactionId,
                includeProperties: "Sags");
            ViewBag.TransactionId = viewModel.TransactionId;
            ViewBag.PermiteeId = viewModel.PermiteeId;
            return PartialView("_TransactionSagGridViewPartial", model);
        }



        #endregion

        public ActionResult TransactionCallbackPanel([ModelBinder(typeof(DevExpressEditorsBinder))]
           TransactionViewModel viewModel)
        {
            ViewBag.transactionId = viewModel.TransactionId;
            ViewBag.PermiteeId = viewModel.PermiteeId;

            var model = unitOfWork.TransactionsRepo.Get();
            return PartialView();
        }

        [OnUserAuthorization(ActionName = "Print Billing Statement")]
        public ActionResult ReportViewerPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            string transactionId)
        {
            if (transactionId != null)
            {
                var transactions = unitOfWork.TransactionsRepo.Fetch(m => m.Id == transactionId);
                var transaction = transactions.FirstOrDefault();
                if (transaction?.IsPrinted != true)
                {
                    transaction.IsPrinted = true;
                    unitOfWork.Save();
                }

                var billingStatement = new List<BillingStatementViewModel>()
                {
                    new BillingStatementViewModel() {TransactionId = transactionId}
                };
                var rpt = new BillingStatementReport() { DataSource = billingStatement };
                var item = billingStatement?.FirstOrDefault();
                if (item != null)
                {
                    if (!item.TransactionFacilities.Any())
                        rpt.groupfacilities.Visible = false;
                    if (!item.TransactionVehicles.Any())
                        rpt.groupVehicles.Visible = false;
                    if (!item.TaxOnExcessSAGVolume.Any())
                        rpt.groupExcessSag.Visible = false;
                    if (item.Transactions.TransactionTypes.TransactionType.Contains("Additional"))
                        rpt.groupNotary.Visible = false;
                    if (!item.GovernorsBusinessPermitFee.Any())
                        rpt.groupGovBusiness.Visible = false;

                    if (!item.TransactionSags.Any())
                    {
                        rpt.groupTransactionSag.Visible = false;
                    }
                    if (!item.GovernorsAccreditationFees.Any())
                    {
                        rpt.detailAccre.Visible = false;
                        rpt.footerAccre.Visible = false;
                    }

                    if (transaction.Signatory.Equals("Governor", StringComparison.OrdinalIgnoreCase))
                        rpt.forGovernorSignatories.Visible = true;
                    else
                        rpt.groupForandInBehalf.Visible = true;

                    if (transaction.TransactionTypes.TransactionType.ToLower() == "payment of pow")
                    {
                        rpt.lblQuarrySites.Text = $@"**ALL COMPUTED CHARGE TO {string.Join(",", transaction.QuarriesInTransactions.Select(x => x.QuarrySitesDistribution))}**";
                    }
                    else
                    {
                        rpt.lblQuarrySites.Text = transaction.Permitees._QuarySites;//[Transactions].[Permitees].[_QuarySites]
                    }


                }

                return PartialView(rpt);
            }

            return PartialView();
        }


        public JsonResult Transactions([ModelBinder(typeof(DevExpressEditorsBinder))]
            string transactionId)
        {
            var model = new
            {
                DetailSubTotal = unitOfWork.TransactionDetailsRepo.Get(m => m.TransactionId == transactionId)
                    .Sum(m => m.TotalCost),
                VehicleSubTotal = unitOfWork.TransactionVehiclesRepo.Fetch(m => m.TransactionId == transactionId)
                    .Sum(m => m.Cost),
                FacilitiesSubTotal = unitOfWork.TransactionFacilitiesRepo.Fetch(m => m.TransactionId == transactionId)
                    .Sum(m => m.Cost),
                SagSubTotal = unitOfWork.TransactionSagsRepo.Get(m => m.TransactionId == transactionId)
                    .Sum(m => m.TotalCost),
                PenaltiesSubTotal = unitOfWork.TransactionPenaltiesRepo.Get(m => m.TransactionId == transactionId).Sum(m => m.Amount)
            };

            return Json(model, JsonRequestBehavior.AllowGet);
        }




        #region Penalties

        public ActionResult AddEditTransactionPenalties([ModelBinder(typeof(DevExpressEditorsBinder))]int? transactionPenaltiesId, [ModelBinder(typeof(DevExpressEditorsBinder))]string transactionId)
        {
            ViewBag.TransactionPenaltiesId = transactionPenaltiesId;
            ViewBag.TransactionId = transactionId;
            var model = unitOfWork.TransactionPenaltiesRepo.Find(m => m.Id == transactionPenaltiesId);
            return PartialView(model);
        }

        [ValidateInput(false)]
        public ActionResult PenaltiesGridViewPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            string transactionId)
        {
            var model = unitOfWork.TransactionPenaltiesRepo.Get(m => m.TransactionId == transactionId);
            ViewBag.TransactionId = transactionId;
            return PartialView("_PenaltiesGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "add penalties")]
        public ActionResult PenaltiesGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.TransactionPenalties item, [ModelBinder(typeof(DevExpressEditorsBinder))]
            string transactionId)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    item.TransactionId = transactionId;
                    unitOfWork.TransactionPenaltiesRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.TransactionPenaltiesRepo.Get(m => m.TransactionId == transactionId);
            ViewBag.TransactionId = transactionId;
            return PartialView("_PenaltiesGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "update penalties")]
        public ActionResult PenaltiesGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.TransactionPenalties item, [ModelBinder(typeof(DevExpressEditorsBinder))]
            string transactionId)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    item.TransactionId = transactionId;
                    unitOfWork.TransactionPenaltiesRepo.Update(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.TransactionPenaltiesRepo.Get(m => m.TransactionId == transactionId);
            ViewBag.TransactionId = transactionId;
            return PartialView("_PenaltiesGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "delete penalties")]
        public ActionResult PenaltiesGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]int? Id, [ModelBinder(typeof(DevExpressEditorsBinder))]
            string transactionId)
        {

            if (Id >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    unitOfWork.TransactionPenaltiesRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.TransactionPenaltiesRepo.Get(m => m.TransactionId == transactionId);
            ViewBag.TransactionId = transactionId;
            return PartialView("_PenaltiesGridViewPartial", model);
        }


        #endregion

        #region Quarries
        [ValidateInput(false)]
        public ActionResult TransactionQuarriesGridViewPartial([ModelBinder(typeof(DevExpressEditorsBinder))]TransactionViewModel viewModel)
        {
            var model = unitOfWork.QuarriesInTransactionsRepo.Get(m => m.TransactionId == viewModel.TransactionId);
            ViewBag.TransactionId = viewModel.TransactionId;
            ViewBag.PermiteeId = viewModel.PermiteeId;

            return PartialView("_TransactionQuarriesGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "add transaction quarries")]
        public ActionResult TransactionQuarriesGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.QuarriesInTransactions item, [ModelBinder(typeof(DevExpressEditorsBinder))] TransactionViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model

                    unitOfWork.QuarriesInTransactionsRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.QuarriesInTransactionsRepo.Get(m => m.TransactionId == viewModel.TransactionId, includeProperties: "Sags,Quarries");
            ViewBag.TransactionId = viewModel.TransactionId;
            ViewBag.PermiteeId = viewModel.PermiteeId;
            return PartialView("_TransactionQuarriesGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "update transaction quarries")]
        public ActionResult TransactionQuarriesGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.QuarriesInTransactions item, [ModelBinder(typeof(DevExpressEditorsBinder))] TransactionViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    unitOfWork.QuarriesInTransactionsRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.QuarriesInTransactionsRepo.Get(m => m.TransactionId == viewModel.TransactionId, includeProperties: "Sags,Quarries");
            ViewBag.TransactionId = viewModel.TransactionId;
            ViewBag.PermiteeId = viewModel.PermiteeId;
            return PartialView("_TransactionQuarriesGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "Delete transaction Quarries")]
        public ActionResult TransactionQuarriesGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]System.Int32 Id, [ModelBinder(typeof(DevExpressEditorsBinder))] TransactionViewModel viewModel)
        {

            if (Id >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    unitOfWork.QuarriesInTransactionsRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.QuarriesInTransactionsRepo.Get(m => m.TransactionId == viewModel.TransactionId, includeProperties: "Sags,Quarries");
            ViewBag.TransactionId = viewModel.TransactionId;
            ViewBag.PermiteeId = viewModel.PermiteeId;
            return PartialView("_TransactionQuarriesGridViewPartial", model);
        }
        public ActionResult AddEditTransactionQuarriesPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            TransactionViewModel viewModel)
        {
            var model = unitOfWork.QuarriesInTransactionsRepo.Find(m => m.Id == viewModel.QuarriesInTransactionId, includeProperties: "Sags,Quarries");
            ViewBag.TransactionId = viewModel.TransactionId;
            ViewBag.PermiteeId = viewModel.PermiteeId;
            //ViewBag.Sags = unitOfWork.SagsRepo.Get();
            //ViewBag.Sag = unitOfWork.SagsRepo.Find(m => m.Id == viewModel.SagId);
            //ViewBag.Quarries = unitOfWork.QuarriesRepo.Get();
            //ViewBag.Quarry = unitOfWork.QuarriesRepo.Find(m => m.Id == viewModel.SagId);
            return PartialView(model);
        }


        #endregion

    }


}