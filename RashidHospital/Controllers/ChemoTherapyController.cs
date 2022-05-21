﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using RashidHospital.Models;
using System;
using System.Globalization;
using Microsoft.AspNet.Identity;
namespace RashidHospital.Controllers
{
    public class ChemoTherapyController : Controller
    {
        // GET: ChemoTherapy
        public ActionResult Index(string patientID)
        {
            int _patientID = Convert.ToInt32(patientID);
            fillBag(_patientID);
            ChemoTherapyTemplateVM ObjVm = new ChemoTherapyTemplateVM();
            List <ChemoTherapyTemplateVM> _list = ObjVm.SelectAll();

            return View(_list);
        }

        private void fillBag(int patientID)
        {
            ViewBag.PatientId = patientID;
            ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(patientID);
            var userID = User.Identity.GetUserId();
            ViewBag.DoctorId = userID;

        }


        public ActionResult bbTemplate(string templateID, DateTime date)
        {
            int TemplateID = Convert.ToInt32(templateID);
            ChemoTherapyTemplateVM _Obj = new ChemoTherapyTemplateVM();
            ChemoTherapyTemplateVM _objVM = _Obj.SelectObject(TemplateID);
            // ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(PatientId);


            _Obj.Admin_Date = date;
            List<DateTime> dates = new List<DateTime>();
            dates.Add( date);
            for (int i = 1; i < _Obj.Maximum_cycles; i++)
            {
            double x =     Convert.ToDouble(_Obj.Frequency);
            DateTime newDate  =   date.AddDays(x);
                dates.Add(newDate);

            }
            _Obj.cycleDates = dates;

        }

      



    }
}