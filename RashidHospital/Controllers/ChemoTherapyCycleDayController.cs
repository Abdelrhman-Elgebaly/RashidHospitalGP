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
       


        public ActionResult Test(int Id)
        {
            ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
            List<ChemoTherapyCycleDayVM> DatesList = _Obj.SelectAllByMainCycleId(Id).OrderBy(a => a.Date).ToList();
            return View(DatesList);
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

                 DeleteObject.Edit();

                finalResult = 1;


            }
            catch (Exception e)
            {
                finalResult = 6;
            }
            return finalResult;
        }


  
        public JsonResult _Rescuedele(int Id)
        {
            if (Id == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
            ChemoTherapyCycleDayVM _Objm = _Obj.SelectObject(Id);
            //if (_objVM == null)
            //{
            //    return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);
            //}

            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("_Rescuedele", _Objm) }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult RescuedeleDates(int Id, DateTime Date)
        {
            ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
            ChemoTherapyCycleDayVM _Objm = _Obj.SelectObject(Id);



              var prevDate = _Objm.Date;
             
            var newDate = Date;

            var diffOfDates = newDate.Subtract(prevDate);

            int x = diffOfDates.Days;
            ChemoTherapyCyclesDatesVM chemoTherapyCyclesDatesVM = new ChemoTherapyCyclesDatesVM();
            List<ChemoTherapyCyclesDatesVM> chemoTherapyCyclesDatesVMs = chemoTherapyCyclesDatesVM.SelectAllByTemplateId(_Objm.TemplateId);


            foreach(var item in chemoTherapyCyclesDatesVMs)
            {

               if (item.Date >= prevDate)
                {
                    item.Date =  item.Date.AddDays(x);
                  
                    item.Edit();
                   
                }
                    
                

            }

            ChemoTherapyCycleDayVM chemoTherapyCycleDayVM = new ChemoTherapyCycleDayVM();
            List<ChemoTherapyCycleDayVM> chemoTherapyCycleDayVMs = chemoTherapyCycleDayVM.SelectAllByTemplateId(_Objm.TemplateId);

            foreach (var item in chemoTherapyCycleDayVMs)
            {




                AppointmentVM appointmntVm = new AppointmentVM();
                List<AppointmentVM> AppointmentList = appointmntVm.SelectAllByPatientId(_Objm.Patient_ID);
                foreach (var itemm in AppointmentList)
                {

                    if (itemm.AppointmentDate.Date == item.Date.Date )
                    {
                        itemm.AppointmentDate=  itemm.AppointmentDate.AddDays(diffOfDates.Days);

                        itemm.Edit();
                    }

                }


               if (item.Date >= prevDate)
                {
                    item.Date = item.Date.AddDays(x);

                    item.Edit();
                  
                }
                   
                 
                





            }

       


            return Json(new { IsRedirect = true }, JsonRequestBehavior.AllowGet);
        }
    }
}