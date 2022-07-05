
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
    public class ChemoTherapyDrugController : Controller
    {




        // GET: RadioTherapy
        /*
        public ActionResult Index(string templateID)
        {
            int _templateID = Convert.ToInt32(templateID);
            fillBag(_templateID);
            ChemoTherapyDrugVM ObjVm = new ChemoTherapyDrugVM();
            List<ChemoTherapyDrugVM> _list = ObjVm.SelectAllByTemplateId(_templateID);
            return View(_list);
        }



        private void fillBag(int templateID)
        {
            ViewBag.PatientId = templateID;
            ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(templateID);
            var userID = User.Identity.GetUserId();
            ViewBag.DoctorId = userID;

        }
        public ActionResult Create(int templateID, string therapyType )
        {
          // int _templateID = Convert.ToInt32(templateID);
            ChemoTherapyDrugVM radioTherapyVMVM = new ChemoTherapyDrugVM();
            radioTherapyVMVM.Template_ID = templateID;
            radioTherapyVMVM.Therapy_Type = therapyType;
         ////   fillBag(_templateID);
         //   ddlViewBags();
            return View(radioTherapyVMVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChemoTherapyDrugVM input)
        {
            if (ModelState.IsValid)
            {
               
                input.Create();
                return RedirectToAction("Index", new { templateID = input.Template_ID });
            }
            else
            {
                return View(input);

            }

        }
*/

        private void fillBag(int templateID)
        {
            ViewBag.PatientId = templateID;
           

        }

        public ActionResult Index(string templateID)
        {
            int _templateID = Convert.ToInt32(templateID);
            fillBag(_templateID);
            ChemoTherapyDrugVM ObjVm = new ChemoTherapyDrugVM();
            List<ChemoTherapyDrugVM> _list = ObjVm.SelectAllByTemplateId(_templateID);
            return View(_list);
        }

        private void fillCreateBag()
        {
            ChemoTherapyDrugVM results = new ChemoTherapyDrugVM();
            ViewBag.Unit = results.GetUnitSelectList();
            ViewBag.Route = results.GetRouteSelectList();
            ViewBag.Fluid = results.GetFluidTSelectList();



        }

        public ActionResult Create(int templateID, string therapyType)
        {
            //  int _patientID = Convert.ToInt32(patientID);
            ChemoTherapyDrugVM radioTherapyVMVM = new ChemoTherapyDrugVM();
            fillCreateBag();
            radioTherapyVMVM.Template_ID = templateID;
            radioTherapyVMVM.Therapy_Type = therapyType;
            fillCreateBag();
            return View(radioTherapyVMVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChemoTherapyDrugVM input)
        {
            fillCreateBag();
            if (ModelState.IsValid)
            {
                input.Create();
                return RedirectToAction("Index", new { templateID = input.Template_ID });
            }
            fillCreateBag();
            return View(input);
        }



        public JsonResult Edit(int patientID)
        {
            if (patientID == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }


            ChemoTherapyDrugVM _Obj = new ChemoTherapyDrugVM();
            ChemoTherapyDrugVM _objVM = _Obj.SelectObject(patientID);
            if (_objVM == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("Edit", _objVM) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public string _Edit(ChemoTherapyDrugVM vm)
        {
            try
            {
              
                vm.Edit();
                return "Success"; // succcess
            }
            catch (Exception ex)
            {

                return "Error500";

            }
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