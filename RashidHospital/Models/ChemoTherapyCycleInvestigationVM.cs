
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

    public class ChemoTherapyCycleInvestigationVM : BusinessBaseClass<ChemoTherapyCycleInvestigation, ChemoTherapyCycleInvestigationVM>
    {

        [Key]
        public int ID { get; set; }
        public Nullable<int> Cycle_ID { get; set; }
        public Nullable<int> Patient_ID { get; set; }

        public string Inves_Type { get; set; }
        public Nullable<int> Actual_Value { get; set; }

        public Nullable<int> Rule_Type { get; set; }
        public int TemplateId { get; set; }

        public string Rule_TypeValue { get; set; }
        public string Note { get; set; }


        public double Value { get; set; }
        internal override ChemoTherapyCycleInvestigation Convert(ChemoTherapyCycleInvestigationVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new ChemoTherapyCycleInvestigation
                {
                    ID = Obj.ID,
                    Patient_ID = Obj.Patient_ID,
                   
                    Cycle_ID = Obj.Cycle_ID,
                    Inves_Type =Obj.Inves_Type,
                    Rule_Type = Obj.Rule_Type,
                    Actual_Value = Obj.Actual_Value,
                    TemplateId = Obj.TemplateId,
                    Value =Obj.Value,
                    Note =Obj.Note,
                };
            }
            return _Obj;
        }


        internal override ChemoTherapyCycleInvestigationVM Convert(ChemoTherapyCycleInvestigation DbObj)
        {
            ChemoTherapyCycleInvestigationVM pl = new ChemoTherapyCycleInvestigationVM();

           

            var Site2 = (Rule)DbObj?.Rule_Type;
            var enumType2 = EnumHelper<Rule>.GetDisplayValue(Site2);

            pl.ID = DbObj.ID;

            pl.Patient_ID = DbObj.Patient_ID;
         
            pl.Cycle_ID = DbObj.Cycle_ID;
       
            pl.Rule_Type = DbObj.Rule_Type;
            pl.Rule_TypeValue = enumType2.ToString();
     

                 pl.Inves_Type = DbObj.Inves_Type;
            pl.Actual_Value = DbObj.Actual_Value;
            pl.TemplateId = DbObj.TemplateId;
            pl.Value = DbObj.Value;
            pl.Note = DbObj.Note;
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

        public List<ChemoTherapyCycleInvestigationVM> SelectAllByCycleID(int CycleID)
        {
            ChemoTherapyCycleInvestigationVM _Obj = new ChemoTherapyCycleInvestigationVM();
            ChemoTherapyCycleInvestigation _BClass = new ChemoTherapyCycleInvestigation();
            List<ChemoTherapyCycleInvestigation> dbList = _BClass.GetInvestigationByCycleID(CycleID).ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }
        public ChemoTherapyCycleInvestigationVM SelectObject(int Id)
        {
            ChemoTherapyCycleInvestigation _BClass = new ChemoTherapyCycleInvestigation();
            ChemoTherapyCycleInvestigationVM ClinicObject = Convert(_BClass.Getobject(Id));
            return ClinicObject;
        }


   

















    }
}