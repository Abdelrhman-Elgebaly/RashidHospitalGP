﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RashidHospital.Controllers
{
    public class TblPatientsController : Controller
    {


        private SampleRestApiEntities db = new SampleRestApiEntities();

        // GET: TblPatients
        public ActionResult Index()
        {
            return View();
        }
    }
}