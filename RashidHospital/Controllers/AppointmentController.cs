using Microsoft.AspNet.Identity;
using RashidHospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RashidHospital.Controllers
{
    [Authorize(Roles = "Admin,Doctor,Assistant,Consultant,Residents,Nurses,physician")]
    public class AppointmentController : Controller
    {
        // GET: Appointment
        public ActionResult Index()
        {
            AppointmentVM appointmntVm = new AppointmentVM();
          List<AppointmentVM> AppointmentList =  appointmntVm.SelectAllByDate(DateTime.Now).OrderByDescending(a => a.AppointmentDate).ToList();
            var userID = User.Identity.GetUserId();
            ViewBag.DoctorId = userID;
            return View(AppointmentList);
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
            return View();
        }

        //CreateNew
        [HttpGet]
        public ActionResult CreateNew()
        {
            fillViewBagsCreateNew();
            return View();
        }
        private void fillViewBags(int PatientId) {
            ClinicVM clinicVM = new ClinicVM();
            ViewBag.clinicList = clinicVM.ClinicSelectList();

            PatientVM patientVm = new PatientVM();
            PatientVM patientObj = patientVm.SelectObject(PatientId);
            ViewBag.PatientInfo = patientObj.Name + " - " + patientObj.MedicalID + "-" + patientObj.DiagnoseName + "-Register Date: " + patientObj.CreatedDate.ToShortDateString();

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
                    return RedirectToAction("Index");
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
                AppointmentVM _appointment = new AppointmentVM();
                AppointmentVM _obj = _appointment.SelectObject(appointmentId);
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
                callBoardVm.IsSkipped = false;
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
                callBoardVm.IsSkipped = false;
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