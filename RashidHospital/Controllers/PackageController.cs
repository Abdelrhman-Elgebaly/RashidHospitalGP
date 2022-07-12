
using Hospital.DAL;
using RashidHospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Text.RegularExpressions;

using System.IO;

namespace RashidHospital.Controllers
{
    public class PackageController : Controller
    {
        private readonly Model1 _context;


        public PackageController(Model1 context)
        {
            _context = context;

        }

        public JsonResult GetProrocolList(int DiseaseId)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            List<protocol> ProtocolList = _context.protocols.Where(x => x.DiseaseId == DiseaseId).ToList();
            return Json(ProtocolList, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Index()
        {

            PackageVM ObjVm = new PackageVM();

            List<PackageVM> _list = ObjVm.SelectAll();

            return View(_list);
        }

  

        public JsonResult Create(int Id)
        {
            if (Id == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);


            }
           
            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("Create", null) }, JsonRequestBehavior.AllowGet);

            //  return View();

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

        public ActionResult Create(PackageVM vm)
        {
            try
            {
          
                vm.Create();

                //PatientId
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error500");
            }
        }
        [HttpPost]
        public JsonResult AddResult( string Name)
        {
            if (Name != null)
            {
                PackageVM _labresults = new PackageVM();
                _labresults.Name = Name;
        
                _labresults.Create();
            }

            return Json(new { IsRedirect = true }, JsonRequestBehavior.AllowGet);
        }














    }
}