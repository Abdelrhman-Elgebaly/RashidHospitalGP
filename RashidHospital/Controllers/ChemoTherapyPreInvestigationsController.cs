﻿using Hospital.DAL;
using RashidHospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace RashidHospital.Controllers
{
    public class ChemoTherapyPreInvestigationsController : Controller
    {
        // GET: ChemoTherapyPreInvestigations
        public ActionResult Index(string templateID)
        {
            int _templateID = Convert.ToInt32(templateID);
            fillBag(_templateID);
            ChemoTherapyPreInvestigationsVM ObjVm = new ChemoTherapyPreInvestigationsVM();
            List<ChemoTherapyPreInvestigationsVM> _list = ObjVm.SelectAllByTemplateID(_templateID);
            return View(_list);
        }


        public ActionResult Create(int templateID)
        {
            //  int _patientID = Convert.ToInt32(patientID);
            ChemoTherapyPreInvestigationsVM radioTherapyVMVM = new ChemoTherapyPreInvestigationsVM();
            radioTherapyVMVM.Template_ID = templateID;
            return View(radioTherapyVMVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChemoTherapyPreInvestigationsVM input)
        {

            if (ModelState.IsValid)
            {
                input.Create();
                return RedirectToAction("Index", new { templateID = input.Template_ID });
            }

            return View(input);
        }


        private void fillBag2(int patientID)
        {
            ViewBag.templateID = patientID;


        }

        private void fillBag(int patientID)
        {
            ViewBag.PatientId = patientID;
            ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(patientID);
            var userID = User.Identity.GetUserId();
            ViewBag.DoctorId = userID;

        }
    }
}