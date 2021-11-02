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

    public class RadiologyRequestController : Controller
    {
        // GET: RadiologyRequest
        public ActionResult Index(string patientID)
        {
            int _patientID = Convert.ToInt32(patientID);
            fillBag(_patientID);
            RadiologyRequestVM ObjVm = new RadiologyRequestVM();
            List<RadiologyRequestVM> _list = ObjVm.SelectAllByPatientId(_patientID);
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
                RadiologyRequestVM _VMObject = new RadiologyRequestVM();
                RadiologyRequestVM _obj = _VMObject.SelectObject(RequestID);
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
            RadiologyRequestVM _obj = new RadiologyRequestVM();
            _obj.PateintID = patientID;
            _obj.RequestDate = DateTime.Now;
            fillcreatebag(patientID);
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            _obj.CreatedBy = userId;
        
            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("_Create", _obj) }, JsonRequestBehavior.AllowGet);
        }
        private void fillcreatebag(int patientId) {
            RadiologyRequestVM _conditionVM = new RadiologyRequestVM();
            ViewBag.SiteSelectList = _conditionVM.GeRadiologySiteSelectList();
            ViewBag.ProcedureTypeList = _conditionVM.GetProcedureTypeSelectList();
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

        //public string Create(RadiologyRequestVM vm)
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
        public JsonResult Create(int ProcedureType, int Site,DateTime? RequestDate, string Note , bool Contrast,  int PateintID)
        {
            try
            {
                RadiologyRequestVM RequestObject = new RadiologyRequestVM();
                Guid userId = Guid.Parse(User.Identity.GetUserId());
                RequestObject.ProcedureType = ProcedureType;
                RequestObject.Site = Site;

                if (RequestDate == null)
                {
                    RequestObject.RequestDate = DateTime.Now;

                }
                else {
                    RequestObject.RequestDate = RequestDate.Value;
                }
                RequestObject.Note = Note;
                RequestObject.Contrast = Contrast;
                RequestObject.PateintID = PateintID;
                RequestObject.CreatedBy = userId;
                RequestObject.Create();
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
            RadiologyRequestVM _obj = new RadiologyRequestVM();
           _obj= _obj.SelectObject(Id);
            fillcreatebag(_obj.PateintID);
          

            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("_Edit", _obj) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public string Edit(RadiologyRequestVM vm)
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

        //View?Id
        [HttpGet]
        public ActionResult View(string Id)
        {
            if (Id == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }

            RadiologyRequestVM _Obj = new RadiologyRequestVM();
            RadiologyRequestVM _objVM = _Obj.SelectObject(Convert.ToInt32(Id));
           // fillBag(_objVM.PateintID);
            fillcreatebag(_objVM.PateintID);

            if (_objVM == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            return View(_objVM);

        }

    }
}