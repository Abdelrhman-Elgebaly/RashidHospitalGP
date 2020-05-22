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
    public class LabResualtVM : BusinessBaseClass<LabResualt, LabResualtVM>
    {
        public int Id { get; set; }

        [Required]
        public string Note { get; set; }
        public string Unit { get; set; }

        public DateTime ResualtDate { get; set; }

        public int PatientId { get; set; }

        public int LabType { get; set; }
        public string TypeValue { get; set; }
        public Guid DoctorId { get; set; }
        public string DoctorName { get; set; }

        public bool IsDeleted { get; set; }

        internal override LabResualt Convert(LabResualtVM Obj)
        {
            return new LabResualt
            {
                Id = Obj.Id,
                Note = Obj.Note,
                ResualtDate = Obj.ResualtDate,
                LabType = Obj.LabType,
                DoctorId = Obj.DoctorId,
                PatientId = Obj.PatientId,
                Unit=Obj.Unit,
                IsDeleted=IsDeleted
            };
        }

        internal override LabResualtVM Convert(LabResualt Obj)
        {
            var Site = (LabTyps)Obj?.LabType;
            var enumType = EnumHelper<LabTyps>.GetDisplayValue(Site);

            return new LabResualtVM
            {
                Id = Obj.Id,
                Note = Obj.Note,
                ResualtDate = Obj.ResualtDate,
                LabType = Obj.LabType,
                TypeValue = enumType.ToString(),
                PatientId = Obj.PatientId,
                DoctorId=Obj.DoctorId,
                 IsDeleted=Obj.IsDeleted,
                DoctorName = Obj.Doctor.FirstName + " " + Obj.Doctor.SecondName + " " + Obj.Doctor.ThirdName ,
                Unit=Obj.Unit
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
        public List<LabResualtVM> SelectAllByPatientId(int PatientId)
        {
            LabResualtVM _Obj = new LabResualtVM();
            LabResualt _BClass = new LabResualt();
            List<LabResualt> dbList = _BClass.GetLabResualtByPatientId(PatientId).ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }
        public LabResualtVM SelectObject(int Id)
        {
            LabResualt _BClass = new LabResualt();
            LabResualtVM ClinicObject = Convert(_BClass.Getobject(Id));
            return ClinicObject;
        }

        public List<SelectListItem> GeLabTypsSelectList()
        {
            return System.Enum.GetValues(typeof(LabTyps)).Cast<LabTyps>().Select(v => new SelectListItem
            {
                Text = EnumHelper<LabTyps>.GetDisplayValue(v),
                Value = ((int)v).ToString()
            }).ToList();
        }


        #endregion
    }
}