
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

          //  GetProtocolPreLab(Id);
           
           ChemoTherapyCyclePackageVM _Obj = new ChemoTherapyCyclePackageVM();

            List<ChemoTherapyCyclePackageVM> LabList = _Obj.SelectAllByCycleID(Id);


            ChemoTherapyCycleDayVM _dObj = new ChemoTherapyCycleDayVM();
            ChemoTherapyCycleDayVM _cObjVM = _dObj.SelectObject(Id);
            _cObjVM.IsStart = true;
            _cObjVM.IsRescuedeled = false;
            _cObjVM.Edit();
            //

            int patientID = Convert.ToInt32(pid);
         

            int TemplateID = Convert.ToInt32(_cObjVM.TemplateId);
   
            ChemoTherapyTemplateVM _Objt = new ChemoTherapyTemplateVM();
            ChemoTherapyTemplateVM _objVMt = _Objt.SelectObject(TemplateID);
            fillBag(Id, pid);
           

            var tuple = new Tuple<ChemoTherapyTemplateVM, List<ChemoTherapyCyclePackageVM>>(_objVMt, LabList);
            return View(tuple);

        }

    
        public JsonResult GetProtocolPreLab(int Id )
        {



            ChemoTherapyCyclePackageVM _Obj = new ChemoTherapyCyclePackageVM();
            List<ChemoTherapyCyclePackageVM> LabList = _Obj.SelectAllByCycleID(Id);
            /*
            foreach (var item in LabList)
            {
                if (item.PreProtocol == 1 ) {
                item.Delete();
                }
            }
          */
            ChemoTherapyCycleDayVM _dObj = new ChemoTherapyCycleDayVM();
            ChemoTherapyCycleDayVM _cObjVM = _dObj.SelectObject(Id);



            ChemoTherapyProtocolVM _cObj = new ChemoTherapyProtocolVM();
            ChemoTherapyProtocolVM _cobjVM = _cObj.SelectObject(_cObjVM.TemplateId);


            int TemplateID = Convert.ToInt32(_cobjVM.Template_ID);

            ChemoTherapyTemplateVM _Objt = new ChemoTherapyTemplateVM();
            ChemoTherapyTemplateVM _objVMt = _Objt.SelectObject(TemplateID);






            ChemoTherapyPreLabVM ObjVm = new ChemoTherapyPreLabVM();
                    List<ChemoTherapyPreLabVM> _list = ObjVm.SelectAllByTemplateID(_objVMt.Template_ID);

            foreach (var item in LabList)
            {
                foreach (var itemm3 in _list)


                {


                    if (item.Test_Type != itemm3.Test_Type) { 
                    ChemoTherapyCyclePackageVM cc = new ChemoTherapyCyclePackageVM();
                    cc.Cycle_ID = Id;
                    cc.Actual_Value = 7;
                    cc.Test_Type = itemm3.Test_Type;
                    cc.Test_Value = itemm3.Value;
                    cc.Rule_Type = itemm3.Rule_Type;
                    cc.PreProtocol = 1;

                    cc.Create();
                    }
                }

            }



       




                    return Json(new { IsRedirect = true }, JsonRequestBehavior.AllowGet);

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

        public JsonResult _EditNote(int Id)
        {
            ViewBag.Id = Id;
            if (Id == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            ViewBag.Id = Id;

            ChemoTherapyCyclePackageVM _Obj = new ChemoTherapyCyclePackageVM();
            ChemoTherapyCyclePackageVM _ObjvM = _Obj.SelectObject(Id);
            //if (_objVM == null)
            //{
            //    return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);
            //}

            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("_EditNote", _ObjvM) }, JsonRequestBehavior.AllowGet);
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
                List<ChemoTherapyCyclePackageVM> LabList = cc.SelectAllByCycleID(CycleId);

                //Prevent Duplications

                int count = 0;
                foreach (var item in LabList)
                {
                    if (itemm3.Test == item.Test_Type)
                    {
                        count = 1;
                    }

                }

                if (count == 0)
                {
                    cc.Cycle_ID = CycleId;
                cc.Actual_Value = 0;
                cc.Test_Type = itemm3.Test;
                cc.Test_Value = itemm3.Value;
                cc.Rule_Type = itemm3.Rule;
               cc.Patient_ID = PatientId;
       
                cc.Create();
                }
            }








            return Json(new { IsRedirect = true }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult AddLabResult(int CycleId, int PatientId, int Test_Type, double Test_Value)

{

            string noResult = "Search Result Not Found";

            //Prevent Duplications

            ChemoTherapyCyclePackageVM cc = new ChemoTherapyCyclePackageVM();
            List<ChemoTherapyCyclePackageVM> LabList = cc.SelectAllByCycleID(CycleId);

            int count = 0;
            foreach (var item in LabList){ 
                if (Test_Type == item.Test_Type)
                {
                    count = 1;
                }
                
            }
            if(count == 0) {

                cc.Cycle_ID = CycleId;
                cc.Actual_Value = 0;
                cc.Rule_Type = 5;
                cc.Rule_TypeValue = null;

                cc.Test_Type = Test_Type;
                cc.Test_Value = Test_Value;

                cc.Patient_ID = PatientId;

                cc.Create();
            }





            if (count == 1)
            {
                ViewBag.Message = noResult;


            }


            return Json(new { IsRedirect = true }, JsonRequestBehavior.AllowGet);
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

        public JsonResult AddNote(int Id, string Note, double Actual_Value)

        {

            ChemoTherapyCyclePackageVM _Obj = new ChemoTherapyCyclePackageVM();
            ChemoTherapyCyclePackageVM _ObjvM = _Obj.SelectObject(Id);
            _ObjvM.Note = Note;
            _ObjvM.Actual_Value = Actual_Value;
            _ObjvM.Edit();
            if(User.IsInAnyRoles("Pharmacist"))
                {
                _ObjvM.IsEditByPharmacy = true;
                _ObjvM.Edit();
            }

            if (User.IsInAnyRoles("Doctor"))
            {
                _ObjvM.IsEditByPharmacy = false;
                _ObjvM.Edit();
            }
            _ObjvM.Edit();
            return Json(new { IsRedirect = true }, JsonRequestBehavior.AllowGet);

        }

    }
}

