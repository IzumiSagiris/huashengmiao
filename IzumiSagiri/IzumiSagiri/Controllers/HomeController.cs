﻿using IzumiSagiri.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IzumiSagiris.Service.IzmuService;

namespace IzumiSagiri
{
    /// <summary>
    /// Home
    /// </summary>
    [IzumiAuthorization]
    public class HomeController :  BaseController
    {
        public readonly IzumiInterFace _service;
        public HomeController()
        {
            _service = DependencyResolver.Current.GetService<IzumiInterFace>();
        }
        // GET: Home
        public ActionResult Index(long ID = 10, long License = 20)
        {
            return View();
        }

        public ActionResult License(long ID = 0, long License = 20)
        {
            return View();
        }

        public ActionResult test()
        {
            return View();
        }

    }
}