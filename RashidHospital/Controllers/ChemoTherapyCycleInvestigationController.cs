using System;
using System.Collections.Generic;
using System.Web.Mvc;
using RashidHospital.Models;
using System.IO;

using System;
using System.Globalization;
using Microsoft.AspNet.Identity;

namespace RashidHospital.Controllers
{
    public class ChemoTherapyCycleInvestigationController : Controller
    {
        // GET: ChemoTherapyCycleInvestigation
        public ActionResult Index(int Id, int pid)
        {
            //
            ChemoTherapyCycleInvestigationVM _Obj = new ChemoTherapyCycleInvestigationVM();




            List<ChemoTherapyCycleInvestigationVM> LabList = _Obj.SelectAllByCycleID(Id);


            ChemoTherapyCycleDayVM _dObj = new ChemoTherapyCycleDayVM();
            ChemoTherapyCycleDayVM _cObjVM = _dObj.SelectObject(Id);

            //

            int patientID = Convert.ToInt32(pid);


            int TemplateID = Convert.ToInt32(_cObjVM.TemplateId);

            ChemoTherapyTemplateVM _Objt = new ChemoTherapyTemplateVM();
            ChemoTherapyTemplateVM _objVMt = _Objt.SelectObject(TemplateID);
            fillBag(patientID);
            fillBag2(Id);






            var tuple = new Tuple<ChemoTherapyTemplateVM, List<ChemoTherapyCycleInvestigationVM>>(_objVMt, LabList);
            return View(tuple);

        }

        private void fillBag(int patientID)
        {
            PatientVM _patientvm = new PatientVM();
            ViewBag.PatientId = patientID;

            ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(patientID);
            var userID = User.Identity.GetUserId();
            ViewBag.DoctorId = userID;

        }
        private void fillBag2(int CycleId)
        {

            ViewBag.CycleId = CycleId;

        }


    }
}