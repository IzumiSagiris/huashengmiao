using IzumiSagiri.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IzumiSagiri
{
    /// <summary>
    /// Home
    /// </summary>
    [IzumiAuthorization]
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index(long ID = 0, long License = 20)
        {
            return View();
        }

        public ActionResult License(long ID = 0, long License = 20)
        {
            return View();
        }

    }
}