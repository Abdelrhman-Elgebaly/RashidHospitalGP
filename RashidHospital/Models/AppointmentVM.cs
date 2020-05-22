using Hospital.DAL;
using RashidHospital.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RashidHospital.Models
{
    public class AppointmentVM : BusinessBaseClass<Appointment, AppointmentVM>
    {
        public int Id { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{dd/mm/yyyy}")]
        public DateTime AppointmentDate { get; set; }

        public bool Done { get; set; }
        [Required]
        public int ClinicId { get; set; }
        [Required]
        public int PatientId { get; set; }
        public string ClinicName { get; set; }
        public string PatientName { get; set; }
        public string PatientMedicalId { get; set; }
        public string Diagnose { get; set; }
        public string PatientUnit { get; set; }
        public int AppointmentCountPerDay { get; set; }

        public Guid? DoctorId { get; set; }

        public string DoctorName { get; set; }
        public bool IsDeleted { get; set; }

        internal override Appointment Convert(AppointmentVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new Appointment
                {
                    Id = Obj.Id,
                    AppointmentDate = Obj.AppointmentDate,
                    ClinicId = Obj.ClinicId,
                    Done = Obj.Done,
                    PatientId= Obj.PatientId,
                    DoctorId=Obj.DoctorId,
                    IsDeleted=Obj.IsDeleted,
                };
            }
            return _Obj;
        }

        internal override AppointmentVM Convert(Appointment DbObj)
        {
            AppointmentVM appointment = new AppointmentVM();
            appointment.Id = DbObj.Id;
            appointment.AppointmentDate = DbObj.AppointmentDate;
            appointment.ClinicId = DbObj.ClinicId;
            appointment.Done = DbObj.Done;
            appointment.PatientId = DbObj.PatientId;
            ClinicVM clinicVm = new ClinicVM();
            ClinicVM clinicObj = clinicVm.SelectObject(DbObj.ClinicId);
            appointment.ClinicName = clinicObj?.Name;
            PatientVM patientVm = new PatientVM();
            PatientVM patientObj = patientVm.SelectObject(DbObj.PatientId);
            appointment.PatientName = patientObj?.Name;
            appointment.PatientMedicalId = patientObj?.MedicalID;
            appointment.Diagnose = patientObj?.DiagnoseName;
            appointment.PatientUnit = patientObj?.PatientUnitName;
            appointment.DoctorId = DbObj?.DoctorId;
            appointment.IsDeleted = DbObj.IsDeleted;
            if (DbObj.DoctorId != null)
            {
                AspNetUser user = new AspNetUser();
                AspNetUser _user = user.Getobject(DbObj.DoctorId);
                appointment.DoctorName = _user?.FirstName + " " + _user?.SecondName + " " + _user?.ThirdName;
            }


            return appointment;
        }

        #region Functions
        public void Create()
        {
            _Obj = Convert(this);
            _Obj.AddNew();
        }

        public void Edit()
        {
            _Obj = Convert(this);
            _Obj.Edit();
        }
        public void Delete()
        {
            _Obj = Convert(this);
            _Obj.Delete();
        }
        public List<AppointmentVM> SelectAll()
        {
            AppointmentVM _Obj = new AppointmentVM();
            Appointment _BClass = new Appointment();
            List<Appointment> dbList = _BClass.GetAll<Appointment>().ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }
        public List<AppointmentVM> SelectAllByDate(DateTime datetime)
        {
            AppointmentVM _Obj = new AppointmentVM();

            Appointment _BClass = new Appointment();
            List<Appointment> dbList = _BClass.GetAppointmentsByDate(datetime);
            List<AppointmentVM> ObjectList = dbList.Select(z => _Obj.Convert(z)).ToList();
            if (ObjectList != null)
                return ObjectList;
            else
                return null;
        }


        public AppointmentVM SelectObject(int Id)
        {
            Appointment _BClass = new Appointment();

            AppointmentVM ClinicObject = Convert(_BClass.Getobject(Id));
            return ClinicObject;
        }
        public bool ValidateDate(DateTime datetime)
        {
            Appointment _BClass = new Appointment();

            AppointmentVM Object = Convert(_BClass.Where(a=> a.AppointmentDate==datetime)?.FirstOrDefault());
            if (Object != null)
                return false;
            else
                return true;
        }
        public List<AppointmentVM> SelectAllByPatientId(int patientId,DateTime date)
        {
            AppointmentVM _Obj = new AppointmentVM();
            Appointment _BClass = new Appointment();
           List<Appointment> dbList = _BClass.GetAppointmentsByPatientId(patientId,date).ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }
        public bool CheckClinicAvailabilty(int ClinicId,DateTime appointmentDate) {
            Appointment _AppointmentClass = new Appointment();
            int appointmentsCounts = _AppointmentClass.GetClinicsAppointmentsCount(ClinicId, appointmentDate);
            Clinic _clinic = new Clinic();
           int? ReservCount= _clinic.Getobject(ClinicId)?.visitsPerDay;
            if (ReservCount!= null&& ReservCount <= appointmentsCounts)
                return false;
            else return true;
        }
        #endregion
    }
}