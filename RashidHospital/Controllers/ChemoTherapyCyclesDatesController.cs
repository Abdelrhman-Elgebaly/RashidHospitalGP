using Hospital.DAL;
using Microsoft.AspNet.Identity;
using RashidHospital.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;


namespace RashidHospital.Controllers
{
    public class ChemoTherapyCyclesDatesController : Controller
    {

        // GET: CycleStartDate


        public ActionResult Index(int PatientId, string templateID)
        {

            //Assign Template to patient 
            int TemplateID = Convert.ToInt32(templateID);
            ChemoTherapyProtocolVM _cObj = new ChemoTherapyProtocolVM();
            ChemoTherapyProtocolVM _cobjVM = _cObj.SelectObject(TemplateID);

            ChemoTherapyTemplateVM _Obj = new ChemoTherapyTemplateVM();
            ChemoTherapyTemplateVM _objVM = _Obj.SelectObject(_cobjVM.Template_ID);


            //Start Dates 

            ChemoTherapyCyclesDatesVM _Labresults = new ChemoTherapyCyclesDatesVM();
            List<ChemoTherapyCyclesDatesVM> OrderList = _Labresults.SelectAllByTemplateId(TemplateID).OrderBy(a => a.Date).ToList();
            fillBag(PatientId, TemplateID);

            var tuple = new Tuple<ChemoTherapyTemplateVM, List<ChemoTherapyCyclesDatesVM>>(_objVM, OrderList);
            return View(tuple);

            // return View(OrderList);
        }


        public JsonResult Create(int PatientId, int TemplateId)
        {
            if (PatientId == null && TemplateId == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);


            }
            fillCreateBag(PatientId, TemplateId);
            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("Create", null) }, JsonRequestBehavior.AllowGet);

            //  return View();

        }


        private void fillBag(int patientID, int TemplateId)
        {
            PatientVM _patientvm = new PatientVM();
            ViewBag.PatientId = patientID;
            ViewBag.TemplateId = TemplateId;

            ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(patientID);
            var userID = User.Identity.GetUserId();
            ViewBag.DoctorId = userID;
        }


        private void fillCreateBag(int PatientId, int TemplateId)
        {
            LabResualtVM results = new LabResualtVM();
            ViewBag.LabTypesList = results.GeLabTypsSelectList();
            fillBag(PatientId, TemplateId);

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
                vm.TemplateId = vm.TemplateId;
                //   vm.ResualtDate = DateTime.Now;

                vm.Create();

                /*  for (int i = 1; i < vm.Cycles_Number; i++)
                  {
                      vm.Patient_ID = vm.Patient_ID;
                      //   vm.ResualtDate = DateTime.Now;
                      double x = Convert.ToDouble(vm.Cycles_Number);
                      DateTime newDate = vm.Date.AddDays(x);

                      vm.Date = newDate;
                      vm.Create();
                  }
                */
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
        public JsonResult AddResult(int Cycles_Number, int PatientId, DateTime Date, int TemplateId)
        {


            ChemoTherapyProtocolVM _cObj = new ChemoTherapyProtocolVM();
            ChemoTherapyProtocolVM _cobjVM = _cObj.SelectObject(TemplateId);


            int TemplateID = Convert.ToInt32(_cobjVM.Template_ID);

            ChemoTherapyTemplateVM _Obj = new ChemoTherapyTemplateVM();
            ChemoTherapyTemplateVM _objVM = _Obj.SelectObject(TemplateID);










            for (int i = 0; i < Cycles_Number; i++)
            {

                if (Cycles_Number != 0 || PatientId != 0)
                {
                    ChemoTherapyCyclesDatesVM _labresults = new ChemoTherapyCyclesDatesVM();

                    _labresults.Patient_ID = PatientId;
                    _labresults.TemplateId = TemplateId;



                    if (i != 0)
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



            ChemoTherapyCyclesDatesVM _Labresults = new ChemoTherapyCyclesDatesVM();
            List<ChemoTherapyCyclesDatesVM> OrderList = _Labresults.SelectAllByTemplateId(TemplateId);



            List<string> tokens = _objVM.Cycle_days.Split('/').ToList();
            List<int> intlist = new List<int>();

            foreach (String str in tokens)
            {
                intlist.Add(Convert.ToInt32(Regex.Replace(str, "[^0-9]+", string.Empty)));
            }




            foreach (var itemm in OrderList)
            {


                //Cycle days

                foreach (var item in intlist)
                {
                    ChemoTherapyCycleDayVM _labresults22 = new ChemoTherapyCycleDayVM();
                    AppointmentVM appointment = new AppointmentVM();
                    _labresults22.Patient_ID = PatientId;
                    _labresults22.TemplateId = TemplateId;
                    appointment.PatientId = PatientId;
                    _labresults22.MainCycle_ID = itemm.ID;
                    appointment.ReturnUrl = System.Web.HttpContext.Current.Request.UrlReferrer?.ToString();
                    _labresults22.Date = itemm.Date.AddDays(item - 1);
                    appointment.AppointmentDate = itemm.Date.AddDays(item - 1);
                    appointment.ClinicId = 1004;
                    appointment.Create();
                    _labresults22.Create();



                }



            }

            // Lab Package

            foreach (var itemm in OrderList)
            {



                ChemoTherapyCycleDayVM _Labresultsv = new ChemoTherapyCycleDayVM();
                List<ChemoTherapyCycleDayVM> OrderListv = _Labresultsv.SelectAllByMainCycleId(itemm.ID);

                foreach (var item in OrderListv)
                {
                    ChemoTherapyPreLabVM ObjVm = new ChemoTherapyPreLabVM();
                    List<ChemoTherapyPreLabVM> _list = ObjVm.SelectAllByTemplateID(_objVM.Template_ID);
                    foreach (var itemm3 in _list)
                    {

                        ChemoTherapyCyclePackageVM cc = new ChemoTherapyCyclePackageVM();
                        cc.Cycle_ID = item.ID;
                        // cc.Actual_Value = 0;
                        cc.Test_Type = itemm3.Test_Type;
                        cc.Test_Value = itemm3.Value;
                        cc.Rule_Type = itemm3.Rule_Type;
                        cc.Patient_ID = item.Patient_ID;
                        cc.TemplateId = TemplateId;

                        cc.Create();

                    }
                    ChemoTherapyPreInvestigationsVM ObjVmi = new ChemoTherapyPreInvestigationsVM();
                    List<ChemoTherapyPreInvestigationsVM> _listi = ObjVmi.SelectAllByTemplateID(_objVM.Template_ID);
                    foreach (var itemm4 in _listi)
                    {

                        ChemoTherapyCycleInvestigationVM cc = new ChemoTherapyCycleInvestigationVM();
                        cc.Cycle_ID = item.ID;
                        // cc.Actual_Value = 0;
                        cc.Inves_Type = itemm4.Test_Name;
                        cc.Value = itemm4.Value;
                        cc.Rule_Type = itemm4.Rule_Type;
                        cc.Patient_ID = item.Patient_ID;
                        cc.TemplateId = TemplateId;
                        cc.Create();

                    }






                }

            }
            return Json(new { IsRedirect = true }, JsonRequestBehavior.AllowGet);
        }








    }
}