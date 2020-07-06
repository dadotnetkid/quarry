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
                    int? receiptNo = Convert.ToInt32(i["receiptno"]);
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












    }
}