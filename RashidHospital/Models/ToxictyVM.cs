using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hospital.DAL;
using RashidHospital.Helper;
using static RashidHospital.Helper.Enum;

namespace RashidHospital.Models
{
    public class ToxictyVM : BusinessBaseClass<Toxicty, ToxictyVM>
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        public string Description { get; set; }
        public int Condition { get; set; }
        public string Note { get; set; }
        public string Pharmacist_Note { get; set; }

        public int ToxictyTypeId { get; set; }
        public string ToxictyTypeName { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? DoctorId { get; set; }
        public int PatientID { get; set; }
        public DateTime ToxictyDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string DoctorName { get; set; }
        public virtual ToxictyType ToxictyTypes { get; set; }

        internal override Toxicty Convert(ToxictyVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new Toxicty
                {
                    Id = Obj.Id,
                    Condition = Obj.Condition,
                    Description=Obj.Description,
                    CreatedDate=Obj.CreatedDate,
                    Grade=Obj.Grade,
                    ModifiedDate=Obj.ModifiedDate,
                    Note=Obj.Note,
                    PatientID=Obj.PatientID,
                    ToxictyTypeId=ToxictyTypeId,
                    DoctorId = Obj.DoctorId,
                    IsDeleted = Obj.IsDeleted,
                    ModifiedBy = Obj.ModifiedBy,
                    ToxictyDate=Obj.ToxictyDate,
                    Pharmacist_Note = Obj.Pharmacist_Note,

    };
            }
            return _Obj;
        }

        internal override ToxictyVM Convert(Toxicty Obj)
        {
            ToxictyTypeVM toxictyTypeVM = new ToxictyTypeVM();
            if (Obj == null)
                return null;
            else
            {
                ToxictyVM _Obj = new ToxictyVM
                {
                    Id = Obj.Id,
                    Condition = Obj.Condition,
                    Description = Obj.Description,
                    CreatedDate = Obj.CreatedDate,
                    Grade = Obj.Grade,
                    ModifiedDate = Obj.ModifiedDate,
                    Note = Obj.Note,
                    PatientID = Obj.PatientID,
                    ToxictyTypeId = Obj.ToxictyTypeId,
                    DoctorId = Obj.DoctorId,
                    IsDeleted = Obj.IsDeleted,
                    ModifiedBy = Obj.ModifiedBy,
                    ToxictyDate = Obj.ToxictyDate,
                    ToxictyTypeName =Obj.ToxictyTypes.Name,
                    DoctorName = Obj.AspNetUser.FirstName + " " + Obj.AspNetUser.SecondName + "  " + Obj.AspNetUser.ThirdName,
                    Pharmacist_Note = Obj.Pharmacist_Note,


                };
                return _Obj;
            }
           
        }

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
        public List<SelectListItem> GetToxictyGradeSelectList()
        {
            return System.Enum.GetValues(typeof(ToxictyGrade)).Cast<ToxictyGrade>().Select(v => new SelectListItem
            {
                Text = EnumHelper<ToxictyGrade>.GetDisplayValue(v),
                Value = ((int)v).ToString()
            }).ToList();
        }
        public List<SelectListItem> GetToxictyConditionSelectList()
        {
            return System.Enum.GetValues(typeof(ToxictyCondition)).Cast<ToxictyCondition>().Select(v => new SelectListItem
            {
                Text = EnumHelper<ToxictyCondition>.GetDisplayValue(v),
                Value = ((int)v).ToString()
            }).ToList();
        }

        public List<ToxictyVM> SelectAllByPatientId(int PatientId)
        {
            Toxicty _BClass = new Toxicty();
            List<Toxicty> dbList = _BClass.GetAllToxictyByPatientId(PatientId).ToList();
            return dbList.Select(z => Convert(z)).ToList();
        }

        public ToxictyVM SelectObject(int Id)
        {
            Toxicty _BClass = new Toxicty();
            ToxictyVM  Object = Convert(_BClass.Getobject(Id));
            return Object;
        }
    }
}