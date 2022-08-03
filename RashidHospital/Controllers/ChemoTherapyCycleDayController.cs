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
    public class ChemoTherapyCycleDayController : Controller
    {

        // GET: ChemoTherapyCycleDay
        public ActionResult Index(int MainCycleId)
        {

       

            ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
            List<ChemoTherapyCycleDayVM> DatesList = _Obj.SelectAllByMainCycleId(MainCycleId).OrderBy(a => a.Date).ToList();
         

        
            return View(DatesList);

            // return View(OrderList);
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


        public JsonResult _TestPopUp(int Id)
        {
            if (Id == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
            List<ChemoTherapyCycleDayVM> DatesList = _Obj.SelectAllByMainCycleId(Id).OrderBy(a => a.Date).ToList();
            //if (_objVM == null)
            //{
            //    return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);
            //}

            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("_TestPopUp", DatesList) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public int Omit(int Id)
        {
            int finalResult = 0;
            try
            {
                ChemoTherapyCycleDayVM _resultVM = new ChemoTherapyCycleDayVM();
                ChemoTherapyCycleDayVM DeleteObject = _resultVM.SelectObject(Id);
                 DeleteObject.IsDeleted = true;
               // DeleteObject.Delete();

                finalResult = 1;


            }
            catch (Exception e)
            {
                finalResult = 6;
            }
            return finalResult;
        }



        public ActionResult Test(int Id)
        {
            ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
            List<ChemoTherapyCycleDayVM> DatesList = _Obj.SelectAllByMainCycleId(Id).OrderBy(a => a.Date).ToList();
            return View(DatesList);
        }
        }
}