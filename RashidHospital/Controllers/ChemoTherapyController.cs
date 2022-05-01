using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hospital.DAL;
using RashidHospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

           // ViewBag.data = new SelectList(_list, "Template_ID", "Protocol_Name");

            return View(_list);
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