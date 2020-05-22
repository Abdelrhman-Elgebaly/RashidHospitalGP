using Hospital.DAL;
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
    [Authorize(Roles = "Admin,Doctor,Assistant,Consultant,Residents")]
    public class LabResualtController : Controller
    {
        // GET: LabResualt
        public ActionResult Index(int PatientId)
        {
            LabResualtVM _Labresults = new LabResualtVM();
           List<LabResualtVM> OrderList = _Labresults.SelectAllByPatientId(PatientId).OrderBy(a => a.ResualtDate).ToList();
            fillBag(PatientId);
            return View(OrderList);
        }
        private void fillBag(int patientID)
        {
            PatientVM _patientvm = new PatientVM();
            ViewBag.PatientId = patientID;
            PatientVM patientObj = _patientvm.SelectObject(patientID);
            ViewBag.PatientInfo = patientObj.Name + " - " + patientObj.MedicalID + "-" + patientObj.DiagnoseName + "-Register Date: " + patientObj.CreatedDate.ToShortDateString();

            var userID = User.Identity.GetUserId();
            ViewBag.DoctorId = userID;
        }
        [HttpPost]
        public int Delete(int ID)
        {
            int finalResult = 0;
            try
            {
                LabResualtVM _resultVM = new LabResualtVM();
                LabResualtVM DeleteObject = _resultVM.SelectObject(ID);
                DeleteObject.IsDeleted = true;
                DeleteObject.Edit();

                finalResult = 1;


            }
            catch (Exception e)
            {
                finalResult = 6;
            }
            return finalResult;
        }

        public JsonResult Create(int PatientId)
        {
            if (PatientId == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);


            }
            fillCreateBag(PatientId);
            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("Create", null) }, JsonRequestBehavior.AllowGet);

          //  return View();

        }
        private void fillCreateBag(int PatientId) {
            LabResualtVM results = new LabResualtVM();
           ViewBag.LabTypesList= results.GeLabTypsSelectList();
            fillBag(PatientId);

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
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(LabResualtVM vm)
        {
            try
            {
                vm.PatientId = vm.PatientId;
                vm.ResualtDate = DateTime.Now;
                Guid userId = Guid.Parse(User.Identity.GetUserId());
                vm.DoctorId = userId;
                vm.Create();
              
                //PatientId
                return RedirectToAction("Index", new { PatientId = vm.PatientId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error500");
            }
        }
        [HttpPost]
        public string AddResult(int LabType, string Unit, string Result, int PatientId) {
            if (LabType != 0 || Unit != null || Result != null || PatientId != 0) {
                LabResualtVM _labresults = new LabResualtVM();
                _labresults.PatientId = PatientId;
                _labresults.LabType = LabType;
                _labresults.Unit = Unit;
                _labresults.Note = Result;
                _labresults.ResualtDate = DateTime.Now;
                Guid userId = Guid.Parse(User.Identity.GetUserId());
                _labresults.DoctorId = userId;
                _labresults.Create();
            }
           
            return "success";
        }
        // GET: Clinics/Edit/5
        public JsonResult _Edit(int Id)
        {
            if (Id == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            LabResualtVM _Obj = new LabResualtVM();
            LabResualtVM _objVM = _Obj.SelectObject(Id);
            fillCreateBag(_objVM.PatientId);
            if (_objVM == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("_Edit", _objVM) }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult EditLab(LabResualtVM vm)
        {
            try
            {
                Guid userId = Guid.Parse(User.Identity.GetUserId());
                vm.DoctorId = userId;
                vm.Edit();
                //PatientId
                return RedirectToAction("Index", new { PatientId = vm.PatientId });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error500");
             ;
            }
        }
    }
}