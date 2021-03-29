using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Models;
using Models.Repository;
using Helpers;
namespace Quary.New.Controllers
{

    [RoutePrefix("file-management")]
    public class FileManagementController : Controller
    {



        private UnitOfWork unitOfWork = new UnitOfWork();
        private FilemanagementImportHelper importHelper;
        // GET: FileManagement
        public ActionResult Index()
        {
            return View();
        }

       
        public ActionResult FileManager()
        {
            return View();
        }
        [Route("transaction-items")]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "TransactionItems")]
        public ActionResult TransactionItems()
        {
            return View();
        }
        [Route("permitees")]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "Permitees")]
        public ActionResult Permitees()
        {
            return View();
        }
        [Route("permitee-types")]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "PermiteeTypes")]
        public ActionResult PermiteeTypes()
        {
            return View();
        }
        [Route("vehicles")]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "Vehicles")]
        public ActionResult Vehicles()
        {
            return View();
        }
        [Route("vehicle-types")]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "VehicleTypes")]
        public ActionResult VehicleTypes()
        {
            return View();
        }
        [Route("facilities")]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "Facilities")]
        public ActionResult Facilities()
        {
            return View();
        }
        [Route("quarries")]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "Quarries")]
        public ActionResult Quarries()
        {
            return View();
        }
        [Route("categories")]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "Categories")]
        public ActionResult Categories()
        {
            return View();
        }
        [Route("unit-of-measurement")]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "UnitOfMeasurement")]
        public ActionResult UnitOfMeasurement()
        {
            return View();
        }

        public ActionResult ImportPermittees()
        {
            return View();
        }
        [Route("program-of-works")]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "ProgramOfWorks")]
        public ActionResult ProgramOfWorks()
        {
            return View();
        }
        [Route("barangays")]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "Barangays")]
        public ActionResult Barangays()
        {
            return View();
        }

        #region transaction-items
        [HttpPost]
        public ActionResult AddEditTransactionItemPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? itemId)
        {
            var item = unitOfWork.ItemsRepo.Find(m => m.Id == itemId);
            return PartialView(item);
        }
        [ValidateInput(false)]
        public ActionResult TransactionItemsGridViewPartial()
        {
            var model = unitOfWork.ItemsRepo.Get();
            return PartialView("_TransactionItemsGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "Add Transaction Items")]
        public ActionResult TransactionItemsGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Items item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    unitOfWork.ItemsRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;

                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                ViewData["Model"] = item;

            }

            var model = unitOfWork.ItemsRepo.Get();
            return PartialView("_TransactionItemsGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "Update Transaction Items")]
        public ActionResult TransactionItemsGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Items item, [ModelBinder(typeof(DevExpressEditorsBinder))]Items oldModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.ItemsRepo.TrackModifiedEntities(m => m.Id == item.Id, item);
                    unitOfWork.ItemsRepo.Update(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.ItemsRepo.Get();
            return PartialView("_TransactionItemsGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "Delete Transaction Items")]
        public ActionResult TransactionItemsGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]int? Id)
        {

            if (Id >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    unitOfWork.ItemsRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.ItemsRepo.Get();
            return PartialView("_TransactionItemsGridViewPartial", model);
        }


        #endregion

        #region permitee-types
        [HttpPost]
        public ActionResult AddEditPermiteeTypePartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? permiteeTypeId)
        {
            var model = unitOfWork.PermiteeTypesRepo.Find(m => m.Id == permiteeTypeId);
            return PartialView(model);
        }
        [ValidateInput(false)]
        public ActionResult PermiteeTypeGridViewPartial()
        {
            var model = unitOfWork.PermiteeTypesRepo.Get();
            return PartialView("_PermiteeTypeGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "Add Permitee Type")]
        public ActionResult PermiteeTypeGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.PermiteeTypes item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.PermiteeTypesRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                ViewData["Model"] = item;
            }

            var model = unitOfWork.PermiteeTypesRepo.Get();
            return PartialView("_PermiteeTypeGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "Update Permitee Type")]
        public ActionResult PermiteeTypeGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.PermiteeTypes item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.PermiteeTypesRepo.Update(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.PermiteeTypesRepo.Get();
            return PartialView("_PermiteeTypeGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "Delete Permitee Type")]
        public ActionResult PermiteeTypeGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]int? Id)
        {

            if (Id >= 0)
            {
                try
                {
                    unitOfWork.PermiteeTypesRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.PermiteeTypesRepo.Get();
            return PartialView("_PermiteeTypeGridViewPartial", model);
        }

        #endregion

        #region quarries

        public ActionResult TokenBoxTownsPartials([ModelBinder(typeof(DevExpressEditorsBinder))]int? quarryId)
        {
            var res = unitOfWork.QuarriesRepo.Find(m => m.Id == quarryId)?.Barangays;
            var towns = unitOfWork.TownsRepo.Get();
            ViewBag.Towns = towns;
            return PartialView(res);
        }
        public ActionResult TokenBoxBarangayPartials([ModelBinder(typeof(DevExpressEditorsBinder))]int? quarryId)
        {
            var res = unitOfWork.QuarriesRepo.Find(m => m.Id == quarryId)?.Barangays;
            var barangays = unitOfWork.BarangaysRepo.Get();
            ViewBag.Barangays = barangays;
            return PartialView(res);
        }

        [ValidateInput(false)]
        public ActionResult QuarriesGridViewPartial()
        {
            var model = unitOfWork.QuarriesRepo.Get();
            return PartialView("_QuarriesGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "add quarries")]
        public ActionResult QuarriesGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Quarries item)
        {
            if (unitOfWork.QuarriesRepo.Fetch(m => m.QuarryName == item.QuarryName).Any())
                ModelState.AddModelError("QuarryName", @"Quarry Name is Already existed");

            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var i in item.BarangayIds)
                    {
                        var barangayIds = Convert.ToInt32(i);
                        item.Barangays.Add(unitOfWork.BarangaysRepo.Find(m => m.Id == barangayIds));
                    }
                    item.LastEditedBy = User.Identity.GetUserId();
                    item.EntryBy = User.Identity.GetUserId();
                    unitOfWork.QuarriesRepo.Insert(item);

                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                ViewData["Model"] = item;
            }

            var model = unitOfWork.QuarriesRepo.Get();
            return PartialView("_QuarriesGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "update quarries")]
        public ActionResult QuarriesGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Quarries item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    //unitOfWork.TrackEntities(unitOfWork.QuarriesRepo.Find(m => m.Id == item.Id), item, "Quarries");
                    unitOfWork.QuarriesRepo.TrackModifiedEntities(m => m.Id == item.Id, item);
                    var quarry = unitOfWork.QuarriesRepo.Find(m => m.Id == item.Id, includeProperties: "Barangays");
                    //       ;
                    quarry.LastEditedBy = User.Identity.GetUserId();
                    quarry.QuarryName = item.QuarryName;
                    quarry.Barangays.Clear();
                    foreach (var i in item.BarangayIds)
                    {
                        var barangayIds = Convert.ToInt32(i);
                        quarry.Barangays.Add(unitOfWork.BarangaysRepo.Find(m => m.Id == barangayIds));
                    }


                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                ViewData["Model"] = item;
            }
            var model = unitOfWork.QuarriesRepo.Get();
            return PartialView("_QuarriesGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "delete quarries")]
        public ActionResult QuarriesGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]int? Id)
        {

            if (Id >= 0)
            {
                try
                {
                    // unitOfWork.QuarriesRepo.TrackDeletedEntities(m => m.Id == Id);
                    unitOfWork.QuarriesRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.QuarriesRepo.Get();
            return PartialView("_QuarriesGridViewPartial", model);
        }

        public ActionResult AddEditQuarriesPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? quarriesId)
        {
            var model = unitOfWork.QuarriesRepo.Find(m => m.Id == quarriesId);

            return PartialView(model);
        }

        #endregion

        #region permitees

        public ActionResult DownloadTemplates(string template)
        {
            string fileName = "";
            if (string.Equals(template, "permittee", StringComparison.CurrentCultureIgnoreCase))
                fileName = "Import Permittee Template.xlsx";
            else if (string.Equals(template, "quarry", StringComparison.CurrentCultureIgnoreCase))
                fileName = "Import Quarry Template.xlsx";
            else if (string.Equals(template, "vehicle", StringComparison.CurrentCultureIgnoreCase))
                fileName = "Import Vehicle Template.xlsx";
            else if (string.Equals(template, "production", StringComparison.CurrentCultureIgnoreCase))
                fileName = "Import Production Template.xlsx";
            return File(Server.MapPath("~/templates/" + fileName), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        [ValidateInput(false)]
        public ActionResult PermiteeGridViewPartial()
        {
            var model = unitOfWork.PermiteesRepo.Get();



            return PartialView("_PermiteeGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "Add Permitee")]
        public ActionResult PermiteeGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Permitees item)
        {
            if (unitOfWork.PermiteesRepo.Fetch(m => m.AccreditationNumber == item.AccreditationNumber).Any())
                ModelState.AddModelError("AccreditationNumber", @"Accreditation Number already exist");
            ViewBag.QuarrySites = item.QuarrySites;
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    foreach (var i in item.QuarrySites)
                    {
                        var id = Convert.ToInt32(i);
                        item.Quarries.Add(unitOfWork.QuarriesRepo.Find(m => m.Id == id));
                    }
                    item.EntryBy = User.Identity.GetUserId();
                    item.LastEditedBy = User.Identity.GetUserId();


                    item.AccreditationNumber = item.AccreditationNumber ?? new PermiteeHelper()?.AccreditationNumber;
                    unitOfWork.PermiteesRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                ViewData["Model"] = item;
            }

            var model = unitOfWork.PermiteesRepo.Get();
            return PartialView("_PermiteeGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "Update Permitee")]
        public ActionResult PermiteeGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Permitees item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.PermiteesRepo.TrackModifiedEntities(m => m.Id == item.Id, item);
                    unitOfWork.PermiteesRepo.Update(item);
                    unitOfWork.Save();
                    var permitees = unitOfWork.PermiteesRepo.Find(m => m.Id == item.Id, includeProperties: "Quarries");
                    permitees.Quarries.Clear();
                    foreach (var i in item.QuarrySites)
                    {
                        var id = Convert.ToInt32(i);
                        permitees.Quarries.Add(unitOfWork.QuarriesRepo.Find(m => m.Id == id));
                    }
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                ViewData["Model"] = item;
            }
            var model = unitOfWork.PermiteesRepo.Get(includeProperties: "Quarries");
            return PartialView("_PermiteeGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "Delete Permitee")]
        public ActionResult PermiteeGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]int? Id)
        {

            if (Id >= 0)
            {
                try
                {
                    unitOfWork.PermiteesRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.PermiteesRepo.Get();
            return PartialView("_PermiteeGridViewPartial", model);
        }
        [HttpPost]
        public ActionResult AddEditPermiteePartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? permiteeId)
        {
            var model = unitOfWork.PermiteesRepo.Find(m => m.Id == permiteeId);
            return PartialView(model);
        }

        public PartialViewResult TokenBoxQuarriesInPermiteePartial([ModelBinder(typeof(DevExpressEditorsBinder))] int? permiteeId, [ModelBinder(typeof(DevExpressEditorsBinder))]List<int> quarrySites)
        {
            ViewBag.PermiteesQuarry = unitOfWork.QuarriesRepo.Fetch(m => m.Permitees.Any(x => x.Id == permiteeId)).ToList();
            var model = unitOfWork.QuarriesRepo.Get();
            ViewBag.QuarrySites = quarrySites;
            return PartialView(model);
        }


        #endregion

        #region vehicle-types

        [ValidateInput(false)]
        public ActionResult VehicleTypesGridViewPartial()
        {
            var model = unitOfWork.VehicleTypesRepo.Get();
            return PartialView("_VehicleTypesGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "Add Vehicle Types")]
        public ActionResult VehicleTypesGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.VehicleTypes item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.VehicleTypesRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                ViewData["Model"] = item;
            }
            var model = unitOfWork.VehicleTypesRepo.Get();
            return PartialView("_VehicleTypesGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "Update Vehicle Types")]
        public ActionResult VehicleTypesGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.VehicleTypes item)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    unitOfWork.VehicleTypesRepo.Update(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                ViewData["Model"] = item;
            }
            var model = unitOfWork.VehicleTypesRepo.Get();
            return PartialView("_VehicleTypesGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "Delete Vehicle Types")]
        public ActionResult VehicleTypesGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]int? Id)
        {

            if (Id >= 0)
            {
                try
                {
                    unitOfWork.VehicleTypesRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.VehicleTypesRepo.Get();
            return PartialView("_VehicleTypesGridViewPartial", model);
        }

        public PartialViewResult AddEditVehicleTypePartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? vehicleTypeId)
        {
            var model = unitOfWork.VehicleTypesRepo.Find(m => m.Id == vehicleTypeId);
            return PartialView(model);
        }


        #endregion

        #region vehicles

        [ValidateInput(false)]
        public ActionResult VehiclesGridViewPartial()
        {
            var model = unitOfWork.VehiclesRepo.Get().ToList();
            return PartialView("_VehiclesGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "Add Vehicles")]
        public ActionResult VehiclesGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Vehicles item)
        {


            if (unitOfWork.VehiclesRepo.Fetch(m => m.PlateNo == item.PlateNo).Any())
                ModelState.AddModelError("PlateNo", "Plate no. is already existed");


            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.VehiclesRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                ViewData["Model"] = item;
            }
            var model = unitOfWork.VehiclesRepo.Get(includeProperties: "VehicleTypes,Permitees");
            return PartialView("_VehiclesGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "Update Vehicles")]
        public ActionResult VehiclesGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Vehicles item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.VehiclesRepo.TrackModifiedEntities(m => m.Id == item.Id, item);
                    unitOfWork.VehiclesRepo.Update(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                ViewData["Model"] = item;
            }
            var model = unitOfWork.VehiclesRepo.Get(includeProperties: "VehicleTypes,Permitees");
            return PartialView("_VehiclesGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "Delete Vehicles")]
        public ActionResult VehiclesGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))] int? Id)
        {

            if (Id >= 0)
            {
                try
                {
                    unitOfWork.VehiclesRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.VehiclesRepo.Get(includeProperties: "VehicleTypes,Permitees");
            return PartialView("_VehiclesGridViewPartial", model);
        }

        public PartialViewResult AddEditVehiclePartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? vehicleId)
        {
            var model = unitOfWork.VehiclesRepo.Find(m => m.Id == vehicleId);
            return PartialView(model);
        }

        #endregion

        #region facilities

        [ValidateInput(false)]
        public ActionResult FacilitiesGridViewPartial()
        {
            var model = unitOfWork.FacilitiesRepo.Get();
            return PartialView("_FacilitiesGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "Add Facilities")]
        public ActionResult FacilitiesGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Facilities item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    unitOfWork.FacilitiesRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                ViewData["Model"] = item;
            }
            var model = unitOfWork.FacilitiesRepo.Get();
            return PartialView("_FacilitiesGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "Update Facilities")]
        public ActionResult FacilitiesGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Facilities item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    unitOfWork.FacilitiesRepo.Update(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                ViewData["Model"] = item;
            }
            var model = unitOfWork.FacilitiesRepo.Get();
            return PartialView("_FacilitiesGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ControllerName = "FileManagement", ActionName = "Delete Facilities")]
        public ActionResult FacilitiesGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]int? Id)
        {

            if (Id >= 0)
            {
                try
                {
                    unitOfWork.FacilitiesRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.FacilitiesRepo.Get();
            return PartialView("_FacilitiesGridViewPartial", model);
        }
        [HttpPost]
        public PartialViewResult AddEditFacilitiesPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? facilitiesId)
        {
            var model = unitOfWork.FacilitiesRepo.Find(m => m.Id == facilitiesId);
            return PartialView(model);
        }

        #endregion

        #region Categories


        [ValidateInput(false)]
        public ActionResult CategoryGridViewPartial()
        {
            var model = unitOfWork.CategoriesRepo.Get();
            return PartialView("_CategoryGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "Add Category")]
        public ActionResult CategoryGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Categories item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.CategoriesRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.CategoriesRepo.Get();
            return PartialView("_CategoryGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "Update Category")]
        public ActionResult CategoryGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Categories item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.CategoriesRepo.Update(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.CategoriesRepo.Get();
            return PartialView("_CategoryGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "Delete Category")]
        public ActionResult CategoryGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]int? Id)
        {

            if (Id >= 0)
            {
                try
                {
                    unitOfWork.CategoriesRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.CategoriesRepo.Get();
            return PartialView("_CategoryGridViewPartial", model);
        }


        #endregion

        #region Unit of Measurement
        [ValidateInput(false)]
        public ActionResult UnitMeasurementGridViewPartial()
        {
            var model = unitOfWork.UnitMeasurementsRepo.Get();
            return PartialView("_UnitMeasurementGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "Add Unit Measurement")]
        public ActionResult UnitMeasurementGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.UnitMeasurements item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    unitOfWork.UnitMeasurementsRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.UnitMeasurementsRepo.Get();
            return PartialView("_UnitMeasurementGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "Update Unit Measurement")]
        public ActionResult UnitMeasurementGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.UnitMeasurements item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.UnitMeasurementsRepo.TrackModifiedEntities(m => m.Id == item.Id, item);
                    // Insert here a code to update the item in your model
                    unitOfWork.UnitMeasurementsRepo.Update(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.UnitMeasurementsRepo.Get();
            return PartialView("_UnitMeasurementGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "Delete Unit Measurement")]
        public ActionResult UnitMeasurementGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))] int? Id)
        {

            if (Id >= 0)
            {
                try
                {
                    unitOfWork.UnitMeasurementsRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.UnitMeasurementsRepo.Get();
            return PartialView("_UnitMeasurementGridViewPartial", model);
        }

        public ActionResult AddEditUnitMeasurementPartial([ModelBinder(typeof(DevExpressEditorsBinder))] int? unitMeasurementId)
        {
            var model = unitOfWork.UnitMeasurementsRepo.Find(m => m.Id == unitMeasurementId);
            return PartialView(model);
        }
        #endregion

        #region Sags

        public ActionResult AddEditSagsPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            int? sagId)
        {
            var model = unitOfWork.SagsRepo.Find(m => m.Id == sagId, includeProperties: "UnitMeasurements");
            return PartialView(model);
        }
        [OnUserAuthorization(ActionName = "Sags")]
        public ActionResult Sags()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult SagsGridViewGridViewPartial()
        {
            var model = unitOfWork.SagsRepo.Get(includeProperties: "UnitMeasurements");
            return PartialView("_SagsGridViewGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "Add Sags")]
        public ActionResult SagsGridViewGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Sags item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.SagsRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.SagsRepo.Get(includeProperties: "UnitMeasurements");
            return PartialView("_SagsGridViewGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "Update Sags")]
        public ActionResult SagsGridViewGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Sags item)
        {
            //var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.SagsRepo.TrackModifiedEntities(m => m.Id == item.Id, item);
                    unitOfWork.SagsRepo.Update(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.SagsRepo.Get(includeProperties: "UnitMeasurements");
            return PartialView("_SagsGridViewGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "Delete Sags")]
        public ActionResult SagsGridViewGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]int? Id)
        {
            //var model = new object[0];
            if (Id >= 0)
            {
                try
                {
                    unitOfWork.SagsRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.SagsRepo.Get(includeProperties: "UnitMeasurements");
            return PartialView("_SagsGridViewGridViewPartial", model);
        }


        #endregion

        #region Program Of Work

        public ActionResult AddEditProgramOfWorkPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? programOfWorkId)
        {
            var model = unitOfWork.ProgramOfWorksRepo.Find(m => m.Id == programOfWorkId);
            return PartialView(model);
        }
        [ValidateInput(false)]
        public ActionResult ProgramOfWorkGridViewPartial()
        {
            var model = unitOfWork.ProgramOfWorksRepo.Get();
            return PartialView("_ProgramOfWorkGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ProgramOfWorkGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.ProgramOfWorks item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    unitOfWork.ProgramOfWorksRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.ProgramOfWorksRepo.Get();
            return PartialView("_ProgramOfWorkGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ProgramOfWorkGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.ProgramOfWorks item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.ProgramOfWorksRepo.TrackModifiedEntities(m => m.Id == item.Id, item);
                    // Insert here a code to update the item in your model
                    unitOfWork.ProgramOfWorksRepo.Update(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.ProgramOfWorksRepo.Get();
            return PartialView("_ProgramOfWorkGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ProgramOfWorkGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]int? Id)
        {
            if (Id >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    unitOfWork.ProgramOfWorksRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.ProgramOfWorksRepo.Get();
            return PartialView("_ProgramOfWorkGridViewPartial", model);
        }



        #endregion

        #region Particulars
        public ActionResult Particulars()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult ParticularGridViewPartial()
        {
            var model = unitOfWork.ParticularsRepo.Get();
            return PartialView("_ParticularGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ParticularGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Particulars item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    unitOfWork.ParticularsRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.ParticularsRepo.Get();
            return PartialView("_ParticularGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ParticularGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Particulars item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.ParticularsRepo.TrackModifiedEntities(m => m.Id == item.Id, item);
                    // Insert here a code to update the item in your model
                    unitOfWork.ParticularsRepo.Update(item);
                    unitOfWork.Save();

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.ParticularsRepo.Get();
            return PartialView("_ParticularGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ParticularGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]int? Id)
        {

            if (Id >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    unitOfWork.ParticularsRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.ParticularsRepo.Get();
            return PartialView("_ParticularGridViewPartial", model);
        }

        public ActionResult AddEditParticularPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? particularId)
        {
            var model = unitOfWork.ParticularsRepo.Find(m => m.Id == particularId);
            return PartialView(model);
        }


        #endregion

        

        #region Barangays

        public ActionResult AddEditBarangayPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? barangayId)
        {
            return PartialView(unitOfWork.BarangaysRepo.Find(m => m.Id == barangayId));
        }


        [ValidateInput(false)]
        public ActionResult BarangayGridViewPartial()
        {
            var model = unitOfWork.BarangaysRepo.Get();
            return PartialView("_BarangayGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "add barangay")]
        public ActionResult BarangayGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Barangays item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model

                    unitOfWork.BarangaysRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.BarangaysRepo.Get();
            return PartialView("_BarangayGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "update barangay")]
        public ActionResult BarangayGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Barangays item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    unitOfWork.BarangaysRepo.Update(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.BarangaysRepo.Get();
            return PartialView("_BarangayGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "delete barangay")]
        public ActionResult BarangayGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]System.Int32 Id)
        {

            if (Id >= 0)
            {
                try
                {
                    unitOfWork.BarangaysRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.BarangaysRepo.Get();
            return PartialView("_BarangayGridViewPartial", model);
        }


        #endregion


        public ActionResult TransactionTypes()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult TransactionTypeGridViewPartial()
        {
            var model = unitOfWork.TransactionTypesRepo.Get();
            return PartialView("_TransactionTypeGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]

        public ActionResult TransactionTypeGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.TransactionTypes item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    unitOfWork.TransactionTypesRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.TransactionTypesRepo.Get();
            return PartialView("_TransactionTypeGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TransactionTypeGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.TransactionTypes item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.TransactionTypesRepo.Update(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.TransactionTypesRepo.Get();
            return PartialView("_TransactionTypeGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TransactionTypeGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]System.Int32 Id)
        {
            if (Id >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    unitOfWork.TransactionTypesRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.TransactionTypesRepo.Get();
            return PartialView("_TransactionTypeGridViewPartial", model);
        }
    }
}