using Microsoft.AspNet.Identity;
using RashidHospital.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static RashidHospital.Helper.Enum;

namespace RashidHospital.Controllers
{
    public class NurseNoteController : Controller
    {
        public ActionResult Index(string patientID)
        {
            int _patientID = Convert.ToInt32(patientID);
            fillBag(_patientID);
            NurseNoteVM ObjVm = new NurseNoteVM();

            List<NurseNoteVM> _list = ObjVm.SelectAllByPatientId(_patientID);
            return View(_list);
        }
        public ActionResult Create(string patientID)
        {
            int _patientID = Convert.ToInt32(patientID);
            NurseNoteVM nurseNoteVM = new NurseNoteVM();
            nurseNoteVM.Patient_ID = _patientID;
            fillBag(_patientID);
           
            return View(nurseNoteVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NurseNoteVM input)
        {
          
            if (ModelState.IsValid)
            {
                input.Date = DateTime.Now;


                // Guid userId = Guid.Parse(User.Identity.GetUserId());
                //double x = Convert.ToDouble((input.Weight * input.Height) / 3600);
                var x = (input.Weight * input.Height) / 3600;
                input.SA = Math.Sqrt(Convert.ToDouble(x));
                input.Create();
                return RedirectToAction("Index", new { patientID = input.Patient_ID });
            }

            return View(input);
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