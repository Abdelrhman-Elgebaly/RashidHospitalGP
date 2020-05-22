using Hospital.DAL;
using RashidHospital.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static RashidHospital.Helper.Enum;

namespace RashidHospital.Models
{
    public class RadiologyRequestVM : BusinessBaseClass<RadiologyRequest, RadiologyRequestVM>
    {
        public int Id { get; set; }
        public int ProcedureType { get; set; }

        public int Site { get; set; }

        public bool Contrast { get; set; }

        public string Note { get; set; }

        public DateTime RequestDate { get; set; }
        public bool IsDeleted { get; set; }

        public int PateintID { get; set; }
        public Guid CreatedBy { get; set; }
        public string DoctorName { get; set; }
        public string SiteString { get; set; }
        public string ProcString { get; set; }
        public string RadiologyValuestring { get; set; }
        internal override RadiologyRequest Convert(RadiologyRequestVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new RadiologyRequest
                {
                    Id = Obj.Id,
                    Contrast=Obj.Contrast,
                    CreatedBy=Obj.CreatedBy,
                    Note=Obj.Note,
                    PateintID=Obj.PateintID,
                    ProcedureType=Obj.ProcedureType,
                    RequestDate=Obj.RequestDate,
                    Site=Obj.Site,
                    IsDeleted=Obj.IsDeleted
                    
                };
            }
            return _Obj;
        }

        internal override RadiologyRequestVM Convert(RadiologyRequest DbObj)
        {
            var Site = (RadiologySite)DbObj?.Site;
            var enumSite = EnumHelper<RadiologySite>.GetDisplayValue(Site);

            var Proc = (RadiologyProcedureType)DbObj?.ProcedureType;
            var enumProc = EnumHelper<RadiologyProcedureType>.GetDisplayValue(Proc);

            return new RadiologyRequestVM()
            {
                Id = DbObj.Id,
                Contrast = DbObj.Contrast,
                CreatedBy = DbObj.CreatedBy,
                Note = DbObj.Note,
                PateintID = DbObj.PateintID,
                ProcedureType = DbObj.ProcedureType,
                RequestDate = DbObj.RequestDate,
                Site = DbObj.Site,
                SiteString = enumSite.ToString(),
                ProcString = enumProc.ToString(),
                 IsDeleted=DbObj.IsDeleted,
                DoctorName=DbObj.AspNetUser.FirstName +" "+ DbObj.AspNetUser.SecondName+"  "+ DbObj.AspNetUser.ThirdName
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
        public List<RadiologyRequestVM> SelectAll()
        {
            RadiologyRequestVM _Obj = new RadiologyRequestVM();
            RadiologyRequest _BClass = new RadiologyRequest();
            List<RadiologyRequest> dbList = _BClass.GetAll<RadiologyRequest>().ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }
        public List<RadiologyRequestVM> SelectAllByPatientId(int PatientId)
        {
            RadiologyRequestVM _Obj = new RadiologyRequestVM();
            RadiologyRequest _BClass = new RadiologyRequest();
            List<RadiologyRequest> dbList = _BClass.GetRadiologyRequestByPatientId(PatientId).ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }
        public RadiologyRequestVM SelectObject(int Id)
        {
            RadiologyRequest _BClass = new RadiologyRequest();

            RadiologyRequestVM ClinicObject = Convert(_BClass.Getobject(Id));
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

        #endregion
    }
}