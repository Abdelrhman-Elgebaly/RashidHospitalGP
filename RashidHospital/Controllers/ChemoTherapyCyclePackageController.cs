
using System.Web.Mvc;
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
    public class ChemoTherapyCyclePackageController : Controller
    {
        // GET: ChemoTherapyCyclePackage
        public ActionResult Index(int Id)
        {
            //
           ChemoTherapyCyclePackageVM _Obj = new ChemoTherapyCyclePackageVM();
            List<ChemoTherapyCyclePackageVM> LabList = _Obj.SelectAllByCycleID(Id);





            //
            ChemoTherapyCycleDayVM _cObj = new ChemoTherapyCycleDayVM();
            ChemoTherapyCycleDayVM _objVM = _cObj.SelectObject(Id);
            int patientID = Convert.ToInt32(_objVM.Patient_ID);
            PatientVM _pObj = new PatientVM();
            PatientVM _pObjVM = _pObj.SelectObject(patientID);

            int TemplateID = Convert.ToInt32(_pObjVM.ChemoTherapyId);
   
            ChemoTherapyTemplateVM _Objt = new ChemoTherapyTemplateVM();
            ChemoTherapyTemplateVM _objVMt = _Objt.SelectObject(TemplateID);
            fillBag(patientID);

            var tuple = new Tuple<ChemoTherapyTemplateVM, List<ChemoTherapyCyclePackageVM>>(_objVMt, LabList);
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



    }
}

