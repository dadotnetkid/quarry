using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Quary.New.Controllers
{
    // Processes Query Builder requests.
    public class QueryBuilderController : DevExpress.Web.Mvc.Controllers.QueryBuilderApiController
    {
        public override ActionResult Invoke()
        {
            return base.Invoke();
        }
    }
}