using Hospital.DAL;
using Microsoft.AspNet.Identity;
using RashidHospital.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RashidHospital.Controllers
{
    [Authorize(Roles = "Doctor, Residents,Admin,Assistant lecturer,Consultant,Nurses,physician,Employee,Assistant,Pharmacist")]
    public class MedicalRecordController : Controller
    {
        // GET: MedicalRecord
        [HttpGet]
        public ActionResult Index(string patientID)
        {
            int _patientID = Convert.ToInt32(patientID);
            PatientVM _patientvm = new PatientVM();
            ViewBag.PatientQuery = patientID;
            MedicalRecordVM ObjVm = new MedicalRecordVM();
            List<MedicalRecordVM> _list = ObjVm.SelectAllByPatientId(_patientID).OrderByDescending(a=>a.RecordDate).ToList();
            FillViewBags(_patientID);
            
            return View(_list);
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

        public JsonResult _Edit(int Id)
        {
            if (Id == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);
            }
            MedicalRecordVM _EditObject = new MedicalRecordVM();
            MedicalRecordVM retObject = _EditObject.SelectObject(Convert.ToInt32(Id));
            int _patientID = Convert.ToInt32(retObject.PatientID);
            PatientVM _patientvm = new PatientVM();
            FillViewBags(_patientID);
            
            
            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("_Edit", retObject) }, JsonRequestBehavior.AllowGet);
        }

        //AddCondition
        [HttpPost]
        public string Edit(MedicalRecordVM vm)
        {
            try
            {
                Guid userId = Guid.Parse(User.Identity.GetUserId());
                vm.DoctorID = userId;
                vm.RecordDate = DateTime.Now;
                vm.Complain = vm.Complain.Replace(".", "." + System.Environment.NewLine + "\n");
                vm.Diagnose = vm.Diagnose.Replace(".", "." + System.Environment.NewLine + "\n");
                vm.Recommendation = vm.Recommendation.Replace(".", "." + System.Environment.NewLine + "\n");
                vm.Edit();
                return "Success"; // succcess
            }
            catch (Exception ex)
            {

                return "Error500";

            }
        }
        [HttpGet]
        public ActionResult Create(int PatientId)
        {
            FillViewBags(PatientId);
            
            return View();
        }

        // POST: Clinics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(MedicalRecordVM obj)
        {
            if (obj.ClinicId != 0) {
                // string urlName = Request.UrlReferrer.ToString();
                int patientId = obj.PatientID;
                obj.RecordDate = DateTime.Now;

                Guid userId = Guid.Parse(User.Identity.GetUserId());
                obj.DoctorID = userId;
                if (ModelState.IsValid)
                {
                    PatientVM patient = new PatientVM();
                    patient = patient.SelectObject(obj.PatientID);
                    patient.LastVisitDate = DateTime.Now.ToShortDateString();
                    patient.Edit();


                    obj.Complain = obj.Complain.Replace(".", ".\n");
                    obj.Diagnose = obj.Diagnose.Replace(".", ".\n");
                    obj.Recommendation = obj.Recommendation.Replace(".", ".\n");



                    obj.Create();
                    return RedirectToAction("Index", new RouteValueDictionary(
                                                     new { action = "Index", patientID = patientId }));
                }
                else
                {
                    FillViewBags(obj.PatientID);
                    return View("Index",new { patientID = obj.PatientID });

                }

            }
            else
            {
                FillViewBags(obj.PatientID);
                return View("Index", new { patientID = obj.PatientID });

            }

        }

        public void FillViewBags(int _patientID) {
            PatientVM _patientvm = new PatientVM();
            ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(_patientID);
            ViewBag.PatientId = _patientID;
            ClinicVM _clinicVm = new ClinicVM();
            ViewBag.ClinicDDL = _clinicVm.ClinicSelectList();
            var userID = User.Identity.GetUserId();
            ViewBag.DoctorId = userID;

        }

        [HttpPost]
        public int Delete(int Id)
        {
            int finalResult = 0;
            try
            {
                MedicalRecordVM _VMObject = new MedicalRecordVM();
                MedicalRecordVM _obj = _VMObject.SelectObject(Id);
                _obj.IsDeleted = true;
                Guid userId = Guid.Parse(User.Identity.GetUserId());

                _obj.ModifiedBy = userId;
                _obj.Edit();

                finalResult = 1;


            }
            catch (Exception e)
            {
                finalResult = 6;
            }
            return finalResult;
        }

        [HttpPost]
        public int Attened(int Id) {

            int finalResult = 0;
            try
            {
                MedicalRecordVM _VMObject = new MedicalRecordVM();
                MedicalRecordVM _obj = _VMObject.SelectObject(Id);
                int PatientId = _obj.PatientID;
                DateTime compareDate = _obj.RecordDate;
                PatientVM patient = new PatientVM();
                patient = patient.SelectObject(_obj.PatientID);
                patient.LastVisitDate = DateTime.Now.ToShortDateString();
                patient.Edit();
                try
                {
                    CallBoardVM _callBoard = new CallBoardVM();
                    _callBoard = _callBoard.getByDateandPatient(compareDate, PatientId);
                    _callBoard.Done = true;
                    Guid userId = Guid.Parse(User.Identity.GetUserId());
                    _callBoard.DoctorId = userId;
                    _callBoard.Edit();
                    
                }
                catch (Exception ex)
                {

                }
               
                finalResult = 1;


            }
            catch (Exception e)
            {
                finalResult = 6;
            }
            return finalResult;
        }
        [HttpGet]
        public ActionResult View(string id)
        {
            if (id == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);
            }
            MedicalRecordVM _EditObject = new MedicalRecordVM();
            MedicalRecordVM retObject = _EditObject.SelectObject(Convert.ToInt32(id));
            int _patientID = Convert.ToInt32(retObject.PatientID);
            PatientVM _patientvm = new PatientVM();
            FillViewBags(_patientID);


            return View(retObject);

        }


        public JsonResult Treatment(string pid)
        {
            if (pid == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);

            }
            int _patientID = Convert.ToInt32(pid);
            PatientVM _Obj = new PatientVM();
            PatientVM _objVM = _Obj.SelectObject(_patientID);
            if (_objVM == null)
            {
                return Json(new { IsRedirect = true, RedirectUrl = Url.Action("Error500", "Home") }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsRedirect = false, Content = RenderRazorViewToString("Treatment", _objVM) }, JsonRequestBehavior.AllowGet);
        }



    }
}