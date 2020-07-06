using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helpers;
using Models;
using Models.Repository;
using Microsoft.AspNet.Identity;

namespace Quary.New.Controllers
{
    public class ProgramOfWorksController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult AddEditProjectOfWorkPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? summaryId)
        {
            var model = unitOfWork.SummaryProgramOfWorksRepo.Find(m => m.Id == summaryId);
            return PartialView(model);
        }
        // GET: ProgramOfWorks
        [OnUserAuthorization(ActionName = "program of works")]
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult SummaryProgramOfWorkGridViewPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? programOfWorkId)
        {
            var model = unitOfWork.SummaryProgramOfWorksRepo.Get(m => m.ProgramOfWorkId == programOfWorkId);
            ViewBag.ProgramOfWorkId = programOfWorkId;
            return PartialView("_SummaryProgramOfWorkGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "add program of works")]
        public ActionResult SummaryProgramOfWorkGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.SummaryProgramOfWorks item, [ModelBinder(typeof(DevExpressEditorsBinder))]int? programOfWorkId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    unitOfWork.SummaryProgramOfWorksRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewBag.ProgramOfWorkId = programOfWorkId;
            var model = unitOfWork.SummaryProgramOfWorksRepo.Get(m => m.ProgramOfWorkId == programOfWorkId, includeProperties: "Sags,Particulars");
            return PartialView("_SummaryProgramOfWorkGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "update program of works")]
        public ActionResult SummaryProgramOfWorkGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.SummaryProgramOfWorks item, [ModelBinder(typeof(DevExpressEditorsBinder))]int? programOfWorkId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.SummaryProgramOfWorksRepo.Update(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewBag.ProgramOfWorkId = programOfWorkId;
            var model = unitOfWork.SummaryProgramOfWorksRepo.Get(m => m.ProgramOfWorkId == programOfWorkId, includeProperties: "Sags,Particulars");
            return PartialView("_SummaryProgramOfWorkGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        [OnUserAuthorization(ActionName = "delete program of works")]
        public ActionResult SummaryProgramOfWorkGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]int? Id, [ModelBinder(typeof(DevExpressEditorsBinder))]int? programOfWorkId)
        {
            if (Id >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    unitOfWork.SummaryProgramOfWorksRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            ViewBag.ProgramOfWorkId = programOfWorkId;
            var model = unitOfWork.SummaryProgramOfWorksRepo.Get(m => m.ProgramOfWorkId == programOfWorkId, includeProperties: "Sags,Particulars");
            return PartialView("_SummaryProgramOfWorkGridViewPartial", model);
        }

        public ActionResult PrintPopupSummaryOfProgramOfWork([ModelBinder(typeof(DevExpressEditorsBinder))]int? programOfWorkId)
        {
            var programOfWorks = unitOfWork.ProgramOfWorksRepo.Find(m => m.Id == programOfWorkId);
            programOfWorks.SummaryProgramOfWorks.Clear();
            var item = new List<ProgramOfWorks>();
            var summary = new List<ReportSummaryProgramOfWorks>();
            var x = unitOfWork.SummaryProgramOfWorksRepo.Fetch(m => m.ProgramOfWorkId == programOfWorkId).ToList();
            foreach (var i in unitOfWork.SummaryProgramOfWorksRepo.Fetch(m => m.ProgramOfWorkId == programOfWorkId).GroupBy(m => m.ParticularId))
            {

                var r = new ReportSummaryProgramOfWorks()
                {
                    Amount = x.Where(m => m.ProgramOfWorkId == programOfWorkId && m.ParticularId == i.Key && m.ProgramOfWorks.Permitees.PermiteeTypes?.PermiteeTypeName != "Government Gratuitous").Sum(m => m.Amount),
                    OrdinaryEarth = x.Where(m => m.ProgramOfWorkId == programOfWorkId && m.ParticularId == i.Key).Sum(m => m.OrdinaryEarth),
                    OrdinaryAmount = x.Where(m => m.ProgramOfWorkId == programOfWorkId && m.ParticularId == i.Key && m.ProgramOfWorks.Permitees.PermiteeTypes?.PermiteeTypeName != "Government Gratuitous").Sum(m => m.OrdinaryAmount),
                    Mixed = x.Where(m => m.ProgramOfWorkId == programOfWorkId && m.ParticularId == i.Key).Sum(m => m.Mixed),
                    MixedAmount = x.Where(m => m.ProgramOfWorkId == programOfWorkId && m.ParticularId == i.Key && m.ProgramOfWorks.Permitees.PermiteeTypes?.PermiteeTypeName != "Government Gratuitous").Sum(m => m.MixedAmount),
                    Boulders = x.Where(m => m.ProgramOfWorkId == programOfWorkId && m.ParticularId == i.Key).Sum(m => m.Boulders),
                    BouldersAmount = x.Where(m => m.ProgramOfWorkId == programOfWorkId && m.ParticularId == i.Key && m.ProgramOfWorks.Permitees.PermiteeTypes?.PermiteeTypeName != "Government Gratuitous").Sum(m => m.BouldersAmount),
                    Coarse = x.Where(m => m.ProgramOfWorkId == programOfWorkId && m.ParticularId == i.Key).Sum(m => m.Coarse),
                    CoarseAmount = x.Where(m => m.ProgramOfWorkId == programOfWorkId && m.ParticularId == i.Key && m.ProgramOfWorks.Permitees.PermiteeTypes?.PermiteeTypeName != "Government Gratuitous").Sum(m => m.CoarseAmount),
                    CrushedRocks = x.Where(m => m.ProgramOfWorkId == programOfWorkId && m.ParticularId == i.Key ).Sum(m => m.CrushedRocks),
                    CrushedRocksAmount = x.Where(m => m.ProgramOfWorkId == programOfWorkId && m.ParticularId == i.Key && m.ProgramOfWorks.Permitees.PermiteeTypes?.PermiteeTypeName != "Government Gratuitous").Sum(m => m.CrushedRocksAmount),
                    FineScreen = x.Where(m => m.ProgramOfWorkId == programOfWorkId && m.ParticularId == i.Key).Sum(m => m.FineScreen),
                    FineScreenAmount = x.Where(m => m.ProgramOfWorkId == programOfWorkId && m.ParticularId == i.Key && m.ProgramOfWorks.Permitees.PermiteeTypes?.PermiteeTypeName != "Government Gratuitous").Sum(m => m.FineScreenAmount),

                    Total = x.Where(m => m.ProgramOfWorkId == programOfWorkId && m.ParticularId == i.Key).Sum(m => m.Total),
                    TotalAmount = x.Where(m => m.ProgramOfWorkId == programOfWorkId && m.ParticularId == i.Key && m.ProgramOfWorks.Permitees.PermiteeTypes?.PermiteeTypeName != "Government Gratuitous").Sum(m => m.TotalAmount),
                    Particulars = unitOfWork.ParticularsRepo.Find(m => m.Id == i.Key),
                    Permittee = x.FirstOrDefault(m => m.ProgramOfWorkId == programOfWorkId)?.ProgramOfWorks?.Permitees
                };
                summary.Add(r);
            }




            programOfWorks.ReportSummaryProgramOfWorks = summary;
            item.Add(programOfWorks);
            rptSummaryProgramOfWorks report = new rptSummaryProgramOfWorks()
            {
                DataSource = item
            };
            var userId = User.Identity.GetUserId();
            var user = unitOfWork.UsersRepo.Find(m => m.Id == userId);
            report.lblPreparedBy.Text = user?.FullName?.ToUpper();
            report.lblPosition.Text = user?.Position?.ToUpper();
            return PartialView(report);
        }
    }
}