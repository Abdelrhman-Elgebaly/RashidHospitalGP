using RashidHospital.Helper;
using System;
using Hospital.DAL;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace RashidHospital.Models
{
    public class CallBoardVM : BusinessBaseClass<CallBoard, CallBoardVM>
    {

        public int Id { get; set; }
        public int ClinicId { get; set; }

        public int PatientId { get; set; }
        public string ClinicName { get; set; }
        public string PatientName { get; set; }
        public string PatientMedicalId { get; set; }
        public string Diagnose { get; set; }
        public string PatientUnit { get; set; }

        public DateTime VisitDate { get; set; }

        public int PatientNumber { get; set; }

        public bool IsSkipped { get; set; }

        public int AppointmentId { get; set; }

        public int? CallsNo { get; set; }
        public DateTime? LastCallTime { get; set; }
        public string CallTime { get; set; }
        public bool Done { get; set; }
        public Guid? DoctorId { get; set; }
        public bool IsDeleted { get; set; }

        public string DoctorName { get; set; }
        internal override CallBoard Convert(CallBoardVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new CallBoard
                {
                    Id = Obj.Id,
                    AppointmentId= Obj.AppointmentId,
                    CallsNo= Obj.CallsNo,
                    IsSkipped= Obj.IsSkipped,
                    LastCallTime= Obj.LastCallTime,
                    PatientId= Obj.PatientId,
                    PatientNumber= Obj.PatientNumber,
                    VisitDate=Obj.VisitDate,
                    ClinicId=Obj.ClinicId,
                    Done=Obj.Done,
                    DoctorId = Obj.DoctorId,
                    IsDeleted=Obj.IsDeleted
                };
            }
            return _Obj;
        }

        internal override CallBoardVM Convert(CallBoard DbObj)
        {
            CallBoardVM callboard = new CallBoardVM();
            callboard.Id = DbObj.Id;
            callboard.AppointmentId = DbObj.AppointmentId;
            callboard.CallsNo = DbObj.CallsNo != null? DbObj.CallsNo:0;
            callboard.IsSkipped = DbObj.IsSkipped;
            callboard.LastCallTime = DbObj.LastCallTime;
            callboard.CallTime = DbObj.LastCallTime != null? DbObj.LastCallTime.Value.ToShortTimeString():string.Empty;
            callboard.PatientId = DbObj.PatientId;
            callboard.PatientNumber = DbObj.PatientNumber;
            callboard.VisitDate = DbObj.VisitDate;
            callboard.ClinicId = DbObj.ClinicId;
            ClinicVM clinicVm = new ClinicVM();
            ClinicVM clinicObj = clinicVm.SelectObject(DbObj.ClinicId);
            callboard.ClinicName = clinicObj?.Name;
            PatientVM patientVm = new PatientVM();
            PatientVM patientObj = patientVm.SelectObject(DbObj.PatientId);
            callboard.PatientName = patientObj?.Name;
            callboard.PatientMedicalId = patientObj?.MedicalID;
            callboard.Diagnose = patientObj?.DiagnoseName;
            callboard.PatientUnit = patientObj?.PatientUnitName;
            callboard.Done = DbObj.Done;
            callboard.IsDeleted = DbObj.IsDeleted;
            callboard.DoctorId = DbObj?.DoctorId;
            if (DbObj.DoctorId != null) {
                AspNetUser user = new AspNetUser();
                AspNetUser _user = user.Getobject(DbObj.DoctorId);
                callboard.DoctorName = _user?.FirstName + " " + _user?.SecondName + " " + _user?.ThirdName;
            }


            return callboard;
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
        public List<CallBoardVM> SelectAll()
        {
            CallBoardVM _Obj = new CallBoardVM();
            CallBoard _BClass = new CallBoard();
            List<CallBoard> dbList = _BClass.GetAll<CallBoard>().ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }
        
        public List<CallBoardVM> getListByDate(DateTime date)
        {
            CallBoardVM _Obj = new CallBoardVM();
            CallBoard _BClass = new CallBoard();
            List<CallBoard> dbList = _BClass.getListByDate(date);
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }
        public CallBoardVM SelectObject(int Id)
        {
            CallBoard _BClass = new CallBoard();

            CallBoardVM _Object = Convert(_BClass.Getobject(Id));
            return _Object;
        }

        public int GetPatientNumber(DateTime date) {
            CallBoard _BClass = new CallBoard();
            return _BClass.GetPatientNumber(date);
        }

        public CallBoardVM getByDateandPatient(DateTime date,int PatientId) {
            try
            {
                CallBoard _BClass = new CallBoard();
                CallBoardVM _Object = Convert(_BClass.getByDateandPatient(date, PatientId));
                return _Object;
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
        #endregion
    }
}