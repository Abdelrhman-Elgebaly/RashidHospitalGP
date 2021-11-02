using Microsoft.AspNet.Identity;
using RashidHospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RashidHospital.Controllers
{
    [Authorize(Roles = "Doctor, Residents,Admin,Assistant lecturer,Consultant,Nurses,physician,Employee,Assistant,Pharmacist")]
    public class AppointmentController : Controller
    {
        // GET: Appointment
        public ActionResult Index(int? patientd)
        {
            if (patientd != null) {
                AppointmentVM appointmntVm = new AppointmentVM();
                List<AppointmentVM> AppointmentList = appointmntVm.SelectAllByPatientId(patientd.Value).OrderByDescending(a => a.AppointmentDate).ToList();
                var userID = User.Identity.GetUserId();
                ViewBag.DoctorId = userID;
                return View(AppointmentList);
            } else {
                AppointmentVM appointmntVm = new AppointmentVM();
                List<AppointmentVM> AppointmentList = appointmntVm.SelectAllByDate(DateTime.Now).OrderByDescending(a => a.AppointmentDate).ToList();
                var userID = User.Identity.GetUserId();
                ViewBag.DoctorId = userID;
                return View(AppointmentList);
            }
           
        }
        public ActionResult IndexAll()
        {
            AppointmentVM appointmntVm = new AppointmentVM();
            List<AppointmentVM> AppointmentList = appointmntVm.SelectAll().OrderByDescending(a => a.AppointmentDate).ToList();
            return View(AppointmentList);
        }
        // GET: Appointment/Create
        [HttpGet]
        public ActionResult Create(int PatientId)
        {
            fillViewBags(PatientId);
             AppointmentVM appointment = new AppointmentVM();
            appointment.PatientId = PatientId;
            appointment.ReturnUrl = System.Web.HttpContext.Current.Request.UrlReferrer?.ToString();
            appointment.AppointmentDate = DateTime.Now;
            return View(appointment);
        }

        //CreateNew
        [HttpGet]
        public ActionResult CreateNew()
        {
            fillViewBagsCreateNew();
            AppointmentVM appointment = new AppointmentVM();
            appointment.ReturnUrl = System.Web.HttpContext.Current.Request.UrlReferrer?.ToString();
            appointment.AppointmentDate = DateTime.Now;
            return View(appointment);
        }
        private void fillViewBags(int PatientId) {
            ClinicVM clinicVM = new ClinicVM();
            ViewBag.clinicList = clinicVM.ClinicSelectList();
            ViewBag.PatientInfo = ViewBagsHelper.getPatientInfo(PatientId);
        }

        // POST: Appointment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AppointmentVM appointment)
        {
            if (ModelState.IsValid)
            {
                AppointmentVM _vm = new AppointmentVM();
               bool results= _vm.CheckClinicAvailabilty(appointment.ClinicId, appointment.AppointmentDate);
                if (results) {
                    Guid userId = Guid.Parse(User.Identity.GetUserId());
                    appointment.DoctorId = userId;
                    appointment.Create();
                    if (appointment.ReturnUrl != null) {
                        return Redirect(appointment.ReturnUrl);

                    }
                    else {
                        return RedirectToAction("Index");

                    }
                }
                else
                {
                    ViewBag.Error = "That clinic completed the reservation times";
                    fillViewBags(appointment.PatientId);
                    return View(appointment);

                }

            }
            else
            {
                fillViewBags(appointment.PatientId);
                return View(appointment);

            }

        }
        //DeleteAppointment
        [HttpPost]
        public int DeleteAppointment(int appointmentId)
        {
            int finalResult = 0;
            try
            {
                AppointmentVM _appointment = new AppointmentVM();
                AppointmentVM _obj = _appointment.SelectObject(appointmentId);
                Guid userId = Guid.Parse(User.Identity.GetUserId());
                _obj.ModifiedBy = userId;
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
        [HttpPost]
        public int attnedAppointment(int appointmentId)
        {
            int finalResult = 0;
            try
            {
                PatientVM patient = new PatientVM();
               
                AppointmentVM _appointment = new AppointmentVM();
                AppointmentVM _obj = _appointment.SelectObject(appointmentId); 
                patient = patient.SelectObject(_obj.PatientId);
                patient.LastVisitDate = DateTime.Now.ToShortDateString();
                patient.Edit();
                if (_obj.Done == false)
                {
                    _obj.Done = true;
                }
                else {
                    _obj.Done = false;

                }
                _obj.Edit();
                CallBoardVM callBoardVm = new CallBoardVM();
                callBoardVm.AppointmentId = _obj.Id;
                callBoardVm.VisitDate = DateTime.Now;
                int count = callBoardVm.GetPatientNumber(DateTime.Now);
                callBoardVm.PatientNumber=  count + 1;
                callBoardVm.ClinicId = _obj.ClinicId;
                callBoardVm.VisitDate = _obj.AppointmentDate;
                callBoardVm.IsOnCall = false;
                callBoardVm.PatientId = _obj.PatientId;
                callBoardVm.Done = false;
                callBoardVm.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                callBoardVm.Create();
                PatientVM _patient = new PatientVM();
                _patient.SetVisitDate(_obj.PatientId);

                finalResult = _obj.PatientId;
               

            }
            catch (Exception e)
            {
                finalResult = 0;
            }
            return finalResult;
        }
        [HttpPost]
        public JsonResult UndoAppointment(int appointmentId)
        {
            int finalResult = 0;
            try
            {
                AppointmentVM _appointment = new AppointmentVM();
                AppointmentVM _obj = _appointment.SelectObject(appointmentId);
                Guid userId = Guid.Parse(User.Identity.GetUserId());
                _obj.ModifiedBy = userId;
                _obj.Done = false;
                _obj.Edit();
                CallBoardVM _objcall = new CallBoardVM();
                CallBoardVM _undo = _objcall.getByDateandPatient(DateTime.Now,_obj.PatientId);
                _undo.IsDeleted = true;
                _undo.Edit();
                return Json(new { IsRedirect = false, Content = 1 }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception e)
            {
                return Json(new { IsRedirect = false, Content = 6 }, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpPost]
        public int BackToCall(int appointmentId)
        {
            int finalResult = 0;
            try
            {
                AppointmentVM _appointment = new AppointmentVM();
                AppointmentVM _obj = _appointment.SelectObject(appointmentId);
                _obj.Done = false;
             
                _obj.Create();
                CallBoardVM callBoardVm = new CallBoardVM();
                callBoardVm.AppointmentId = _obj.Id;
                callBoardVm.VisitDate = DateTime.Now;
                int count = callBoardVm.GetPatientNumber(DateTime.Now);
                callBoardVm.PatientNumber = count + 1;
                callBoardVm.ClinicId = _obj.ClinicId;
                callBoardVm.VisitDate = _obj.AppointmentDate;
                callBoardVm.IsOnCall = false;
                callBoardVm.PatientId = _obj.PatientId;
                callBoardVm.Done = false;
                callBoardVm.Create();
                finalResult = _obj.PatientId;


            }
            catch (Exception e)
            {
                finalResult = 0;
            }
            return finalResult;
        }
        private void fillViewBagsCreateNew()
        {
            ClinicVM clinicVM = new ClinicVM();
            ViewBag.clinicList = clinicVM.ClinicSelectList();

            PatientVM patientVm = new PatientVM();
            ViewBag.PatientList = patientVm.PatientFullSelectList();
            

        }
        
    }
}