using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace IzumiSagiri
{
    public class BaseController : Controller
    {
        public BaseController()
        {

        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {

        }
    }
}