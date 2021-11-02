using Hospital.DAL;
using RashidHospital.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static RashidHospital.Helper.Enum;

namespace RashidHospital.Models
{
    public class MedicalConditionVM : BusinessBaseClass<MedicalCondition, MedicalConditionVM>
    {
        public int Id { get; set; }

        public string Condition { get; set; }

        public DateTime? ConditionDate { get; set; }
        [Required]

        public int PatientId { get; set; }

        public int ConditionType { get; set; }
        public int HistroryType { get; set; }
        public string ConditionName { get; set; }

        public string HistoryName { get; set; }
        public string PatientName { get; set; }
        public string PatientMedicalId { get; set; }
        public Guid DoctorId { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? ModifiedBy { get; set; }

        public string DoctorName { get; set; }

        internal override MedicalCondition Convert(MedicalConditionVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new MedicalCondition
                {
                    Id = Obj.Id,
                    Condition = Obj.Condition,
                    ConditionDate = Obj.ConditionDate,
                    PatientId = Obj.PatientId,
                    ConditionType= Obj.ConditionType,
                    HistroryType= Obj.HistroryType,
                    DoctorId=Obj.DoctorId,
                    IsDeleted=Obj.IsDeleted,
                    ModifiedBy=Obj.ModifiedBy
                };
            }
            return _Obj;
        }

        internal override MedicalConditionVM Convert(MedicalCondition DbObj)
        {

            MedicalConditionVM ObjVM = new MedicalConditionVM();
            ObjVM.Id = DbObj.Id;
            ObjVM.Condition = DbObj.Condition;
            ObjVM.ConditionDate = DbObj.ConditionDate;
            ObjVM.PatientId = DbObj.PatientId;
            ObjVM.ConditionType = DbObj.ConditionType;
            ObjVM.HistroryType = DbObj.HistroryType;
            ObjVM.DoctorId = DbObj.DoctorId;
            ObjVM.IsDeleted = DbObj.IsDeleted;
            AspNetUser _doctor = new AspNetUser();
            AspNetUser Doctor= _doctor.Getobject(DbObj.DoctorId);
            ObjVM.DoctorName = Doctor?.FirstName+" "+ Doctor?.SecondName + " " +Doctor?.ThirdName;
            if (DbObj.HistroryType == 1)
            {
                var _MedicalHistory = (MedicalHistory)DbObj.ConditionType;
                var enumCOndition = EnumHelper<MedicalHistory>.GetDisplayValue(_MedicalHistory);
                ObjVM.ConditionName = enumCOndition.ToString();
                ObjVM.HistoryName = "MedicalHistory";

            }
            else if (DbObj.HistroryType == 2) {
                var _MedicalHistory = (SurgicalHistory)DbObj.ConditionType;
                var enumCOndition = EnumHelper<SurgicalHistory>.GetDisplayValue(_MedicalHistory);
                ObjVM.ConditionName = enumCOndition.ToString();
                ObjVM.HistoryName = "SurgicalHistory";
            }
            else if (DbObj.HistroryType==3) {
           
                ObjVM.HistoryName = "FamilyHistory";
            }
            else
            {
              
                ObjVM.HistoryName = "AllergyHistory";

            }
            PatientVM _PatientVM = new PatientVM();
           var patientObj = _PatientVM.SelectObject(ObjVM.PatientId);
            ObjVM.PatientName = patientObj.Name;
            ObjVM.PatientMedicalId = patientObj.MedicalID;
            ObjVM.ModifiedBy = DbObj.ModifiedBy;

            return ObjVM;
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
        public List<MedicalConditionVM> SelectAll()
        {
            MedicalConditionVM _Obj = new MedicalConditionVM();
            MedicalCondition _BClass = new MedicalCondition();
            List<MedicalCondition> dbList = _BClass.GetAll<MedicalCondition>().ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }

        public List<MedicalConditionVM> SelectAllByPatientId(int patientId)
        {
            MedicalConditionVM _Obj = new MedicalConditionVM();
            MedicalCondition _BClass = new MedicalCondition();
            List<MedicalCondition> dbList = _BClass.GetMedicalRecordsByPatientId(patientId).ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }

        public MedicalConditionVM SelectObject(int Id)
        {
            MedicalCondition _BClass = new MedicalCondition();

            MedicalConditionVM ClinicObject = Convert(_BClass.Getobject(Id));
            return ClinicObject;
        }

        public List<SelectListItem> GetMedicalSelectList()
        {
           return System.Enum.GetValues(typeof(MedicalHistory)).Cast<MedicalHistory>().Where(a => a.ToString() != "Allergy"|| a.ToString()!= "FamilyHistory"||a.ToString()!= "Hypertension"||a.ToString() != "Diabetes").Select(v => new SelectListItem
            {
               Text = EnumHelper<MedicalHistory>.GetDisplayValue(v),
               Value = ((int)v).ToString()
           }).ToList();
        }

        public List<SelectListItem> GetSurgicalSelectList()
        {
            return System.Enum.GetValues(typeof(SurgicalHistory)).Cast<SurgicalHistory>().Select(v => new SelectListItem
            {
                Text = EnumHelper<SurgicalHistory>.GetDisplayValue(v),
                Value = ((int)v).ToString()
            }).ToList();
        }

        #endregion
    }
}