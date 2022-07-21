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
                double test = Convert.ToDouble(input.SA);
                input.SA = Math.Round(test, 2);

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


        public JsonResult _EditNote(int Id)
        {
            ViewBag.Id = Id;
            if (Id == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            ViewBag.Id = Id;

            NurseNoteVM _Obj = new NurseNoteVM();
            NurseNoteVM _ObjvM = _Obj.SelectObject(Id);
            //if (_objVM == null)
            //{
            //    return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);
            //}

            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("_EditNote", _ObjvM) }, JsonRequestBehavior.AllowGet);
        }

        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        public JsonResult AddNote(int Id, string Note, int SA)

        {

            NurseNoteVM _Obj = new NurseNoteVM();
            NurseNoteVM _ObjvM = _Obj.SelectObject(Id);
            _ObjvM.Pharmacist_Note = Note;
            _ObjvM.SA = SA;
            _ObjvM.Edit();
            return Json(new { IsRedirect = true }, JsonRequestBehavior.AllowGet);

        }

    }
}