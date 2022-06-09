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
    public class ChemoTherapyController : Controller
    {
        // GET: ChemoTherapy
        public ActionResult Index(string patientID)
        {
            int _patientID = Convert.ToInt32(patientID);
            fillBag(_patientID);
            ChemoTherapyTemplateVM ObjVm = new ChemoTherapyTemplateVM();
            List <ChemoTherapyTemplateVM> _list = ObjVm.SelectAll();

            return View(_list);
        }

        private void fillBag(int patientID)
        {
            ViewBag.PatientId = patientID;
            ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(patientID);
           

            var userID = User.Identity.GetUserId();
            ViewBag.DoctorId = userID;

        }

        public ActionResult View(string patientID , string templateID)
        {
            int PatientId = Convert.ToInt32(patientID);
            int TemplateID = Convert.ToInt32(templateID);
            fillBag(PatientId);
            ChemoTherapyTemplateVM _Obj = new ChemoTherapyTemplateVM();
            ChemoTherapyTemplateVM _objVM = _Obj.SelectObject(TemplateID);

            return View(_objVM);

        }
        public ActionResult ApplyTemplate(string patientID, string templateID)
        {

            //
            int PatientId = Convert.ToInt32(patientID);
            fillBag(PatientId);
            int TemplateID = Convert.ToInt32(templateID);
            //
            PatientVM _pObj = new PatientVM();
            PatientVM _pObjVM = _pObj.SelectObject(PatientId);
            _pObjVM.ChemoTherapyId = TemplateID;
            _pObjVM.Edit();
            //
            ChemoTherapyTemplateVM _Obj = new ChemoTherapyTemplateVM();
            ChemoTherapyTemplateVM _objVM = _Obj.SelectObject(TemplateID);
            //

            //
            if (_pObjVM.cycleDates == null) { 
            DateTime date = new DateTime(2015, 06, 27);

            List<DateTime> dates = new List<DateTime>();
            dates.Add(date);
            for (int i = 1; i < _objVM.Maximum_cycles; i++)
            {
                double x = Convert.ToDouble(_objVM.Frequency);
                DateTime newDate = date.AddDays(x);
                dates.Add(newDate);
                date = newDate;

            }

            _pObjVM.cycleDates = dates;
            _pObjVM.Edit();
                //
                var tuple = new Tuple<ChemoTherapyTemplateVM, PatientVM>(_objVM, _pObjVM);
                return View(tuple);

            }
            else {
                var tuple = new Tuple<ChemoTherapyTemplateVM, PatientVM>(_objVM, _pObjVM);
                return View(tuple);
            }
        
         
            
        }


        public ActionResult Test(string patientID, string templateID)
        {

            //
            int PatientId = Convert.ToInt32(patientID);
            fillBag(PatientId);
            int TemplateID = Convert.ToInt32(templateID);
            //
            PatientVM _pObj = new PatientVM();
            PatientVM _pObjVM = _pObj.SelectObject(PatientId);
            _pObjVM.ChemoTherapyId = TemplateID;
            _pObjVM.Edit();
            //
            ChemoTherapyTemplateVM _Obj = new ChemoTherapyTemplateVM();
            ChemoTherapyTemplateVM _objVM = _Obj.SelectObject(TemplateID);
            DateTime date = new DateTime(2015, 06, 27);
            _objVM.Admin_Date = date;
            _objVM.Edit();
            List<DateTime> dates = new List<DateTime>();
            dates.Add(date);
            for (int i = 1; i < _objVM.Maximum_cycles; i++)
            {
                double x = Convert.ToDouble(_objVM.Frequency);
                DateTime newDate = date.AddDays(x);
                dates.Add(newDate);
                date = newDate;

            }
            _objVM.cycleDates = dates;

            _objVM.Edit();




            return View(_objVM);

        }



        [HttpPost]
        public string EditDatePatient(PatientVM vm)
        {
            try
            {
                PatientVM _Obj = new PatientVM();
                PatientVM _objVM = _Obj.SelectObject(vm.Id);
                _objVM.StartDate = vm.StartDate;

                _objVM.ModifiedDate = DateTime.Now;
                _objVM.Edit();
                return "Success"; // succcess
            }
            catch (Exception ex)
            {

                return "Error500";

            }
        }


        public JsonResult _EditDate(int patientID)
        {
            if (patientID == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
          

            PatientVM _Obj = new PatientVM();
            PatientVM _objVM = _Obj.SelectObject(patientID);
            if (_objVM == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("InsertDate", _objVM) }, JsonRequestBehavior.AllowGet);
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

    }
}



