using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hospital.DAL;
using RashidHospital.Models;
using System.IO;

namespace RashidHospital.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class ClinicsController : Controller
    {
       

        // GET: Clinics
        public ActionResult Index()
        {
            ClinicVM _obj = new ClinicVM();
           List<ClinicVM> list= _obj.SelectAll();
            return View(list);
        }

       
        // GET: Clinics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clinics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClinicVM clinic)
        {
            if (ModelState.IsValid)
            {
                
                clinic.Create();
                return RedirectToAction("Index");
            }
            else {
                return View(clinic);

            }

        }

        // GET: Clinics/Edit/5
        public JsonResult _Edit(int ClinicID)
        {
            if (ClinicID == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            ClinicVM _Obj = new ClinicVM();
            ClinicVM _objVM = _Obj.SelectObject(ClinicID);
            if (_objVM == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("_Edit", _objVM) }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public string EditClinic(ClinicVM vm)
        {
            try
            {
                vm.Edit();
                return "Success"; // succcess

            }
            catch (Exception e)
            {
                return "Error500";
            }
        }
        
        [HttpPost]
        public int DeleteClinic(int ClinicId)
        {
            int finalResult = 0;
            try
            {
                ClinicVM _Clinic = new ClinicVM();
               ClinicVM _obj = _Clinic.SelectObject(ClinicId);
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
    }
}
