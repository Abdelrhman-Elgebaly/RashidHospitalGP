using Hospital.DAL;
using RashidHospital.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RashidHospital.Models
{
    public class MedicalRecordVM : BusinessBaseClass<MedicalRecord, MedicalRecordVM>
    {

        public int Id { get; set; }

        public string Complain { get; set; }

        public string Diagnose { get; set; }

       
        public string Recommendation { get; set; }
        [Required]
        public DateTime RecordDate { get; set; }
        public bool Flag { get; set; }

        public Guid DoctorID { get; set; }
        public string DoctorName { get; set; }
        public string ClinicName { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? ModifiedBy { get; set; }


        [Required]
        public int ClinicId { get; set; }

        public int PatientID { get; set; }
        internal override MedicalRecordVM Convert(MedicalRecord Obj)
        {
            if (Obj != null){
                MedicalRecordVM _record = new MedicalRecordVM();
                _record.Id = Obj.Id;
                _record.ClinicId = Obj.ClinicId;
                _record.Complain = Obj.Complain;
                _record.Diagnose = Obj.Diagnose;
                _record.DoctorID = Obj.DoctorID;
                _record.PatientID = Obj.PatientID;
                _record.Flag = Obj.Flag;
                _record.Recommendation = Obj.Recommendation;
                _record.RecordDate = Obj.RecordDate;
                _record.ClinicName = Obj.Clinic?.Name;
                _record.IsDeleted = Obj.IsDeleted;
                _record.DoctorName = Obj.AspNetUser?.FirstName + " " + Obj.AspNetUser?.SecondName + " " + Obj.AspNetUser?.ThirdName;
                _record.ModifiedBy = Obj.ModifiedBy;
                return _record;
            }
            else return null;
        }

        internal override MedicalRecord Convert(MedicalRecordVM Obj)
        {
            if (Obj == null)
                return null;

            return new MedicalRecord()
            {
                Id = Obj.Id,
                ClinicId = Obj.ClinicId,
                Complain = Obj.Complain,
                Diagnose = Obj.Diagnose,
                DoctorID = Obj.DoctorID,
                PatientID = Obj.PatientID,
                Recommendation = Obj.Recommendation,
                RecordDate= Obj.RecordDate,
                Flag=Obj.Flag,
                 IsDeleted=Obj.IsDeleted,
                 ModifiedBy=Obj.ModifiedBy
            };
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
        public List<MedicalRecordVM> SelectAll()
        {
            MedicalRecordVM _Obj = new MedicalRecordVM();
            MedicalRecord _BClass = new MedicalRecord();
            List<MedicalRecord> dbList = _BClass.GetAll<MedicalRecord>().ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }

        public MedicalRecordVM SelectObject(int Id)
        {
            MedicalRecord _BClass = new MedicalRecord();

            MedicalRecordVM ClinicObject = Convert(_BClass.Getobject(Id));
            return ClinicObject;
        }
        public List<MedicalRecordVM> SelectAllByPatientId(int patientId)
        {
            MedicalRecordVM _Obj = new MedicalRecordVM();
            MedicalRecord _BClass = new MedicalRecord();
            List<MedicalRecord> dbList = _BClass.GetMedicalRecordsByPatientId(patientId).ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }

        #endregion
    }
}