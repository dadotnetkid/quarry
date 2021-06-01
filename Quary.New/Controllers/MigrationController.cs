using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentMigrator.Infrastructure;
using FluentMigrator.Runner;

namespace Quary.New.Controllers
{
    public class MigrationController : Controller
    {
        private readonly IMigrationRunner _migrationRunner;

        // GET: Migration
        public MigrationController()
        {
            _migrationRunner = DependencyResolver.Current.GetService<IMigrationRunner>();
        }

        public ActionResult Index()
        {
            try
            {
                _migrationRunner.MigrateUp();

            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return View();
        }
    }
}