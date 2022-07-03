
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
    public class ChemoTherapyCyclePackageController : Controller
    {
        // GET: ChemoTherapyCyclePackage
        public ActionResult Index(int Id)
        {
            //
           ChemoTherapyCyclePackageVM _Obj = new ChemoTherapyCyclePackageVM();
            List<ChemoTherapyCyclePackageVM> LabList = _Obj.SelectAllByCycleID(Id);





            //
            ChemoTherapyCycleDayVM _cObj = new ChemoTherapyCycleDayVM();
            ChemoTherapyCycleDayVM _objVM = _cObj.SelectObject(Id);
            int patientID = Convert.ToInt32(_objVM.Patient_ID);
            PatientVM _pObj = new PatientVM();
            PatientVM _pObjVM = _pObj.SelectObject(patientID);

            int TemplateID = Convert.ToInt32(_pObjVM.ChemoTherapyId);
   
            ChemoTherapyTemplateVM _Objt = new ChemoTherapyTemplateVM();
            ChemoTherapyTemplateVM _objVMt = _Objt.SelectObject(TemplateID);
            fillBag(patientID);
            fillBag2(Id);

            var tuple = new Tuple<ChemoTherapyTemplateVM, List<ChemoTherapyCyclePackageVM>>(_objVMt, LabList);
            return View(tuple);


           

        }


        private void fillBag(int patientID )
        {
            PatientVM _patientvm = new PatientVM();
            ViewBag.PatientId = patientID;
          
            ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(patientID);
            var userID = User.Identity.GetUserId();
            ViewBag.DoctorId = userID;

        }
        private void fillBag2( int CycleId)
        {
        
            ViewBag.CycleId = CycleId;
     
        }
        public JsonResult Create(int Id)
        {
            if (Id == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);


            }
            fillCreateBag(Id);
            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("Create", null) }, JsonRequestBehavior.AllowGet);

            //  return View();

        }
        private void fillCreateBag(int Id)
        {
            ChemoTherapyCyclePackageVM results = new ChemoTherapyCyclePackageVM();
            ViewBag.LabTypesList = results.GeLabTypsSelectList();
            ViewBag.RuleTypesList = results.GetRuleTypsSelectList();
            fillBag2(Id);

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

        public ActionResult Create(ChemoTherapyCyclePackageVM vm)
        {
            try
            {
              //  vm.Cycle_ID = vm.Cycle_ID;
                //   vm.ResualtDate = DateTime.Now;
               
                vm.Create();


                return RedirectToAction("Error500");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error500");
            }
        }
        [HttpPost]
        public JsonResult AddResult(int Test_Type, int Test_Value, int Cycle_ID, int Rule_Type)
        {
           
                ChemoTherapyCyclePackageVM _labresults = new ChemoTherapyCyclePackageVM();

             
                _labresults.Test_Type = Test_Type;
                _labresults.Test_Value = Test_Value;
                _labresults.Cycle_ID = Cycle_ID;
                _labresults.Rule_Type = Rule_Type;
               
                _labresults.Create();
          

            return Json(new { IsRedirect = true }, JsonRequestBehavior.AllowGet);
        }




    }
}

