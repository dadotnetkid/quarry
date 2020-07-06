using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Models.Repository;

namespace Helpers
{
    public class OnUserAuthorizationAttribute : AuthorizeAttribute
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            string actionName = ActionName;//HttpContext.Current.Request.RequestContext.RouteData.Values["Action"].ToString();
            string controllerName = ControllerName;// HttpContext.Current.Request.RequestContext.RouteData.Values["Controller"].ToString();
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var users = unitOfWork.UsersRepo.Fetch(m => m.Id == userId).Any(m => m.UserRoles.Any(x => x.UserRolesInActions.Any(u => u.Action.Contains(actionName))));


            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (!users)
                    filterContext.Result = new RedirectToRouteResult(new
                        RouteValueDictionary(new { controller = "error", action = "accessdenied" }));
            }
            base.OnAuthorization(filterContext);
        }
    }
}
