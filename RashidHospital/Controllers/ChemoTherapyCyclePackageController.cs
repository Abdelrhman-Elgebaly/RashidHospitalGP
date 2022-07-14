
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
            fillBag(patientID);
            fillBag2(Id);


          



            var tuple = new Tuple<ChemoTherapyTemplateVM, List<ChemoTherapyCyclePackageVM>>(_objVMt, LabList);
            return View(tuple);

        }

    
    
    void Edit(int Id, int pid )
        {
            /*   ChemoTherapyCyclePackageVM _Obj = new ChemoTherapyCyclePackageVM();
             List<ChemoTherapyCyclePackageVM> LabList = _Obj.SelectAllByCycleID(Id);
            foreach (var itemm in LabList)
             {
                 ChemoTherapyCyclePackageVM _objVMt = _Obj.SelectObject(itemm.ID);
                 _objVMt.Edit();
             }

             return RedirectToAction("Index", new { Id = Id, pid = pid });
             */


            ChemoTherapyCyclePackageVM _Obj = new ChemoTherapyCyclePackageVM();

            
                ChemoTherapyCyclePackageVM _objVMt = _Obj.SelectObject(Id);
                _objVMt.Edit();
            
          
        }


        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(List<ChemoTherapyCyclePackageVM> input)
        {
            foreach (var itemm in input)
            {
                if (ModelState.IsValid)
                {
                    itemm.Edit();
                    return RedirectToAction("Index", new { Id = itemm.Cycle_ID, pid = itemm.Patient_ID });
                }
            }
            return View(input);
        }
        */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ChemoTherapyCyclePackageVM input)
        {
            
                if (ModelState.IsValid)
                {
                input.Edit();
                return RedirectToAction("Index", new { Id = input.Cycle_ID, pid = input.Patient_ID });
            }

            return View(input);
        }


        public ActionResult Test(int Id , int pid)
        {
            //  int _patientID = Convert.ToInt32(patientID);
            fillCreateBag(Id);
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
                vm.Cycle_ID = vm.Cycle_ID;
               
              
                vm.Create();

                //PatientId
                return RedirectToAction("Index", new { Id = vm.Cycle_ID });
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

        public ActionResult Investigation(int Id, int pid)
        {
            //
            ChemoTherapyCyclePackageVM _Obj = new ChemoTherapyCyclePackageVM();
            List<ChemoTherapyCyclePackageVM> LabList = _Obj.SelectAllByCycleID(Id);





            //

            int patientID = Convert.ToInt32(pid);
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

        public ActionResult Toxicty(string patientID)
        {
            int _patientID = Convert.ToInt32(patientID);
            fillBag(_patientID);
            ToxictyVM ObjVm = new ToxictyVM();
            List<ToxictyVM> _list = ObjVm.SelectAllByPatientId(_patientID);
            return View(_list);
        }

        public ActionResult NurseNote(string patientID)
        {
            int _patientID = Convert.ToInt32(patientID);
            fillBag(_patientID);
            NurseNoteVM ObjVm = new NurseNoteVM();

            List<NurseNoteVM> _list = ObjVm.SelectAllByPatientId(_patientID);
            return View(_list);
        }



        public ActionResult NCreate(string patientID)
        {
            int _patientID = Convert.ToInt32(patientID);
            NurseNoteVM nurseNoteVM = new NurseNoteVM();
            nurseNoteVM.Patient_ID = _patientID;
            fillBag(_patientID);

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
                return RedirectToAction("NurseNote", new { patientID = input.Patient_ID });
            }

            return View(input);
        }







    }
}

