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

    public class RadiologyResultController : Controller
    {
        // GET: RadiologyResult
        public ActionResult Index(string patientID)
        {
            int _patientID = Convert.ToInt32(patientID);
            fillBag(_patientID);
            RadiologyResultVM ObjVm = new RadiologyResultVM();
            List<RadiologyResultVM> _list = ObjVm.SelectAllByPatientId(_patientID);
            return View(_list);
        }
        private void fillBag(int patientID)
        {
            PatientVM _patientvm = new PatientVM();
            ViewBag.PatientId = patientID;
            PatientVM _Objvm = _patientvm.SelectObject(patientID);
            ViewBag.PatientInfo = _Objvm.Name + " - " + _Objvm.MedicalID + "-" + _Objvm.DiagnoseName + "-Register Date: " + _Objvm.CreatedDate.ToShortDateString();

            var userID = User.Identity.GetUserId();
            ViewBag.DoctorId = userID;
        }
        [HttpPost]
        public int Delete(int RequestID)
        {
            int finalResult = 0;
            try
            {
                RadiologyResultVM _VMObject = new RadiologyResultVM();
                RadiologyResultVM _obj = _VMObject.SelectObject(RequestID);
                _obj.IsDeleted = true;
                _obj.Edit();

                finalResult = 1;


            }
            catch (Exception e)
            {
                finalResult = 6;
            }
            return finalResult;
        }

        public JsonResult _Create(int patientID)
        {
            if (patientID == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            RadiologyResultVM _obj = new RadiologyResultVM();
            _obj.PateintID = patientID;
            fillcreatebag(patientID);
            AspNetUser _users = new AspNetUser();
            var _ObjLst = _users.GetAll<AspNetUser>().ToList();
            _obj.CreatedBy = _ObjLst.FirstOrDefault().Id;

            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("_Create", _obj) }, JsonRequestBehavior.AllowGet);
        }
        private void fillcreatebag(int patientId)
        {
            RadiologyResultVM _conditionVM = new RadiologyResultVM();
            ViewBag.SiteSelectList = _conditionVM.GeRadiologySiteSelectList();
            ViewBag.ProcedureTypeList = _conditionVM.GetProcedureTypeSelectList();
            ViewBag.RadiologyRacist = _conditionVM.GetRadiologyRacistSelectList();
            fillBag(patientId);
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

        //[HttpPost]
        //public string Create(RadiologyResultVM vm)
        //{
        //    try
        //    {
        //        Guid userId = Guid.Parse(User.Identity.GetUserId());
        //        vm.CreatedBy = userId;
        //        vm.Create();
        //        return "Success"; // succcess
        //    }
        //    catch (Exception ex)
        //    {

        //        return "Error500";

        //    }
        //}
        [HttpPost]
        public string Create(int ProcedureType, int Site, DateTime RadiologyDate, int Recist, string T, string M, string N, string Note, bool Contrast, int PateintID)
        {
            try
            {
                RadiologyResultVM RadiologyObject = new RadiologyResultVM();
                RadiologyObject.ProcedureType = ProcedureType;
                RadiologyObject.Site = Site;
                RadiologyObject.RadiologyDate = RadiologyDate;
                RadiologyObject.Recist = Recist;
                RadiologyObject.T = T;
                RadiologyObject.M = M;
                RadiologyObject.N = N;
                RadiologyObject.Note = Note;
                RadiologyObject.Contrast = Contrast;
                RadiologyObject.PateintID = PateintID;
                Guid userId = Guid.Parse(User.Identity.GetUserId());
                RadiologyObject.CreatedBy = userId;
                RadiologyObject.Create();
                return "Success"; // succcess
            }
            catch (Exception ex)
            {

                return "Error500";

            }
        }
     }
}