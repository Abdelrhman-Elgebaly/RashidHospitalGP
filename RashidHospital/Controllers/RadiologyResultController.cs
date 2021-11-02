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
    [Authorize(Roles = "Doctor, Residents,Admin,Assistant lecturer,Consultant,Nurses,physician,Employee,Assistant,Pharmacist")]

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
            ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(patientID);
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
        public JsonResult Create(int ProcedureType, int Site,DateTime? RadiologyDate, int Recist, string T, string M, string N, string Note, bool Contrast, int PateintID)
        {
            try
            {
                RadiologyResultVM RadiologyObject = new RadiologyResultVM();
                RadiologyObject.ProcedureType = ProcedureType;
                RadiologyObject.Site = Site;


                if (RadiologyDate == null)
                {
                    RadiologyObject.RadiologyDate = DateTime.Now;

                }
                else {
                    RadiologyObject.RadiologyDate = RadiologyDate.Value;
                }
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
                return Json(new { IsRedirect = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult _Edit(int Id)
        {
            if (Id == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            RadiologyResultVM _obj = new RadiologyResultVM();
            _obj = _obj.SelectObject(Id);
            fillcreatebag(_obj.PateintID);


            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("_Edit", _obj) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public string Edit(RadiologyResultVM vm)
        {
            try
            {
                Guid userId = Guid.Parse(User.Identity.GetUserId());
                vm.CreatedBy = userId;
                vm.Edit();
                return "Success"; // succcess
            }
            catch (Exception ex)
            {

                return "Error500";

            }
        }
        [HttpGet]
        public ActionResult View(string Id)
        {
            if (Id == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            
            RadiologyResultVM _Obj = new RadiologyResultVM();
            RadiologyResultVM _objVM = _Obj.SelectObject(Convert.ToInt32(Id));
            int PatientId = Convert.ToInt32(_objVM.PateintID);
            fillcreatebag(PatientId);

            fillBag(PatientId);
            if (_objVM == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            return View(_objVM);

        }
    }
}