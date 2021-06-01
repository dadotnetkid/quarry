using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using Helpers;
using Microsoft.AspNet.Identity;
using Models;
using Models.Entities;
using Models.Repository;

namespace Quary.New.Controllers
{
    public partial class TransactionsController : Controller
    {


        #region Import Productions

        public ActionResult ImportProductions()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult ImportProductionGridViewPartial()
        {
            var model = Session["excel"] as DataTable;
            List<object> obj = new List<object>();
            if (model != null)
            {
                foreach (DataRow i in model.Rows)
                {
                    string code = i["code"]?.ToString();
                    string sagId = i["sag"]?.ToString();
                    int? quantity = Convert.ToInt32(i["quantity"]);
                    string plateno = i["plateno"]?.ToString();
                    string quarries = i["quarries"]?.ToString();
                    string receiptNo = i["receiptno"]?.ToString();
                    unitOfWork.ProductionsRepo.Insert(new Models.Productions()
                    {
                        PermiteeId = unitOfWork.PermiteesRepo.Find(m => m.AccreditationNumber == code)?.Id,
                        SagId = unitOfWork.SagsRepo.Find(m => m.Sag == sagId)?.Id,
                        Quantity = quantity,
                        VehicleId = unitOfWork.VehiclesRepo.Find(m => m.PlateNo == plateno)?.Id,
                        QuarriesId = unitOfWork.QuarriesRepo.Find(m => m.QuarryName == quarries)?.Id,
                        ReceiptNo = receiptNo,
                        DateCreated = DateTime.Now

                    });
                    unitOfWork.Save();
                    obj.Add(new
                    {
                        Permittee = unitOfWork.PermiteesRepo.Find(m => m.AccreditationNumber == code)?.FullName,
                        Code = code,
                        PlateNo = i["plateno"],
                        Sag = unitOfWork.SagsRepo.Find(m => m.Sag == sagId).Sag,
                        ReceiptNo = i["receiptno"],
                        Destination = i["destination"],
                        Origin = i["origin"],
                        Vehicle = plateno,
                        Quarries = quarries,
                        Quantity = quantity
                    });
                }


            }

            return PartialView("_ImportProductionGridViewPartial", obj);
        }

        #endregion













        [ValidateInput(false)]
        public ActionResult ActiveQuarryGridViewPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            int? permiteeId)
        {
            DomainDb db = new DomainDb();
            var model = new List<ActiveQuarries>();
            if (permiteeId != null)
            {
                var year = DateTime.Now.Year;
                model = db.ActiveQuarries.Where(x => x.PermitteeId == permiteeId && x.Year == year).ToList();
                ViewBag.Quarries = unitOfWork.QuarriesRepo.Get(x => x.Permitees.Any(m => m.Id == permiteeId));
                ViewBag.permiteeId = permiteeId;
            }

            return PartialView("_ActiveQuarryGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ActiveQuarryGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Entities.ActiveQuarries item)
        {
            DomainDb db = new DomainDb();
            var model = new List<ActiveQuarries>();
            var year = DateTime.Now.Year;


            if (ModelState.IsValid)
            {
                try
                {
                    db.ActiveQuarries.Add(new ActiveQuarries()
                    {
                        Id = Guid.NewGuid().ToString(),
                        PermitteeId = item.PermitteeId,
                        QuarryId = item.QuarryId,
                        Year = DateTime.Now.Year
                    });
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            model = db.ActiveQuarries.Where(x => x.PermitteeId == item.PermitteeId && x.Year == year).ToList();
            ViewBag.Quarries = unitOfWork.QuarriesRepo.Get(x => x.Permitees.Any(m => m.Id == item.PermitteeId));
            ViewBag.permiteeId = item.PermitteeId;
            return PartialView("_ActiveQuarryGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ActiveQuarryGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Entities.ActiveQuarries item)
        {
            DomainDb db = new DomainDb();
            var model = new List<ActiveQuarries>();

            var year = DateTime.Now.Year;

            if (ModelState.IsValid)
            {
                try
                {
                    var quarry = db.ActiveQuarries.Find(item.Id);
                    quarry.QuarryId = item.QuarryId;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            model = db.ActiveQuarries.Where(x => x.PermitteeId == item.PermitteeId && x.Year == year).ToList();
            ViewBag.Quarries = unitOfWork.QuarriesRepo.Get(x => x.Permitees.Any(m => m.Id == item.PermitteeId));
            ViewBag.permiteeId = item.PermitteeId;
            return PartialView("_ActiveQuarryGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ActiveQuarryGridViewPartialDelete(System.String Id)
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
            return PartialView("_ActiveQuarryGridViewPartial", model);
        }
    }
}