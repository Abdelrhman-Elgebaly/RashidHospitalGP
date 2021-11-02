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
    public class RadioTherapyVM : BusinessBaseClass<RadioTherapy, RadioTherapyVM>
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public Guid DoctorId { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PatientID { get; set; }
        public int RTXSite { get; set; }
        public string RTXSiteString { get; set; }
        public int RTXType { get; set; }
        public string RTXTypeString { get; set; }
        public int TotalDose { get; set; }
        public int FractionNumber { get; set; }

        public int DoseFractionNumberResult { get; set; }

        public int[] Fixation { get; set; }
        public string FixationString { get; set; }
        public List<int> FixationListID { get; set; }
        public bool CocomittantChemoTherapy { get; set; }
        public int TypeOfTechnique { get; set; }
        public string TypeOfTechniqueString { get; set; }
        public string Note { get; set; }
        public string Attachment { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public bool Done { get; set; }
        public string DoctorName { get; set; }

        internal override RadioTherapy Convert(RadioTherapyVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new RadioTherapy
                {
                    Id = Obj.Id,
                    DoctorId = Obj.DoctorId,
                    IsDeleted = Obj.IsDeleted,
                    ModifiedBy = Obj.ModifiedBy,
                    AppointmentDate=Obj.AppointmentDate,
                    Attachment=Obj.Attachment,
                    CreatedDate=Obj.CreatedDate,
                    Done=Obj.Done,
                    DoseFractionNumberResult= Obj.TotalDose/Obj.FractionNumber,
                    CocomittantChemoTherapy=Obj.CocomittantChemoTherapy,
                    Note=Obj.Note,
                    FractionNumber=Obj.FractionNumber,
                    ModifiedDate=Obj.ModifiedDate,
                    PatientID=Obj.PatientID,
                    RTXSite=(int)Obj.RTXSite,
                    RTXType= (int)Obj.RTXType,
                    TotalDose=Obj.TotalDose,
                    TypeOfTechnique= (int)Obj.TypeOfTechnique,

                };
            }
            return _Obj;
        }

        internal override RadioTherapyVM Convert(RadioTherapy Obj)
        {
            var Site = (RTXSite)Obj.RTXSite;
            var enumSite = EnumHelper<RTXSite>.GetDisplayValue(Site);

            var type = (RTXType)Obj.RTXType;
            var enumtype = EnumHelper<RTXType>.GetDisplayValue(type);

            var TypeOfTechnique = (RadiologyProcedureType)Obj.TypeOfTechnique;
            var enumTypeOfTechnique = EnumHelper<RadiologyProcedureType>.GetDisplayValue(TypeOfTechnique);
            RadiioTherapiesFixationVM radiioTherapiesFixation = new RadiioTherapiesFixationVM();
            string FixationString= radiioTherapiesFixation.RadiioTherapiesFixationString(Obj.Id);
            int[] FixationId = radiioTherapiesFixation.RadiioTherapiesListFixationId(Obj.Id);

            RadioTherapyVM _Obj = new RadioTherapyVM {
                Id = Obj.Id,
                DoctorId = Obj.DoctorId,
                IsDeleted = Obj.IsDeleted,
                ModifiedBy = Obj.ModifiedBy,
                AppointmentDate = Obj.AppointmentDate,
                Attachment = Obj.Attachment,
                CreatedDate = Obj.CreatedDate,
                Done = Obj.Done,
                DoseFractionNumberResult = Obj.DoseFractionNumberResult,
                CocomittantChemoTherapy = Obj.CocomittantChemoTherapy,
                Note = Obj.Note,
                FractionNumber = Obj.FractionNumber,
                ModifiedDate = Obj.ModifiedDate,
                PatientID = Obj.PatientID,
                RTXSiteString = enumSite,
                RTXTypeString = enumtype,
                TotalDose = Obj.TotalDose,
                TypeOfTechniqueString = enumTypeOfTechnique,
                DoctorName = Obj.AspNetUser.FirstName + " " + Obj.AspNetUser.SecondName + "  " + Obj.AspNetUser.ThirdName,
                RTXSite = Obj.RTXSite,
                RTXType = Obj.RTXType,
                TypeOfTechnique = Obj.TypeOfTechnique,
                FixationString = FixationString,
                Fixation = FixationId,
                FixationListID=FixationId.ToList()
            };
            
            return _Obj;
        }
        public void Create()
        {
            _Obj = Convert(this);
            _Obj.AddNew();
            RadiioTherapiesFixationVM fixationVM = new RadiioTherapiesFixationVM();
            foreach (int item in this.Fixation)
            {
                fixationVM.FixationsId = item;
                fixationVM.RadioTherapyId = _Obj.Id;
                fixationVM.Create();

            }
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
        public RadioTherapyVM SelectObject(int Id)
        {
            RadioTherapy _BClass = new RadioTherapy();
            RadioTherapyVM _Object = Convert(_BClass.Getobject(Id));
            return _Object;
        }
        public List<RadioTherapyVM> SelectAllByPatientId(int patientId)
        {
            RadioTherapyVM _Obj = new RadioTherapyVM();
            RadioTherapy _BClass = new RadioTherapy();
            List<RadioTherapy> dbList = _BClass.GetRadioTherapyByPatientId(patientId).ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }

    }
}