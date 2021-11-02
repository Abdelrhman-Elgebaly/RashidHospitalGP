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
    public class RadiologyResultVM : BusinessBaseClass<RadiologyResult, RadiologyResultVM>
    {
        #region properties
        public int Id { get; set; }
        public int ProcedureType { get; set; }

        public int Site { get; set; }

        public bool Contrast { get; set; }

        public DateTime RadiologyDate { get; set; }
        public int Recist { get; set; }
        public bool IsDeleted { get; set; }


        [StringLength(50)]
        public string T { get; set; }

        [StringLength(50)]
        public string N { get; set; }

        [StringLength(10)]
        public string M { get; set; }
        public string Note { get; set; }
        public int PateintID { get; set; }
        public Guid CreatedBy { get; set; }
        public string DoctorName { get; set; }
        public string SiteString { get; set; }
        public string ProcString { get; set; }
        public string Recisttring { get; set; }
        public Guid? ModifiedBy { get; set; }

        #endregion

        internal override RadiologyResult Convert(RadiologyResultVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new RadiologyResult
                {
                    Id = Obj.Id,
                    Contrast=Obj.Contrast,
                    CreatedBy=Obj.CreatedBy,
                    M=Obj.M,
                    N=Obj.N,
                    PateintID=Obj.PateintID,
                    ProcedureType=Obj.ProcedureType,
                    RadiologyDate=Obj.RadiologyDate,
                    Recist=Obj.Recist,
                    Note=Obj.Note,
                    Site=Obj.Site,
                    T=Obj.T,
                    IsDeleted=Obj.IsDeleted
                };
            }
            return _Obj;
        }

        internal override RadiologyResultVM Convert(RadiologyResult DbObj)
        {
            RadiologySite Site =(RadiologySite)DbObj?.Site;
            var enumSite = DbObj?.Site != 0? EnumHelper<RadiologySite>.GetDisplayValue(Site):string.Empty;

            RadiologyProcedureType Proc = (RadiologyProcedureType)DbObj?.ProcedureType;
            var enumProc = DbObj?.ProcedureType != 0 ? EnumHelper<RadiologyProcedureType>.GetDisplayValue(Proc):string.Empty;

            RadiologyRecist recist = (RadiologyRecist)DbObj?.Recist;
            var enumrecist = DbObj?.ProcedureType != 0 ? EnumHelper<RadiologyRecist>.GetDisplayValue(recist) : string.Empty;

            return new RadiologyResultVM()
            {
                Id = DbObj.Id,
                Contrast = DbObj.Contrast,
                CreatedBy = DbObj.CreatedBy,
                M = DbObj.M,
                N = DbObj.N,
                PateintID = DbObj.PateintID,
                ProcedureType = DbObj.ProcedureType,
                RadiologyDate = DbObj.RadiologyDate,
                Recist = DbObj.Recist,
                Site = DbObj.Site,
                T = DbObj.T,
                SiteString = enumSite,
                ProcString = enumProc,
                Recisttring=enumrecist,
                IsDeleted=DbObj.IsDeleted,
                DoctorName = DbObj.AspNetUser.FirstName + " " + DbObj.AspNetUser.SecondName + "  " + DbObj.AspNetUser.ThirdName,
                Note = DbObj.Note
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
        public List<RadiologyResultVM> SelectAll()
        {
            RadiologyResultVM _Obj = new RadiologyResultVM();
            RadiologyResult _BClass = new RadiologyResult();
            List<RadiologyResult> dbList = _BClass.GetAll<RadiologyResult>().ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }
        public List<RadiologyResultVM> SelectAllByPatientId(int PatientId)
        {
            RadiologyResultVM _Obj = new RadiologyResultVM();
            RadiologyResult _BClass = new RadiologyResult();
            List<RadiologyResult> dbList = _BClass.GetRadiologyResultByPatientId(PatientId).ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }
        public RadiologyResultVM SelectObject(int Id)
        {
            RadiologyResult _BClass = new RadiologyResult();

            RadiologyResultVM ClinicObject = Convert(_BClass.Getobject(Id));
            return ClinicObject;
        }
        public List<SelectListItem> GetProcedureTypeSelectList()
        {
            return System.Enum.GetValues(typeof(RadiologyProcedureType)).Cast<RadiologyProcedureType>().Select(v => new SelectListItem
            {
                Text = EnumHelper<RadiologyProcedureType>.GetDisplayValue(v),
                Value = ((int)v).ToString()
            }).ToList();
        }

        public List<SelectListItem> GeRadiologySiteSelectList()
        {
            return System.Enum.GetValues(typeof(RadiologySite)).Cast<RadiologySite>().Select(v => new SelectListItem
            {
                Text = EnumHelper<RadiologySite>.GetDisplayValue(v),
                Value = ((int)v).ToString()
            }).ToList();
        }
        public List<SelectListItem> GetRadiologyRacistSelectList()
        {
            return System.Enum.GetValues(typeof(RadiologyRecist)).Cast<RadiologyRecist>().Select(v => new SelectListItem
            {
                Text = EnumHelper<RadiologyRecist>.GetDisplayValue(v),
                Value = ((int)v).ToString()
            }).ToList();
        }

        #endregion
    }
}