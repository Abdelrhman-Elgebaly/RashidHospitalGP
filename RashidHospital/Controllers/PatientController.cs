using Microsoft.AspNet.Identity;
using RashidHospital.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RashidHospital.Controllers
{
    [Authorize(Roles = "Doctor, Residents,Admin,Assistant lecturer,Consultant,Nurses,physician,Employee,Assistant,Pharmacist")]
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult Index()
        {
            PatientVM _obj = new PatientVM();
            List<PatientVM> list = _obj.SelectAll();
            return View(list);
        }


        // GET: Clinics/Create
        public ActionResult Create()
        {
            FillViewBags();
            PatientVM _obj = new PatientVM();
            return View(_obj);
        }

        // POST: Clinics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PatientVM patient)
        {
                patient.CreatedDate = DateTime.Now;
                 DiagonsVM _DiagonsVM = new DiagonsVM();
                patient.DiagnoseId= _DiagonsVM.GetDefault();
                Guid userId = Guid.Parse(User.Identity.GetUserId());
                 patient.CreatedBy = userId;
                patient.Create();
                return RedirectToAction("Index");
           

        }

        // GET: Clinics/Edit/5
        public JsonResult _Edit(int patientID)
        {
            if (patientID == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            FillViewBags();

            PatientVM _Obj = new PatientVM();
            PatientVM _objVM = _Obj.SelectObject(patientID);
            if (_objVM == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("_Edit", _objVM) }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult View(string patientID)
        {
            if (patientID == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            FillViewBags();
            int PatientId = Convert.ToInt32(patientID);
            PatientVM _Obj = new PatientVM();
            PatientVM _objVM = _Obj.SelectObject(PatientId);
            ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(PatientId);
            if (_objVM == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            return View(_objVM);

        }

        public JsonResult _EditDiagnose(int patientID) {
          

            PatientVM _Obj = new PatientVM();
            PatientVM _objVM = _Obj.SelectObject(patientID);
            if (_objVM == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("_DiagonsEdit", _objVM) }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public string EditPatient(PatientVM vm)
        {
            try
            {
               
                    vm.ModifiedDate = DateTime.Now;
                    vm.Edit();
                    return "Success"; // succcess
                
            }
            catch (Exception e)
            {
                return "Error500";
            }
        }

        [HttpPost]
        public string EditDiagnosePatient(PatientVM vm) {
            try
            {
                PatientVM _Obj = new PatientVM();
                PatientVM _objVM = _Obj.SelectObject(vm.Id);
             _objVM.BirthDate = vm.BirthDate;
             _objVM.ModifiedDate = DateTime.Now;
                _objVM.Edit();
                return "Success"; // succcess
            }
            catch (Exception ex)
            {

                return "Error500";

            }
        }

        [HttpPost]
        public int DeletePatient(int PatientId)
        {
            int finalResult = 0;
            try
            {
                PatientVM _patient = new PatientVM();
                PatientVM _obj = _patient.SelectObject(PatientId);
                _obj.IsDeleted = true;
                Guid userId = Guid.Parse(User.Identity.GetUserId());
                _obj.ModifiedBy = userId;
                _obj.Edit();
                finalResult = 1;


            }
            catch (Exception e)
            {
                finalResult = 6;
            }
            return finalResult;
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
        private void FillViewBags()
        {
            PatientUnitVM _PatientUnitModel = new PatientUnitVM();
            ViewBag.PatientUnit = _PatientUnitModel.GetSelectList();
        }

        private void FillDiagnoseViewBags() {

            DiagonsVM _DiagnoseModel = new DiagonsVM();
            ViewBag.Diagnoses = _DiagnoseModel.GetSelectList();
        }
        [HttpGet]
        public ActionResult RequestsandResults(int patientID) {
            PatientVM _Obj = new PatientVM();
            int PatientId = Convert.ToInt32(patientID);
            PatientVM _objVM = _Obj.SelectObject(PatientId);
            ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(PatientId);
            return View(_objVM); }
    }
}