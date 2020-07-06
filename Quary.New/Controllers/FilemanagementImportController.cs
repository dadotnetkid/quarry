using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web;
using Helpers;

namespace Quary.New.Controllers
{
    public class FilemanagementImportController : Controller
    {
        private FilemanagementImportHelper importHelper;

        public ActionResult PopupImportContainerPartial(string actionName, string controllerName, object routes)
        {
            ViewBag.ActionName = actionName;
            ViewBag.ControllerName = controllerName;
            ViewBag.Routes = routes;

            return PartialView(routes);
        }




        [OnUserAuthorization(ActionName = "Import Quarries")]
        public ActionResult ImportQuarriesUploadControlUpload([ModelBinder(typeof(DevExpressEditorsBinder))]bool? validate)
        {
            UploadControlSettings.Validate = validate;
            var files = UploadControlExtension.GetUploadedFiles("ImportQuarriesUploadControlUpload", UploadControlSettings.UploadValidationSettings, UploadControlSettings.ImportQuarries);
            //     importHelper = new FilemanagementImportHelper(new ExcelHelper(Session["excelFilePath"] as string).ExecuteReader());


            return null;
        }
        [OnUserAuthorization(ActionName = "Import Permittees")]
        public ActionResult ImportPermitteeUploadControlUpload([ModelBinder(typeof(DevExpressEditorsBinder))]
            bool? validate)
        {
            UploadControlSettings.Validate = validate;
            var files = UploadControlExtension.GetUploadedFiles("ImportPermitteeUploadControlUpload", UploadControlSettings.UploadValidationSettings, UploadControlSettings.ImportPermittee);

            return null;
            //return PartialView();
        }
        [OnUserAuthorization(ActionName = "Import Vehicles")]
        public ActionResult ImportVehicleUploadControlUpload([ModelBinder(typeof(DevExpressEditorsBinder))]
            bool? validate)
        {
            try
            {
                UploadControlSettings.Validate = validate;
                var files = UploadControlExtension.GetUploadedFiles("ImportVehicleUploadControlUpload", UploadControlSettings.UploadValidationSettings, UploadControlSettings.ImportVehicle);


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            return null;
        }

        public ActionResult ImportProductionUploadControlUpload([ModelBinder(typeof(DevExpressEditorsBinder))]
            bool? validate, [ModelBinder(typeof(DevExpressEditorsBinder))]
            bool? vehicle)
        {
            try
            {

                UploadControlSettings.Validate = validate;
                UploadControlSettings.AutomaticAddVehicle = vehicle;
                var files = UploadControlExtension.GetUploadedFiles("ImportProductionUploadControlUpload", UploadControlSettings.UploadValidationSettings, UploadControlSettings.ImportProduction);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            return null;
        }
        public ActionResult ImportBarangayUploadControlUpload([ModelBinder(typeof(DevExpressEditorsBinder))]
            bool? validate)
        {
            try
            {
                UploadControlSettings.Validate = validate;
                var files = UploadControlExtension.GetUploadedFiles("ImportBarangayUploadControlUpload", UploadControlSettings.UploadValidationSettings, UploadControlSettings.ImportBarangay);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            return null;
        }

    }
    public class UploadControlSettings
    {
        public static bool? Validate;
        private static FilemanagementImportHelper importHelper;
        public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".xls", ".xlsx" },
            MaxFileSize = 4000000
        };

        public static bool? AutomaticAddVehicle { get; set; }

        public static void FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                // Save uploaded file to some location

                var fileName = HttpContext.Current.
                    Server.MapPath(System.IO.Path.Combine("~/content/excel", Guid.NewGuid().ToString() + ".xlsx"));
                e.UploadedFile.SaveFile();
                HttpContext.Current.Session["excelFilePath"] = fileName;

            }
        }

        public static void ImportPermittee(object sender, FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                // Save uploaded file to some location
                var fileName = e.UploadedFile.SaveFile();
                importHelper = new FilemanagementImportHelper(new ExcelHelper(fileName).ExecuteReader());
                var helper = importHelper.ImportPermittees(Validate);
                if (helper != "")
                {
                    e.ErrorText = helper;
                    e.CallbackData = helper;
                }
                else
                {
                    e.CallbackData = "Successfully imported permittees";
                }
                System.IO.File.Delete(fileName);

            }
        }

        public static void ImportVehicle(object sender, FileUploadCompleteEventArgs e)
        {
            var fileName = e.UploadedFile.SaveFile();
            importHelper = new FilemanagementImportHelper(new ExcelHelper(fileName).ExecuteReader());
            var helper = importHelper.ImportVehicles(Validate);
            if (helper != "")
                e.CallbackData = helper;
            else
                e.CallbackData = "Successfully imported vehicles";
            System.IO.File.Delete(fileName);
        }
        public static void ImportQuarries(object sender, FileUploadCompleteEventArgs e)
        {

            var fileName = e.UploadedFile.SaveFile();
            importHelper = new FilemanagementImportHelper(new ExcelHelper(fileName).ExecuteReader());
            var helper = importHelper.ImportQuarries(Validate);
            if (helper != "")
                e.CallbackData = helper;
            else
                e.CallbackData = "Successfully imported quarries";
            System.IO.File.Delete(fileName);
        }

        public static void ImportProduction(object sender, FileUploadCompleteEventArgs e)
        {
            var fileName = e.UploadedFile.SaveFile();
            importHelper = new FilemanagementImportHelper(new ExcelHelper(fileName).ExecuteReader());
            var helper = importHelper.ImportProduction(Validate, AutomaticAddVehicle);
            if (helper != "")
                e.CallbackData = helper;
            else
                e.CallbackData = "Successfully imported production";
            System.IO.File.Delete(fileName);
        }

        public static void ImportBarangay(object sender, FileUploadCompleteEventArgs e)
        {
            var fileName = e.UploadedFile.SaveFile();
            importHelper = new FilemanagementImportHelper(new ExcelHelper(fileName).ExecuteReader());
            var helper = importHelper.ImportBarangay(Validate);
            if (helper != "")
                e.CallbackData = helper;
            else
                e.CallbackData = "Successfully imported barangay";
            System.IO.File.Delete(fileName);
        }
    }

}