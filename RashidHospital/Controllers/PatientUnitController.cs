using RashidHospital.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RashidHospital.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class PatientUnitController : Controller
    {
        // GET: PatientUnit

        // GET: Clinics
        public ActionResult Index()
        {
            PatientUnitVM _obj = new PatientUnitVM();
            List<PatientUnitVM> list = _obj.SelectAll();
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
        public ActionResult Create(PatientUnitVM clinic)
        {
            if (ModelState.IsValid)
            {
                clinic.Create();
                return RedirectToAction("Index");
            }
            else
            {
                return View(clinic);

            }

        }

        // GET: Clinics/Edit/5
        public JsonResult _Edit(int UnitID)
        {
            if (UnitID == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            PatientUnitVM _Obj = new PatientUnitVM();
            PatientUnitVM _objVM = _Obj.SelectObject(UnitID);
            if (_objVM == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("_Edit", _objVM) }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public string EditPatientUnit(PatientUnitVM vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    vm.Edit();
                    return "Success"; // succcess
                }
                else {
                    return "Error500";

                }
            }
            catch (Exception e)
            {
                return "Error500";
            }
        }

        [HttpPost]
        public int DeletePatientUnit(int UnitId)
        {
            int finalResult = 0;
            try
            {
                PatientUnitVM _Unit = new PatientUnitVM();
                PatientUnitVM _obj = _Unit.SelectObject(UnitId);
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