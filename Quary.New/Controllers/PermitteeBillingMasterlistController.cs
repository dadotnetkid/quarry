using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using Helpers.Services;
using Models;
using Models.Repository;
using Models.ViewModels;

namespace Quary.New.Controllers
{
    public class PermitteeBillingMasterlistController : Controller
    {
        private readonly IBillingMasterListReportService _billingMasterListReportService;

        public PermitteeBillingMasterlistController()
        {
            _billingMasterListReportService = BillingMasterListReportService.Create();
        }
        // GET: PermitteeBillingMasterlist
        public ActionResult Index()
        {
            var list = new List<Transactions>();
            list.Add(new Transactions() { Id = "", TransactionNumber = "All" });

            list.AddRange(new UnitOfWork().TransactionsRepo.Fetch(x => x.TransactionSags.Any()).ToList());
            return View(list);
        }

        public ActionResult CboPermitteeType()
        {
            return PartialView(_billingMasterListReportService.GetPermitteeType());
        }

                public ActionResult PermitteeBillingMasterlistPartial([ModelBinder(typeof(DevExpressEditorsBinder))] string transactionId = "")
        {
            rptPermitteeBillingMasterlist rpt = new rptPermitteeBillingMasterlist();
            rpt.DataSource = new List<PermitteeBillingMasterlistViewModel>()
            {
                new PermitteeBillingMasterlistViewModel()
                {
                    Permitees=new UnitOfWork().PermiteesRepo.Fetch().ToList(),//: new List<Permitees>(){new UnitOfWork().TransactionsRepo.Fetch(x=>x.Id==transactionId).FirstOrDefault()?.Permitees }
                    TransactionId=transactionId

                }
    };
            return PartialView(rpt);
        }
    }
}