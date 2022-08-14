
using Microsoft.AspNet.Identity;
using RashidHospital.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static RashidHospital.Helper.Enum;
using System.Text.RegularExpressions;


namespace RashidHospital.Controllers
{
    public class LabPackageController : Controller
    {




      

        private void fillBag(int PackageId)
        {
            ViewBag.PackageId = PackageId;


        }

        public ActionResult Index(string PackageId)
        {
            

            int _PackageId = Convert.ToInt32(PackageId);
            fillBag(_PackageId);

           PackageVM objVm = new PackageVM();

            PackageVM Package = objVm.SelectObject(_PackageId);
            ViewBag.PackageName = Package.Name;



            LabPackageVM ObjVm = new LabPackageVM();
           
            List<LabPackageVM> _list = ObjVm.SelectAllByPackageID(_PackageId);
          

            return View(_list);
        }

        public JsonResult Create(int PackageId)
        {
            if (PackageId == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);


            }
            fillCreateBag(PackageId);
            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("Create", null) }, JsonRequestBehavior.AllowGet);

            //  return View();

        }

        public JsonResult Edit(int PackageId , int Id)
        {
            if (Id == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);


            }
            LabPackageVM results = new LabPackageVM();
            LabPackageVM obj = results.SelectObject(Id);
            fillCreateBag(PackageId);
            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("Edit", obj) }, JsonRequestBehavior.AllowGet);

            //  return View();

        }
        private void fillCreateBag(int PackageId)
        {
            LabPackageVM results = new LabPackageVM();
            ViewBag.LabTypesList = results.GeLabTypsSelectList();
            ViewBag.RuleTypesList = results.GetRuleTypsSelectList();
            fillBag(PackageId);

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

        public ActionResult Create(LabPackageVM vm)
        {
            try
            {
                vm.Package_ID = vm.Package_ID;
             
                vm.Create();

                //PatientId
                return RedirectToAction("Index", new { PackageId = vm.Package_ID });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error500");
            }
        }
        [HttpPost]
        public JsonResult AddResult(int Package_ID, int Test,  double Value, int Rule)
        {
            if (Package_ID != 0  || Test != 0)
            {
                LabPackageVM _labresults = new LabPackageVM();
                _labresults.Package_ID = Package_ID;
                _labresults.Test = Test;
                _labresults.Value = Value;
                _labresults.Rule = Rule;
               
              
                _labresults.Create();
            }

            return Json(new { IsRedirect = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditResult(int Package_ID, int Test, double Value, int Rule, int Id)
        {
            if (Package_ID != 0 || Test != 0)
            {

                LabPackageVM results = new LabPackageVM();
                LabPackageVM obj = results.SelectObject(Id);
                obj.Package_ID = Package_ID;
                obj.Test = Test;
                obj.Value = Value;
                obj.Rule = Rule;


                obj.Edit();
            }

            return Json(new { IsRedirect = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int Delete(int ID)
        {
            int finalResult = 0;
            try
            {
                LabPackageVM _resultVM = new LabPackageVM();
                LabPackageVM DeleteObject = _resultVM.SelectObject(ID);
              
                DeleteObject.Delete();

                finalResult = 1;


            }
            catch (Exception e)
            {
                finalResult = 6;
            }
            return finalResult;
        }








    }
}