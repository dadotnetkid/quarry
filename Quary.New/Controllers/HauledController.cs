using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helpers;
using Models.Repository;
using Microsoft.AspNet.Identity;
using Models;

namespace Quary.New.Controllers
{
    public class HauledController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Hauled
        [OnUserAuthorization(ActionName ="Hauled")]
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult HauledGridViewPartial()
        {
            var model = unitOfWork.HauledsRepo.Get(includeProperties: "Sags,ProgramOfWorks");
            return PartialView("_HauledGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "add hauled")]
        public ActionResult HauledGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Hauleds item)
        {
            //var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    item.DateCreated = DateTime.Now;
                    item.CreatedBy = User.Identity.GetUserId();
                    // Insert here a code to insert the new item in your model
                    unitOfWork.HauledsRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.HauledsRepo.Get(includeProperties: "Sags,ProgramOfWorks");
            return PartialView("_HauledGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "update hauled")]
        public ActionResult HauledGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Hauleds item)
        {
            //var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    unitOfWork.HauledsRepo.Update(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.HauledsRepo.Get(includeProperties: "Sags,ProgramOfWorks");
            return PartialView("_HauledGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "delete hauled")]
        public ActionResult HauledGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]System.Int32 Id)
        {
            //var model = new object[0];
            if (Id >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    unitOfWork.HauledsRepo.Delete(m=>m.Id==Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.HauledsRepo.Get(includeProperties: "Sags,ProgramOfWorks");
            return PartialView("_HauledGridViewPartial", model);
        }

        public ActionResult AddEditHauledPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            int? hauledId)
        {
            var model = unitOfWork.HauledsRepo.Find(m => m.Id == hauledId);
            return PartialView(model);
        }
    }
}