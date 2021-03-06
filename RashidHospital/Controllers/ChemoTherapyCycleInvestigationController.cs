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
    public class ChemoTherapyCycleInvestigationController : Controller
    {
        // GET: ChemoTherapyCycleInvestigation
        public ActionResult Index(int Id, int pid)
        {



        //    GetProtocolPreInves(Id, pid);
           
            ChemoTherapyCycleInvestigationVM _Obj = new ChemoTherapyCycleInvestigationVM();




            List<ChemoTherapyCycleInvestigationVM> LabList = _Obj.SelectAllByCycleID(Id);


            ChemoTherapyCycleDayVM _dObj = new ChemoTherapyCycleDayVM();
            ChemoTherapyCycleDayVM _cObjVM = _dObj.SelectObject(Id);

            //

            int patientID = Convert.ToInt32(pid);


            int TemplateID = Convert.ToInt32(_cObjVM.TemplateId);

            ChemoTherapyTemplateVM _Objt = new ChemoTherapyTemplateVM();
            ChemoTherapyTemplateVM _objVMt = _Objt.SelectObject(TemplateID);
            fillBag(patientID);
            fillBag2(Id);






            var tuple = new Tuple<ChemoTherapyTemplateVM, List<ChemoTherapyCycleInvestigationVM>>(_objVMt, LabList);
            return View(tuple);

        }

        private void fillBag(int patientID)
        {
            PatientVM _patientvm = new PatientVM();
            ViewBag.PatientId = patientID;

            ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(patientID);
            var userID = User.Identity.GetUserId();
            ViewBag.DoctorId = userID;

        }
        private void fillBag2(int CycleId)
        {

            ViewBag.CycleId = CycleId;

        }



        public JsonResult GetProtocolPreInves(int Id,int pid)
        {



            ChemoTherapyCycleInvestigationVM _Obj = new ChemoTherapyCycleInvestigationVM();
            List<ChemoTherapyCycleInvestigationVM> LabList = _Obj.SelectAllByCycleID(Id);

            foreach (var item in LabList)
            {
                item.Delete();
            }

            ChemoTherapyCycleDayVM _dObj = new ChemoTherapyCycleDayVM();
            ChemoTherapyCycleDayVM _cObjVM = _dObj.SelectObject(Id);



            ChemoTherapyProtocolVM _cObj = new ChemoTherapyProtocolVM();
            ChemoTherapyProtocolVM _cobjVM = _cObj.SelectObject(_cObjVM.TemplateId);


            int TemplateID = Convert.ToInt32(_cobjVM.Template_ID);

            ChemoTherapyTemplateVM _Objt = new ChemoTherapyTemplateVM();
            ChemoTherapyTemplateVM _objVMt = _Objt.SelectObject(TemplateID);






            ChemoTherapyPreInvestigationsVM ObjVmi = new ChemoTherapyPreInvestigationsVM();
            List<ChemoTherapyPreInvestigationsVM> _list = ObjVmi.SelectAllByTemplateID(_objVMt.Template_ID);
            foreach (var itemm4 in _list)
            {

        


                ChemoTherapyCycleInvestigationVM cc = new ChemoTherapyCycleInvestigationVM();
                cc.Cycle_ID = Id;
                // cc.Actual_Value = 0;
                cc.Inves_Type = itemm4.Test_Name;
                cc.Value = itemm4.Value;
                cc.Rule_Type = itemm4.Rule_Type;
                cc.Patient_ID = pid;
               
                cc.Create();


            }










            return Json(new { IsRedirect = true }, JsonRequestBehavior.AllowGet);

        }





        public JsonResult _EditNote(int Id)
        {
            ViewBag.Id = Id;
            if (Id == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            ViewBag.Id = Id;

            ChemoTherapyCycleInvestigationVM _Obj = new ChemoTherapyCycleInvestigationVM();
            ChemoTherapyCycleInvestigationVM _ObjvM = _Obj.SelectObject(Id);
            //if (_objVM == null)
            //{
            //    return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);
            //}

            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("_EditNote", _ObjvM) }, JsonRequestBehavior.AllowGet);
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

        public JsonResult AddNote(int Id, string Note, int Actual_Value)

        {

            ChemoTherapyCycleInvestigationVM _Obj = new ChemoTherapyCycleInvestigationVM();
            ChemoTherapyCycleInvestigationVM _ObjvM = _Obj.SelectObject(Id);
            _ObjvM.Note = Note;
            _ObjvM.Actual_Value = Actual_Value;
            _ObjvM.Edit();
            return Json(new { IsRedirect = true }, JsonRequestBehavior.AllowGet);

        }


    }
}