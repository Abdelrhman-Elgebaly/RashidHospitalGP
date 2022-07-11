using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hospital.DAL;
using RashidHospital.Helper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using static RashidHospital.Helper.Enum;
using System.Web.Mvc;
namespace RashidHospital.Models
{

    public class ChemoTherapyDoseVM : BusinessBaseClass<ChemoTherapyDose, ChemoTherapyDoseVM>
    {

        [Key]
        public int ID { get; set; }
        public Nullable<int> Patient_ID { get; set; }
        public Nullable<int> Drug_ID { get; set; }
        public Nullable<int> Dose { get; set; }
        public Nullable<int> Note_ID { get; set; }



        internal override ChemoTherapyDose Convert(ChemoTherapyDoseVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new ChemoTherapyDose
                {
                    ID = Obj.ID,
                    Patient_ID = Obj.Patient_ID,
                    Drug_ID = Obj.Drug_ID,
                    Dose = Obj.Dose,
                    Note_ID = Obj.Note_ID,

                };
            }
            return _Obj;
        }


        internal override ChemoTherapyDoseVM Convert(ChemoTherapyDose DbObj)
        {


            ChemoTherapyDoseVM pl = new ChemoTherapyDoseVM();



            pl.ID = DbObj.ID;
            pl.Patient_ID = DbObj.Patient_ID;
            pl.Drug_ID = DbObj.Drug_ID;
            pl.Dose = DbObj.Dose;
            pl.Note_ID = DbObj.Note_ID;


            return pl;
        }



        public void Create()
        {
            _Obj = Convert(this);
            _Obj.AddNew();
        }

        public List<ChemoTherapyDoseVM> SelectAllByNoteID(int PatientId)
        {
            ChemoTherapyDoseVM _Obj = new ChemoTherapyDoseVM();
            ChemoTherapyDose _BClass = new ChemoTherapyDose();
            List<ChemoTherapyDose> dbList = _BClass.GetDoseByNoteId(PatientId).ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }



        public ChemoTherapyDoseVM SelectObject(int Id)
        {
            ChemoTherapyDose _BClass = new ChemoTherapyDose();
            ChemoTherapyDoseVM ClinicObject = Convert(_BClass.Getobject(Id));
            return ClinicObject;
        }






    }
}