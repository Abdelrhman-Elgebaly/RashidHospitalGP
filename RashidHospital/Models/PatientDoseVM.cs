using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hospital.DAL;
using RashidHospital.Helper;
using static RashidHospital.Helper.Enum;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hospital.DAL;
using RashidHospital.Models;
using System.Text.RegularExpressions;

using Microsoft.AspNet.Identity;
//NurseNoteController
namespace RashidHospital.Models
{

    //PatientDoseController
    public class PatientDoseVM : BusinessBaseClass<PatientDose, PatientDoseVM>
    {
        [Key]
        public int ID { get; set; }
        public int Patient_ID { get; set; }
        public int MainDrugId { get; set; }


        public Nullable<int> NurseNote_ID { get; set; }

        public Nullable<int> Template_ID { get; set; }
        public int Cycle_ID { get; set; }
        public Nullable<double> Dose_Calculated { get; set; }
        public string Therapy_Type { get; set; }
        //
        public string Drug_Name { get; set; }
        public double Drug_Dose { get; set; }

        public string Fluid_Vol { get; set; }
        public string Unit_Value { get; set; }
        public int Pharmacy_Condition { get; set; }
        public string Pharmacist_Note { get; set; }

        public bool IsEditByDoctor { get; set; }
        public bool IsEditByPharmacy { get; set; }
        public bool IsApproved { get; set; }
        public string PatientName { get; set; }

        public Nullable<int> Sequence_Number { get; set; }
        public string Route_Value { get; set; }
        public string Fluid_Type_Value { get; set; }
        public string Duration { get; set; }
        public string Pharmacy_Condition_Value { get; set; }
        public Nullable<DateTime> Date { get; set; }




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
                    Therapy_Type = Obj.Therapy_Type,
                    Drug_Name = Obj.Drug_Name,
                    Drug_Dose = Obj.Drug_Dose,
                    Unit_Value = Obj.Unit_Value,
                    Fluid_Vol = Obj.Fluid_Vol,
                    Pharmacy_Condition = Obj.Pharmacy_Condition,
                    Pharmacist_Note = Obj.Pharmacist_Note,

                    IsEditByDoctor = Obj.IsEditByDoctor,
                    IsEditByPharmacy = Obj.IsEditByPharmacy,
                    IsApproved = Obj.IsApproved,
                    MainDrugId = Obj.MainDrugId,

                    Date = Obj.Date,


                };
            }
            return _Obj;
        }


        internal override PatientDoseVM Convert(PatientDose DbObj)
        {

     
            PatientDoseVM pl = new PatientDoseVM();
            //  var Site = (DrugOrder)DbObj?.Pharmacy_Condition;
            //  var enumType = EnumHelper<DrugOrder>.GetDisplayValue(Site);
            pl.ID = DbObj.ID;
            pl.Patient_ID = DbObj.Patient_ID;
            pl.NurseNote_ID = DbObj.NurseNote_ID;
            pl.Template_ID = DbObj.Template_ID;
            pl.Cycle_ID = DbObj.Cycle_ID;
            pl.Dose_Calculated = DbObj.Dose_Calculated;
            pl.Therapy_Type = DbObj.Therapy_Type;
            //
            pl.Drug_Name = DbObj.Drug_Name;
            pl.Drug_Dose = DbObj.Drug_Dose;
            pl.Unit_Value = DbObj.Unit_Value;
            pl.Fluid_Vol = DbObj.Fluid_Vol;
            pl.Pharmacist_Note = DbObj.Pharmacist_Note;
            pl.IsEditByDoctor = DbObj.IsEditByDoctor;
            pl.IsEditByPharmacy = DbObj.IsEditByPharmacy;
            pl.IsApproved = DbObj.IsApproved;
            pl.MainDrugId = DbObj.MainDrugId;
            pl.Pharmacy_Condition = DbObj.Pharmacy_Condition;
            pl.Date = DbObj.Date;
            //  pl.Pharmacy_Condition_Value = enumType.ToString();
            PatientVM Obj = new PatientVM();

            PatientVM Objm = Obj.SelectObject(DbObj.Patient_ID);
            pl.PatientName = Objm.Name;

            ChemoTherapyDrugVM dObj = new ChemoTherapyDrugVM();

            ChemoTherapyDrugVM dObjm = dObj.SelectObject(DbObj.MainDrugId);
            pl.Sequence_Number = dObjm.Sequence_Number;
            pl.Route_Value = dObjm.Route_Value;
            pl.Fluid_Type_Value = dObjm.Fluid_Type_Value;
            pl.Duration = dObjm.Duration;

      
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


        public List<PatientDoseVM> SelectAll(int NoteId, int CycleId)
        {
            PatientDose _BClass = new PatientDose();
            List<PatientDose> dbList = _BClass.GetAllPatientDoses(NoteId, CycleId).ToList();
            return dbList.Select(z => Convert(z)).ToList();
        }

        public PatientDoseVM SelectObject(int Id)
        {
            PatientDose _BClass = new PatientDose();
            PatientDoseVM Object = Convert(_BClass.Getobject(Id));
            return Object;
        }

        public List<PatientDoseVM> SelectAllFinalApproved()
        {
            PatientDoseVM _Obj = new PatientDoseVM();
            PatientDose _BClass = new PatientDose();
            List<PatientDose> dbList = _BClass.GetAllFinalApproved().ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }
        /*
                public List<SelectListItem> GetOrderSelectList()
                {
                    return System.Enum.GetValues(typeof(DrugOrder)).Cast<DrugOrder>().Select(v => new SelectListItem
                    {
                        Text = EnumHelper<DrugOrder>.GetDisplayValue(v),
                        Value = ((int)v).ToString()
                    }).ToList();
                }
        */


    }
}