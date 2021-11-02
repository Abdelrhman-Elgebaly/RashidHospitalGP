using Hospital.DAL;
using RashidHospital;
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
    public class PathologyVM : BusinessBaseClass<Pathology, PathologyVM>
    {
        public int Id { get; set; }

        public string ProcedureType { get; set; }

        public DateTime Date { get; set; }

        [StringLength(50)]
        public string pT { get; set; }

        [StringLength(50)]
        public string pN { get; set; }

        [StringLength(50)]
        public string pM { get; set; }

        public string Comment { get; set; }

        public int TumerHistology { get; set; }
        public string TumerHistologyString { get; set; }

        public int? TumerGrade { get; set; }

        public int? TumerSize { get; set; }

        public int? TumerNumber { get; set; }

        public int? TumerFocality { get; set; }

        public bool? TumerLVI { get; set; }

        public bool? TumerPNI { get; set; }

        public int? TumerMargin { get; set; }

        [StringLength(50)]
        public string LymphnodeTypeOfsurgery { get; set; }

        [StringLength(50)]
        public string LymphnodePositiveLN { get; set; }

        [StringLength(50)]
        public string LymphnodeTotalLN { get; set; }

        public bool? LymphnodeECE { get; set; }

        public int PatientId { get; set; }
        public string PatientName { get; set; }

        public Guid DoctorId { get; set; }

        public string DoctorName { get; set; }
        public string TumerGradeName { get; set; }
        public string TumorHistologyName { get; set; }
        public string TumorMarginsName { get; set; }
        public string TumorFocalityName { get; set; }
        public bool IsDeleted { get; set; }

        public List<IHCVM> IHCVMList { get; set; }
        internal override PathologyVM Convert(Pathology Obj)
        {
            PathologyVM _obj = new PathologyVM();
            _obj.Comment = Obj.Comment;
            _obj.Date = Obj.Date;
            _obj.DoctorId = Obj.DoctorId;
            _obj.DoctorName = Obj.Doctors.FirstName + " " + Obj.Doctors.SecondName + " " + Obj.Doctors.ThirdName;
            _obj.Id = Obj.Id;
            _obj.LymphnodeECE = Obj.LymphnodeECE;
            _obj.LymphnodePositiveLN = Obj.LymphnodePositiveLN;
            _obj.LymphnodeTotalLN = Obj.LymphnodeTotalLN;
            _obj.LymphnodeTypeOfsurgery = Obj.LymphnodeTypeOfsurgery;
            _obj.PatientId = Obj.PatientId;
            _obj.PatientName = Obj.Patient.Name;
            _obj.pM = Obj.pM;
            _obj.pN = Obj.pN;
            _obj.ProcedureType = Obj.ProcedureType;
            _obj.pT = Obj.pT;
            _obj.TumerFocality = Obj.TumerFocality;
            _obj.TumerGrade = Obj.TumerGrade;
            TumerHistologyTypesVM _histologyType = new TumerHistologyTypesVM();
             _histologyType = _histologyType.SelectObject(Obj.TumerHistology);
                _obj.TumerHistologyString = _histologyType?.Title;
            _obj.TumerHistology = Obj.TumerHistology;
            _obj.TumerLVI = Obj.TumerLVI;
            _obj.TumerMargin = Obj.TumerMargin;
            _obj.TumerNumber = Obj.TumerNumber;
            _obj.TumerPNI = Obj.TumerPNI;
            _obj.IsDeleted = Obj.IsDeleted;
            _obj.TumerSize = Obj.TumerSize;
            if (Obj.TumerGrade != null) {
                var enumTumerGrade = (TumorGrade)Obj.TumerGrade;
                _obj.TumerGradeName = enumTumerGrade.ToString();
            }
          
            if(Obj.TumerMargin != null)
            {
                var enumTumorMargins = (TumorHistology)Obj.TumerMargin;
                _obj.TumorMarginsName = enumTumorMargins.ToString();
            }
            if (Obj.TumerFocality != null)
            {
                var enumTumorFocality = (TumorHistology)Obj.TumerFocality;
                _obj.TumorFocalityName = enumTumorFocality.ToString();
            }
            


            return _obj;

        }

        internal override Pathology Convert(PathologyVM Obj)
        {
            Pathology _obj = new Pathology();
            _obj.Comment = Obj.Comment;
            _obj.Date = Obj.Date;
            _obj.DoctorId = Obj.DoctorId;
            _obj.Id = Obj.Id;
            _obj.LymphnodeECE = Obj.LymphnodeECE;
            _obj.LymphnodePositiveLN = Obj.LymphnodePositiveLN;
            _obj.LymphnodeTotalLN = Obj.LymphnodeTotalLN;
            _obj.LymphnodeTypeOfsurgery = Obj.LymphnodeTypeOfsurgery;
            _obj.PatientId = Obj.PatientId;
            _obj.pM = Obj.pM;
            _obj.pN = Obj.pN;
            _obj.ProcedureType = Obj.ProcedureType;
            _obj.pT = Obj.pT;
            _obj.TumerFocality = Obj.TumerFocality;
            _obj.TumerGrade = Obj.TumerGrade;
            _obj.TumerHistology = Obj.TumerHistology;
            _obj.TumerLVI = Obj.TumerLVI;
            _obj.TumerMargin = Obj.TumerMargin;
            _obj.TumerNumber = Obj.TumerNumber;
            _obj.TumerPNI = Obj.TumerPNI;
            _obj.TumerSize = Obj.TumerSize;
            _obj.IsDeleted = Obj.IsDeleted;
            return _obj;
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
        public List<PathologyVM> SelectAllByPatientId(int PatientId)
        {
            PathologyVM _Obj = new PathologyVM();
            Pathology _BClass = new Pathology();
            List<Pathology> dbList = _BClass.GetPathologyByPatientId(PatientId).ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }


        public PathologyVM SelectObject(int Id)
        {
            Pathology _BClass = new Pathology();

            PathologyVM _Object = Convert(_BClass.Getobject(Id));
            return _Object;
        }

        public List<SelectListItem> GetTumerGradeSelectList()
        {

            return System.Enum.GetValues(typeof(TumorGrade)).Cast<TumorGrade>().Select(v => new SelectListItem
            {
                Text = EnumHelper<TumorGrade>.GetDisplayValue(v),
                Value = ((int)v).ToString()
            }).ToList();
        }
        public List<SelectListItem> GetTumorMarginsSelectList()
        {

            return System.Enum.GetValues(typeof(TumorMargins)).Cast<TumorMargins>().Select(v => new SelectListItem
            {
                Text = EnumHelper<TumorMargins>.GetDisplayValue(v),
                Value = ((int)v).ToString()
            }).ToList();
        }
        public List<SelectListItem> GetTumorFocalitySelectList()
        {

            return System.Enum.GetValues(typeof(TumorFocality)).Cast<TumorFocality>().Select(v => new SelectListItem
            {
                Text = EnumHelper<TumorFocality>.GetDisplayValue(v),
                Value = ((int)v).ToString()
            }).ToList();
        }
        #endregion
    }
    public class JsonIHC
    {
        public string Value { get; set; }
        public string Type { get; set; }
        public bool Result { get; set; }
        public string PatientId { get; set; }
    }
}