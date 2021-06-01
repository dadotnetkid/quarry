using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using DevExpress.XtraReports.Web;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Models.Migrations;
using Newtonsoft.Json;

namespace Quary.New
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            DevExpress.XtraReports.Web.QueryBuilder.Native.QueryBuilderBootstrapper.SessionState = System.Web.SessionState.SessionStateBehavior.Disabled;

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            };
            var services = new ServiceCollection();
            AddFluentMigratorService(services);

            DevExpress.XtraReports.Web.WebDocumentViewer.Native.WebDocumentViewerBootstrapper.SessionState = System.Web.SessionState.SessionStateBehavior.Disabled;
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ASPxWebDocumentViewer.StaticInitialize();
            ASPxQueryBuilder.StaticInitialize();


            var resolver = new DefaultDependencyResolver(services.BuildServiceProvider());

            // Set the application resolver to our default resolver. This comes from "System.Web.Mvc"
            //Other services may be added elsewhere through time
            DependencyResolver.SetResolver(resolver);

        }
        private static IServiceProvider AddFluentMigratorService(ServiceCollection services)
        {
            return services
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(new DomainDb().Database.Connection.ConnectionString)
                    .ScanIn(typeof(ActiveTransactionMigration).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }
        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
    public class DefaultDependencyResolver : IDependencyResolver
    {
        /// <summary>
        /// Provides the service that holds the services
        /// </summary>
        protected IServiceProvider serviceProvider;

        /// <summary>
        /// Create the service resolver using the service provided (Direct Injection pattern)
        /// </summary>
        /// <param name="serviceProvider"></param>
        public DefaultDependencyResolver(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Get a service by type - assume you get the first one encountered
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            return this.serviceProvider.GetService(serviceType);
        }

        /// <summary>
        /// Get all services of a type
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.serviceProvider.GetServices(serviceType);
        }
    }
}