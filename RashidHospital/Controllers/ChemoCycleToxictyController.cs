﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RashidHospital.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
    public class ChemoCycleToxictyController : Controller
    {
        // GET: ChemoCycleToxicty
    


        public ActionResult Index(int Id, string pid)
        {
            int _patientID = Convert.ToInt32(pid);
            fillBag(Id, _patientID);

            ToxictyVM ObjVm = new ToxictyVM();
            List<ToxictyVM> _list = ObjVm.SelectAllByPatientId(_patientID);
            return View(_list);
        }


     
        public ActionResult Create(int Id, string pid)
        {
           
            int _patientID = Convert.ToInt32(pid);
         
          
            ToxictyVM toxictyVM = new ToxictyVM();
            toxictyVM.PatientID = _patientID;
            fillBag(Id, _patientID);
            ddlViewBags();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ToxictyVM input)
        {
            if (ModelState.IsValid)
            {
                input.CreatedDate = DateTime.Now;
                input.ModifiedDate = DateTime.Now;
                Guid userId = Guid.Parse(User.Identity.GetUserId());
                input.DoctorId = userId;
                input.Create();
                return RedirectToAction("Index", new { Id = ViewBag.CycleId, pid = input.PatientID });
            }
            else
            {
                return View(input);

            }

        }
        private void ddlViewBags()
        {
            ToxictyTypeVM toxictyTypeVM = new ToxictyTypeVM();
            ViewBag.ToxictyTypeList = toxictyTypeVM.GetSelectList();
            ToxictyVM toxictyVM = new ToxictyVM();
            ViewBag.ToxictyGrade = toxictyVM.GetToxictyGradeSelectList();
            ViewBag.ToxictyCondition = toxictyVM.GetToxictyConditionSelectList();

        }
        private void fillBag(int CycleId, int PatientId)
        {
           
            ViewBag.PatientId = PatientId;
            ViewBag.CycleId = CycleId;
            ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(PatientId);
            var userID = User.Identity.GetUserId();
            ViewBag.DoctorId = userID;

        }


        [HttpPost]
        public int Delete(int toxictyID)
        {
            int finalResult = 0;
            try
            {
                ToxictyVM _VMObject = new ToxictyVM();
                ToxictyVM _obj = _VMObject.SelectObject(toxictyID);
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
            RadiologyResultVM _obj = new RadiologyResultVM();
            _obj = _obj.SelectObject(Id);
            ddlViewBags();


            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("_Edit", _obj) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public string Edit(ToxictyVM vm)
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

        [HttpPost]
        public JsonResult getDescription(int ToxictyTypeId, int GradeId)
        {
            try
            {
                ToxictyTypeVM toxictyTypeVM = new ToxictyTypeVM();
                var ToxictyType = toxictyTypeVM.GetById(ToxictyTypeId);
                if (GradeId == (int)ToxictyGrade.grade1)
                {
                    return Json(new { data = ToxictyType.Grade1 }, JsonRequestBehavior.AllowGet);
                }
                else if (GradeId == (int)ToxictyGrade.grade2)
                {
                    return Json(new { data = ToxictyType.Grade2 }, JsonRequestBehavior.AllowGet);
                }
                else if (GradeId == (int)ToxictyGrade.grade3)
                {
                    return Json(new { data = ToxictyType.Grade3 }, JsonRequestBehavior.AllowGet);
                }
                else if (GradeId == (int)ToxictyGrade.grade4)
                {
                    return Json(new { data = ToxictyType.Grade4 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { data = ToxictyType.Grade5 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult _EditNote(int Id)
        {
            ViewBag.Id = Id;
            if (Id == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            ViewBag.Id = Id;

            ToxictyVM _Obj = new ToxictyVM();
            ToxictyVM _ObjvM = _Obj.SelectObject(Id);
            //if (_objVM == null)
            //{
            //    return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);
            //}

            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("_EditNote", _ObjvM) }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult AddNote(int Id, string Note)

        {

            ToxictyVM _Obj = new ToxictyVM();
            ToxictyVM _ObjvM = _Obj.SelectObject(Id);
            _ObjvM.Pharmacist_Note = Note;
           
            _ObjvM.Edit();
            if (User.IsInAnyRoles("Pharmacist"))
            {
                _ObjvM.IsEditByPharmacy = true;
                _ObjvM.Edit();
            }
            return Json(new { IsRedirect = true }, JsonRequestBehavior.AllowGet);

        }

    }
}