
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
        public ActionResult Index(int Id, int pid)
        {
            //
           ChemoTherapyCyclePackageVM _Obj = new ChemoTherapyCyclePackageVM();

          


            List<ChemoTherapyCyclePackageVM> LabList = _Obj.SelectAllByCycleID(Id);


            ChemoTherapyCycleDayVM _dObj = new ChemoTherapyCycleDayVM();
            ChemoTherapyCycleDayVM _cObjVM = _dObj.SelectObject(Id);

            //

            int patientID = Convert.ToInt32(pid);
         

            int TemplateID = Convert.ToInt32(_cObjVM.TemplateId);
   
            ChemoTherapyTemplateVM _Objt = new ChemoTherapyTemplateVM();
            ChemoTherapyTemplateVM _objVMt = _Objt.SelectObject(TemplateID);
            fillBag(Id, pid);
           

            var tuple = new Tuple<ChemoTherapyTemplateVM, List<ChemoTherapyCyclePackageVM>>(_objVMt, LabList);
            return View(tuple);

        }

    
    
  




        public ActionResult Test(int Id , int pid)
        {
            //  int _patientID = Convert.ToInt32(patientID);
            //fillCreateBag(Id);
            ChemoTherapyCyclePackageVM radioTherapyVMVM = new ChemoTherapyCyclePackageVM();
            radioTherapyVMVM.Cycle_ID = Id;
            radioTherapyVMVM.Patient_ID = pid;
            return View(radioTherapyVMVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Test(ChemoTherapyCyclePackageVM input)
        {

            if (ModelState.IsValid)
            {
                input.Create();
                return RedirectToAction("Index", new { Id = input.Cycle_ID , pid = input.Patient_ID});
            }

            return View(input);
        }


        public JsonResult CreateLabPackage(int CycleId, int PatientId)
        {
            if (PatientId == null && CycleId == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);


            }
            fillCreateBag(PatientId, CycleId);
            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("CreateLabPackage", null) }, JsonRequestBehavior.AllowGet);

            //  return View();

        }
        public JsonResult CreateLab(int CycleId, int PatientId)
        {
            if (PatientId == null && CycleId == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);


            }
            fillCreateBag(PatientId, CycleId);
            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("CreateLab", null) }, JsonRequestBehavior.AllowGet);

            //  return View();

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
 
     
        private void fillCreateBag(int CycleId, int PatientId)
        {
            ChemoTherapyCyclePackageVM results = new ChemoTherapyCyclePackageVM();
            ViewBag.LabTypesList = results.GeLabTypsSelectList();
            ViewBag.RuleTypesList = results.GetRuleTypsSelectList();
            PackageVM resultss = new PackageVM();
            ViewBag.PackagesList = resultss.PackageSelectList();
           
            fillBag(PatientId, CycleId);


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

        public ActionResult CreateLabPackage(ChemoTherapyCyclePackageVM vm)
        {
            try
            {

                vm.Patient_ID = vm.Patient_ID;
              
                vm.Create();

        
                return RedirectToAction("Error500");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error500");
            }
        }
        public JsonResult AddResult(int CycleId, int PatientId, int Test_Type)

        {


            int _PackageId = Convert.ToInt32(Test_Type);
           

            LabPackageVM ObjVm = new LabPackageVM();
            List<LabPackageVM> _list = ObjVm.SelectAllByPackageID(_PackageId);
            foreach (var itemm3 in _list)
            {

                ChemoTherapyCyclePackageVM cc = new ChemoTherapyCyclePackageVM();
                cc.Cycle_ID = CycleId;
                
                cc.Test_Type = itemm3.Test;
                cc.Test_Value = itemm3.Value;
                cc.Rule_Type = itemm3.Rule;
               cc.Patient_ID = PatientId;
       
                cc.Create();

            }








            return Json(new { IsRedirect = true }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult AddLabResult(int CycleId, int PatientId, int Test_Type,int Test_Value)




        { 

                ChemoTherapyCyclePackageVM cc = new ChemoTherapyCyclePackageVM();

                cc.Cycle_ID = CycleId;

                cc.Test_Type = Test_Type;
                cc.Test_Value = Test_Value;
                cc.Rule_Type= 5;
                cc.Patient_ID = PatientId;
                  
                cc.Create();

            








            return Json(new { IsRedirect = true }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Toxicty(int Id , string pid)
        {
            int _patientID = Convert.ToInt32(pid);
            fillBag(Id, _patientID);

            ToxictyVM ObjVm = new ToxictyVM();
            List<ToxictyVM> _list = ObjVm.SelectAllByPatientId(_patientID);
            return View(_list);
        }

        public ActionResult NurseNote(int Id, string pid)
        {
            int _patientID = Convert.ToInt32(pid);
        
            NurseNoteVM ObjVm = new NurseNoteVM();
            fillBag(Id, _patientID);
            List<NurseNoteVM> _list = ObjVm.SelectAllByPatientId(_patientID);
            return View(_list);
        }



        public ActionResult NCreate(int Id, string pid)
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
        public ActionResult NCreate(NurseNoteVM input)
        {

            if (ModelState.IsValid)
            {
                input.Date = DateTime.Now;


                // Guid userId = Guid.Parse(User.Identity.GetUserId());
                //double x = Convert.ToDouble((input.Weight * input.Height) / 3600);
                var x = (input.Weight * input.Height) / 3600;
                input.SA = Math.Sqrt(Convert.ToDouble(x));
                input.Create();
                return RedirectToAction("NurseNote", new { Id = input.CycleId, pid = input.Patient_ID });
            }

            return View(input);
        }



        public JsonResult EditLabValue( int CycleId, int pid)
        {
            ChemoTherapyCyclePackageVM _Obj = new ChemoTherapyCyclePackageVM();


            List<ChemoTherapyCyclePackageVM> LabList = _Obj.SelectAllByCycleID(CycleId);
            foreach(var itemm in LabList)
            {
                ChemoTherapyCyclePackageVM Objvm = _Obj.SelectObject(itemm.ID);
                Objvm.Edit();
            }
            return Json(new { IsRedirect = true }, JsonRequestBehavior.AllowGet);


        }



    }
}

