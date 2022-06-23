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
      
        public ActionResult Index(string patientID, string templateID)
        {
          
            int PatientId = Convert.ToInt32(patientID);
            fillBag(PatientId);
            int TemplateID = Convert.ToInt32(templateID);
            //
            PatientVM _pObj = new PatientVM();
            PatientVM _pObjVM = _pObj.SelectObject(PatientId);
            _pObjVM.ChemoTherapyId = TemplateID;
            _pObjVM.Edit();
            //
            ChemoTherapyTemplateVM _Obj = new ChemoTherapyTemplateVM();
            ChemoTherapyTemplateVM _objVM = _Obj.SelectObject(TemplateID);
            //
            ChemoTherapyCyclesDatesVM c= new  ChemoTherapyCyclesDatesVM();
            ChemoTherapyCyclesDatesVM  _cobjVM = c.SelectObject(PatientId);
            //
            if (_cobjVM.ID != null)
            {
                if (_cobjVM.cycleDates == null)
                {
                    // DateTime date = _CobjVM.Date;
                    DateTime date = _cobjVM.Date;
                    List<DateTime> dates = new List<DateTime>();
                    dates.Add(date);
                    for (int i = 1; i < _objVM.Maximum_cycles; i++)
                    {
                        double x = Convert.ToDouble(_objVM.Frequency);
                        DateTime newDate = date.AddDays(x);
                        dates.Add(newDate);
                        date = newDate;

                    }

                    _cobjVM.cycleDates = dates;
                    _cobjVM.Edit();
                    //
                    var tuple = new Tuple<ChemoTherapyTemplateVM, ChemoTherapyCyclesDatesVM>(_objVM, _cobjVM);
                    return View(tuple);

                }
                else
                {
                    var tuple = new Tuple<ChemoTherapyTemplateVM, ChemoTherapyCyclesDatesVM>(_objVM, _cobjVM);
                    return View(tuple);
                }

            } else
            {
                return View(_objVM);
            }

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
            if (Cycles_Number != 0 || PatientId != 0)
            {
                ChemoTherapyCyclesDatesVM _labresults = new ChemoTherapyCyclesDatesVM();
                _labresults.Patient_ID = PatientId;

                _labresults.Date = Date;
                _labresults.Cycles_Number = Cycles_Number;
                _labresults.ID = PatientId;
                _labresults.Create();
            }

            return Json(new { IsRedirect = true }, JsonRequestBehavior.AllowGet);
        }









    }
}