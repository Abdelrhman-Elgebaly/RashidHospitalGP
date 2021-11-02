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
    public class RadioTherapyController : Controller
    {
        // GET: RadioTherapy
        public ActionResult Index(string patientID)
        {
            int _patientID = Convert.ToInt32(patientID);
            fillBag(_patientID);
            RadioTherapyVM ObjVm = new RadioTherapyVM();
            List<RadioTherapyVM> _list = ObjVm.SelectAllByPatientId(_patientID);
            return View(_list);
        }
        public ActionResult Create(string patientID)
        {
            int _patientID = Convert.ToInt32(patientID);
            RadioTherapyVM radioTherapyVMVM = new RadioTherapyVM();
            radioTherapyVMVM.PatientID = _patientID;
            fillBag(_patientID);
            ddlViewBags();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RadioTherapyVM input)
        {
            if (ModelState.IsValid)
            {
                input.CreatedDate = DateTime.Now;
                input.ModifiedDate = DateTime.Now;
                Guid userId = Guid.Parse(User.Identity.GetUserId());
                input.DoctorId = userId;
                input.Create();
                return RedirectToAction("Index", new { patientID = input.PatientID });
            }
            else
            {
                return View(input);

            }

        }
       
        private void fillBag(int patientID)
        {
            ViewBag.PatientId = patientID;
            ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(patientID);
            var userID = User.Identity.GetUserId();
            ViewBag.DoctorId = userID;

        }
        [HttpPost]
        public int Delete(int toxictyID)
        {
            int finalResult = 0;
            try
            {
                RadioTherapyVM _VMObject = new RadioTherapyVM();
                RadioTherapyVM _obj = _VMObject.SelectObject(toxictyID);
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

        public JsonResult _Edit(int Id)
        {
            if (Id == 0)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            RadioTherapyVM _obj = new RadioTherapyVM();
            _obj = _obj.SelectObject(Id);
            fillBag(_obj.PatientID);
            ddlViewBags();
            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("_Edit", _obj) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public string Edit(RadioTherapyVM vm)
        {
            try
            {
                Guid userId = Guid.Parse(User.Identity.GetUserId());
                vm.ModifiedBy = userId;
                vm.ModifiedDate = DateTime.Now;
                vm.Edit();
                return "Success"; // succcess
            }
            catch (Exception ex)
            {

                return "Error500";

            }
        }

        private void ddlViewBags()
        {
            ViewBag.RTXSite = System.Enum.GetValues(typeof(RTXSite)).Cast<RTXSite>().Select(v => new SelectListItem
            {
                Text = EnumHelper<RTXSite>.GetDisplayValue(v),
                Value = ((int)v).ToString()
            }).ToList();

            
            ViewBag.RTXType = System.Enum.GetValues(typeof(RTXType)).Cast<RTXType>().Select(v => new SelectListItem
            {
                Text = EnumHelper<RTXType>.GetDisplayValue(v),
                Value = ((int)v).ToString()
            }).ToList();

            ViewBag.TypeOfTechnique = System.Enum.GetValues(typeof(TypeOfTechnique)).Cast<TypeOfTechnique>().Select(v => new SelectListItem
            {
                Text = EnumHelper<TypeOfTechnique>.GetDisplayValue(v),
                Value = ((int)v).ToString()
            }).ToList();

            FixationVM fixationVM = new FixationVM();
            ViewBag.Fixation = fixationVM.GetFixtionSelectList();

        }
        [HttpGet]
        public ActionResult View(string id)
        {
            if (id == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);
            }
            RadioTherapyVM _EditObject = new RadioTherapyVM();
            RadioTherapyVM retObject = _EditObject.SelectObject(Convert.ToInt32(id));
            int _patientID = Convert.ToInt32(retObject.PatientID);
            fillBag(_patientID);
            ddlViewBags();
            return View(retObject);

        }

    }
}