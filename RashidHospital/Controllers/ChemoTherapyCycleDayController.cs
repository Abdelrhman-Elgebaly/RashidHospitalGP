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


            ViewBag.MainCycleId = Id;
            if (Id == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            ViewBag.MainCycleId = Id;
            ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
            List<ChemoTherapyCycleDayVM> DatesList = _Obj.SelectAllByMainCycleId(Id).ToList();
            //if (_objVM == null)
            //{
            //    return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);
            //}

            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("_TestPopUp", DatesList) }, JsonRequestBehavior.AllowGet);
        }
       


        public ActionResult Test(int Id)
        {
            ChemoTherapyCyclesDatesVM chemoTherapyCyclesDates = new ChemoTherapyCyclesDatesVM();

            ChemoTherapyCyclesDatesVM Obj = chemoTherapyCyclesDates.SelectObject(Id);
            ViewBag.Date = Obj.Date.Date;
            ViewBag.pid = Obj.Patient_ID;
            ViewBag.tid = Obj.TemplateId;
            ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
            List<ChemoTherapyCycleDayVM> DatesList = _Obj.SelectAllByMainCycleId(Id).ToList();
            return View(DatesList);
        }


        [HttpPost]
        public int Omit(int Id)
        {
            int finalResult = 0;
            try
            {
                ChemoTherapyCycleDayVM cycle = new ChemoTherapyCycleDayVM();
                ChemoTherapyCycleDayVM DeleteObject = cycle.SelectObject(Id);
                DeleteObject.IsDeleted = true;

                 DeleteObject.Edit();

// Omit in appoitment
                AppointmentVM appointmntVm = new AppointmentVM();
                List<AppointmentVM> AppointmentList = appointmntVm.SelectAllByPatientId(DeleteObject.Patient_ID);
                foreach (var item in AppointmentList)
                {

                    if (item.AppointmentDate.Date == DeleteObject.Date.Date)
                    {


                        Guid userId = Guid.Parse(User.Identity.GetUserId());
                        item.ModifiedBy = userId;
                        item.IsDeleted = true;
                        item.Edit();

                    }
                }













             

















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
        public JsonResult RescuedeleDates(int Id, DateTime Date , string Reason)
        {
            ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
            ChemoTherapyCycleDayVM _Objm = _Obj.SelectObject(Id);
            _Objm.Reason = Reason;
            _Objm.IsRescuedeled = true;
            _Objm.Edit();


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
                        if (itemm.AppointmentDate >= prevDate)
                        {

                            itemm.AppointmentDate = itemm.AppointmentDate.AddDays(diffOfDates.Days);

                            itemm.Edit();
                        }
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