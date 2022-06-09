
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

    //PatientDoseController
    public class PatientDoseVM : BusinessBaseClass<PatientDose, PatientDoseVM>
    {
        [Key]
        public int ID { get; set; }
        public Nullable<int> Patient_ID { get; set; }

        public Nullable<int> NurseNote_ID { get; set; }

        public Nullable<int> Template_ID { get; set; }
        public Nullable<int> Cycle_ID { get; set; }
        public Nullable<double> Dose_Calculated { get; set; }




        internal override PatientDose Convert(PatientDoseVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new PatientDose
                {
                    ID = Obj.ID,
                    Patient_ID = Obj.Patient_ID,
                    NurseNote_ID = Obj.NurseNote_ID,
                    Template_ID = Obj.Template_ID,
                    Cycle_ID = Obj.Cycle_ID,
                    Dose_Calculated = Obj.Dose_Calculated,
           


                };
            }
            return _Obj;
        }


        internal override PatientDoseVM Convert(PatientDose DbObj)
        {
            PatientDoseVM pl = new PatientDoseVM();

            pl.ID = DbObj.ID;
            pl.Patient_ID = DbObj.Patient_ID;
            pl.NurseNote_ID = DbObj.NurseNote_ID;
            pl.Template_ID = DbObj.Template_ID;
            pl.Cycle_ID = DbObj.Cycle_ID;
            pl.Dose_Calculated = DbObj.Dose_Calculated;
       

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

        /*
        public List<NurseNoteVM> SelectAllByPatientId(int PatientId)
        {
            NurseNote _BClass = new NurseNote();
            List<NurseNote> dbList = _BClass.GetAllNurseNoteByPatientId(PatientId).ToList();
            return dbList.Select(z => Convert(z)).ToList();
        }
        */
        public PatientDoseVM SelectObject(int Id)
        {
            PatientDose _BClass = new PatientDose();
            PatientDoseVM Object = Convert(_BClass.Getobject(Id));
            return Object;
        }
    }
}