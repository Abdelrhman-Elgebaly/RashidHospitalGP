using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    public class ChemoCycleNuresNoteController : Controller
    {
        public ActionResult Index(int Id, string pid)
        {

            ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
            ChemoTherapyCycleDayVM _Objm = _Obj.SelectObject(Id);
            ViewBag.SelectedNote = _Objm.SelectedNnote;
            int _patientID = Convert.ToInt32(pid);
            fillBag(Id, _patientID);

            NurseNoteVM ObjVm = new NurseNoteVM();
            List<NurseNoteVM> _list = ObjVm.SelectAllByPatientId(_patientID);
            return View(_list);
        }



        public ActionResult Create(int Id, string pid)
        {
            int _patientID = Convert.ToInt32(pid);
            fillBag(Id, _patientID);
            NurseNoteVM nurseNoteVM = new NurseNoteVM();
            nurseNoteVM.Patient_ID = _patientID;
            nurseNoteVM.CycleId = Id;
            // fillBag(_patientID);

            return View(nurseNoteVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NurseNoteVM input)
        {

            if (ModelState.IsValid)
            {
                input.Date = DateTime.Now;

                var x = (input.Weight * input.Height) / 3600;
                input.SA = Math.Sqrt(Convert.ToDouble(x));
                double test = Convert.ToDouble(input.SA);
                input.SA = Math.Round(test, 2);

                input.Create();
                return RedirectToAction("Index", new { Id = input.CycleId, pid = input.Patient_ID });
            }

            return View(input);
        }

        private void fillBag(int CycleId, int PatientId)
        {
            PatientVM _patientvm = new PatientVM();
            ViewBag.PatientId = PatientId;
            ViewBag.CycleId = CycleId;
            ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(PatientId);
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