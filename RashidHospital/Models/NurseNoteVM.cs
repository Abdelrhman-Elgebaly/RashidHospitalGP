using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hospital.DAL;
using RashidHospital.Helper;
using static RashidHospital.Helper.Enum;
using System.ComponentModel.DataAnnotations;
//NurseNoteController
namespace RashidHospital.Models
{
    public class NurseNoteVM : BusinessBaseClass<NurseNote, NurseNoteVM>
    {

        [Key]
        public int ID { get; set; }
        public Nullable<int> Patient_ID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> Temperature { get; set; }
        public Nullable<int> Pulse { get; set; }
        public Nullable<int> BP { get; set; }
        public Nullable<double> Weight { get; set; }
        public Nullable<double> Height { get; set; }
        public Nullable<double> SA { get; set; }
        public bool IsApproved { get; set; }
        public Nullable<double> Dose_Calculated { get; set; }
        public Nullable<int> CycleId { get; set; }
        public string Pharmacist_Note { get; set; }
        public Nullable<bool> IsSelected { get; set; }

        
        internal override NurseNote Convert(NurseNoteVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new NurseNote
                {
                    ID = Obj.ID,
                    Patient_ID = Obj.Patient_ID,
                    Date = Obj.Date,
                    Temperature = Obj.Temperature,
                    Pulse = Obj.Pulse,
                    BP = Obj.BP,
                    Weight = Obj.Weight,
                    Height = Obj.Height,
                    SA = Obj.SA,
                    IsApproved = Obj.IsApproved,
                    Dose_Calculated = Obj.Dose_Calculated,
                    Pharmacist_Note=Obj.Pharmacist_Note,
                    IsSelected = Obj.IsSelected,

                };
            }
            return _Obj;
        }


        internal override NurseNoteVM Convert(NurseNote DbObj)
        {
            NurseNoteVM pl = new NurseNoteVM();

            pl.ID = DbObj.ID;
            pl.Patient_ID = DbObj.Patient_ID;
            pl.Date = DbObj.Date;
            pl.Temperature = DbObj.Temperature;
            pl.Pulse = DbObj.Pulse;
            pl.BP = DbObj.BP;
            pl.Weight = DbObj.Weight;
            pl.Height = DbObj.Height;
            pl.SA = DbObj.SA;
            pl.IsApproved = DbObj.IsApproved;
            pl.Dose_Calculated = DbObj.Dose_Calculated;
            pl.Pharmacist_Note = DbObj.Pharmacist_Note;
            pl.IsSelected = DbObj.IsSelected;
            return pl;
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


        public List<NurseNoteVM> SelectAllByPatientId(int PatientId)
        {
            NurseNote _BClass = new NurseNote();
            List<NurseNote> dbList = _BClass.GetAllNurseNoteByPatientId(PatientId).ToList();
            return dbList.Select(z => Convert(z)).ToList();
        }

        public NurseNoteVM SelectObject(int Id)
        {
            NurseNote _BClass = new NurseNote();
            NurseNoteVM Object = Convert(_BClass.Getobject(Id));
            return Object;
        }






    }
}