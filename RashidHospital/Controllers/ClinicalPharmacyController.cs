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
    public class ClinicalPharmacyController : Controller
    {
        // GET: ClinicalPharmacy
        public ActionResult IndexCurrent()
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());


            ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
            List<ChemoTherapyCycleDayVM> _List = _Obj.SelectAllReleased().Where(a =>  a.PharmacistId == userId || a.PharmacistId == null && a.Date == DateTime.Now).ToList();


            return View(_List);
        }

        public ActionResult IndexCurrentAll()
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());

            ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
            List<ChemoTherapyCycleDayVM> _List = _Obj.SelectAllReleased().Where(a => a.PharmacistId == userId || a.PharmacistId == null).ToList();



            return View(_List);
        }
        public ActionResult Pending()
        {

            Guid userId = Guid.Parse(User.Identity.GetUserId());

            ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
            List<ChemoTherapyCycleDayVM> _List = null;
            if (User.IsInAnyRoles("Doctor"))
            {

                 _List = _Obj.SelectAllPending().Where(a => a.Date == DateTime.Now && a.DoctorId == userId).ToList();
            }
            
             else if (User.IsInAnyRoles("Pharmacist"))

            {

                _List = _Obj.SelectAllPending().Where(a => a.Date == DateTime.Now && a.PharmacistId == userId).ToList();
            }
            return View(_List);
        }

        public ActionResult PendingAll()
        {

            Guid userId = Guid.Parse(User.Identity.GetUserId());


            ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
            List<ChemoTherapyCycleDayVM> _List = null;

            if (User.IsInAnyRoles("Doctor"))
            {

                _List = _Obj.SelectAllPending().Where(a => a.DoctorId == userId).ToList();
            }

            else if (User.IsInAnyRoles("Pharmacist"))

            {

                _List = _Obj.SelectAllPending().Where(a =>  a.PharmacistId == userId).ToList();
            }


            return View(_List);
        }

        public ActionResult FinalApproved()
        {
            PatientDoseVM _Obj = new PatientDoseVM();
            List<PatientDoseVM> _List = _Obj.SelectAllFinalApproved();



            return View(_List);
        }

        public ActionResult ClinicalPharmacy()
        {


            PatientDoseVM _Obj = new PatientDoseVM();
            List<PatientDoseVM> _List = _Obj.SelectAllFinalApproved().Where(a => a.Date == DateTime.Now).ToList();

            return View(_List);
        }



        public ActionResult ClinicalPharmacyAll()
        {

            /*
                   PatientDoseVM _Obj = new PatientDoseVM();
                   List<PatientDoseVM> _List = _Obj.SelectAllFinalApproved();
                   return View(_List);
              */
            DrugOrderVM Obj = new DrugOrderVM();

            ViewBag.OrderList = Obj.OrderSelectList();
            PatientDoseVM _Obj = new PatientDoseVM();
                       List<PatientDoseVM> _List = _Obj.SelectAllFinalApproved();
                       return View(_List);
             

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
        public JsonResult _EditDrugCondition(int Id)
        {
            ViewBag.Id = Id;
          

            if (Id == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            ViewBag.Id = Id;




            DrugOrderVM Obj = new DrugOrderVM();

            ViewBag.OrderList = Obj.OrderSelectList();


            PatientDoseVM _Obj = new PatientDoseVM();
            PatientDoseVM _ObjvM = _Obj.SelectObject(Id);
            //if (_objVM == null)
            //{
            //    return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);
            //}

            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("_EditDrugCondition", _ObjvM) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddCondition(int Id, int condition)

        {
         
            
                PatientDoseVM _Obj = new PatientDoseVM();
                PatientDoseVM _ObjvM = _Obj.SelectObject(Id);
           
            _ObjvM.Pharmacy_Condition = condition;
            _ObjvM.Edit();
            DrugOrderVM Obj = new DrugOrderVM();
            DrugOrderVM orderVM = Obj.SelectObject(condition);
            _ObjvM.Pharmacy_Condition_Value = orderVM.Name;
          
            _ObjvM.Edit();
            

            return Json(new { IsRedirect = true }, JsonRequestBehavior.AllowGet);

        }

        public void fillBag(int filterID)
        {
            ViewBag.filterID = 1;


        }

    








    }

}