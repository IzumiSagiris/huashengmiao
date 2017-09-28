using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IzumiSagiri.App_Start
{
    public class IzumiAuthorization : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("HttpContext");
            }
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                var paras = httpContext.Request.Params;

                return false;
            }

            return true;
        }

        public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;
            string ReturnPara = string.Empty;

            var paras = filterContext.HttpContext.Request.Params;
            var paraNames = filterContext.ActionDescriptor.GetParameters();
            foreach(var paraName in paraNames)
            {

            }

            base.OnAuthorization(filterContext);
        }

    }
}