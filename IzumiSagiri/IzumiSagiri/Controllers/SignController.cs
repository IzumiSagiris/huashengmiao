using IzumiSagiri.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IzumiSagiri
{
    public class SignController : BaseController
    {
        // GET: SignIn
        [AllowAnonymous]
        public ActionResult SignIn(string returnUrl = "")
        {
            Response.Headers.Add("returnUrl", returnUrl);
            return View();
        }

        [AllowAnonymous]
        public ActionResult SignOn(string UserName = "", string Password = "", string returnUrl = "")
        {
            //test
            UserName = "Izmui";
            Password = "Sagiri";
            FormsAuthentication.SetAuthCookie(UserName, false);
            return RedirectToAction("Index", "Home");
        }
    }
}