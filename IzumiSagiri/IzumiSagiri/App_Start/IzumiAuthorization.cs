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
                return false;
            }
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }
            return true;
        }

        public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;

            IzumiPrincipal principal = new IzumiPrincipal(HttpContext.Current.User.Identity.Name);
            filterContext.HttpContext.User = principal;

            if (!AuthorizeCore(filterContext.HttpContext))
            {
                string ReturnPara = string.Empty;
                var paras = filterContext.HttpContext.Request.Params;
                var paraNames = filterContext.ActionDescriptor.GetParameters();
                foreach (var paraName in paraNames)
                {
                    ReturnPara += paras[paraName.ParameterName];
                    ReturnPara += "&";
                }

            }
        }

    }
}