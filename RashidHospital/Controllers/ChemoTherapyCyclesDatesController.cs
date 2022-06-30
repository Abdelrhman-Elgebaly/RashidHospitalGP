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
    public class ChemoTherapyCyclesDatesController : Controller
    {
       
        // GET: CycleStartDate
      
      
        public ActionResult Index(int PatientId, string templateID)
        {

            //Assign Template to patient 
            int TemplateID = Convert.ToInt32(templateID);
            ChemoTherapyTemplateVM _Obj = new ChemoTherapyTemplateVM();
            ChemoTherapyTemplateVM _objVM = _Obj.SelectObject(TemplateID);
            PatientVM _pObj = new PatientVM();
            PatientVM _pObjVM = _pObj.SelectObject(PatientId);
            _pObjVM.ChemoTherapyId = TemplateID;
            _pObjVM.Edit();

            //Start Dates 

            ChemoTherapyCyclesDatesVM _Labresults = new ChemoTherapyCyclesDatesVM();
            List<ChemoTherapyCyclesDatesVM> OrderList = _Labresults.SelectAllByPatientId(PatientId).OrderBy(a => a.Date).ToList();
            fillBag(PatientId);
          
            var tuple = new Tuple<ChemoTherapyTemplateVM, List<ChemoTherapyCyclesDatesVM>>(_objVM, OrderList);
            return View(tuple);

           // return View(OrderList);
        }


        public JsonResult Create(int PatientId)
        {
            if (PatientId == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);


            }
            fillCreateBag(PatientId);
            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("Create", null) }, JsonRequestBehavior.AllowGet);

            //  return View();

        }


        private void fillBag(int patientID)
        {
            PatientVM _patientvm = new PatientVM();
            ViewBag.PatientId = patientID;
            ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(patientID);
            var userID = User.Identity.GetUserId();
            ViewBag.DoctorId = userID;
        }

     
        private void fillCreateBag(int PatientId)
        {
            LabResualtVM results = new LabResualtVM();
            ViewBag.LabTypesList = results.GeLabTypsSelectList();
            fillBag(PatientId);

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

        public ActionResult Create(ChemoTherapyCyclesDatesVM vm)
        {
            try
            {

                vm.Patient_ID = vm.Patient_ID;
                //   vm.ResualtDate = DateTime.Now;

                vm.Create();

                for (int i = 1; i < vm.Cycles_Number; i++)
                {
                    vm.Patient_ID = vm.Patient_ID;
                    //   vm.ResualtDate = DateTime.Now;

                    double x = Convert.ToDouble(vm.Cycles_Number);
                    DateTime newDate = vm.Date.AddDays(x);
                   
                    vm.Date = newDate;

                    vm.Create();

                }

                //PatientId
                //return RedirectToAction("Index", new { PatientId = vm.Patient_ID });
                return RedirectToAction("Error500");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error500");
            }
        }
        [HttpPost]
        public JsonResult AddResult(int Cycles_Number, int PatientId, DateTime Date)
        {
            PatientVM _pObj = new PatientVM();
            PatientVM _pObjVM = _pObj.SelectObject(PatientId);
            int TemplateID = Convert.ToInt32(_pObjVM.ChemoTherapyId);

            ChemoTherapyTemplateVM _Obj = new ChemoTherapyTemplateVM();
            ChemoTherapyTemplateVM _objVM = _Obj.SelectObject(TemplateID);
            for (int i = 0; i < Cycles_Number; i++)
            {

                if (Cycles_Number != 0 || PatientId != 0)
                {
                    ChemoTherapyCyclesDatesVM _labresults = new ChemoTherapyCyclesDatesVM();
                    
                    _labresults.Patient_ID = PatientId;

                 

                    if (i !=0)
                    {
                        double x = Convert.ToDouble(_objVM.Frequency);
                        DateTime newDate = Date.AddDays(x);
                        Date = newDate;
                    }
            

                    _labresults.Date = Date;
                    _labresults.Cycles_Number = Cycles_Number;
                  //  _labresults.ID = PatientId;
                    _labresults.Create();
                }









            }

            //Cycle days/////

            //Cycle days
            List<int> lst = new List<int>();
            lst.Add(1);
            lst.Add(2);

            ChemoTherapyCyclesDatesVM _Labresults = new ChemoTherapyCyclesDatesVM();
            List<ChemoTherapyCyclesDatesVM> OrderList = _Labresults.SelectAllByPatientId(PatientId);

            foreach (var itemm in OrderList)
            {


                //Cycle days

                foreach (var item in lst)
                {
                    ChemoTherapyCycleDayVM _labresults22 = new ChemoTherapyCycleDayVM();
                    _labresults22.Patient_ID = PatientId;
                    _labresults22.MainCycle_ID = itemm.ID;

                    _labresults22.Date = itemm.Date.AddDays(item - 1);
                    _labresults22.Create();

                }



            }

            return Json(new { IsRedirect = true }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult test(int PatientId)
        {
            List<int> lst = new List<int>();
            lst.Add(1);
            lst.Add(2);

            ChemoTherapyCyclesDatesVM _Labresults = new ChemoTherapyCyclesDatesVM();
            List<ChemoTherapyCyclesDatesVM> OrderList = _Labresults.SelectAllByPatientId(PatientId);

            foreach (var itemm in OrderList)
            {


                //Cycle days

                foreach (var item in lst)
                {
                    ChemoTherapyCycleDayVM _labresults22 = new ChemoTherapyCycleDayVM();
                    _labresults22.Patient_ID = PatientId;
                    _labresults22.MainCycle_ID = itemm.ID;

                    _labresults22.Date = itemm.Date.AddDays(item - 1);
                    _labresults22.Create();

                }

               

            }
            return RedirectToAction("Index", new { PatientId = PatientId });
        }






    }
}