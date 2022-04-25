using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hospital.DAL;
using RashidHospital.Helper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace RashidHospital.Models
{

    public class ChemoTherapyDrugVM : BusinessBaseClass<ChemoTherapyDrug, ChemoTherapyDrugVM>
    {

        [Key]
        public int Drug_ID { get; set; }
        public int Template_ID { get; set; }
        public string Therapy_Type { get; set; }
        public string Days { get; set; }
        public Nullable<int> Sequence_Number { get; set; }
        public string Drug_Name { get; set; }
        public int Drug_Dose { get; set; }
        public string Fluid_Type { get; set; }
        public string Fluid_Vol { get; set; }
        public string Unit { get; set; }
        public string Route { get; set; }
        public string Duration { get; set; }
        public string Note { get; set; }





        internal override ChemoTherapyDrug Convert(ChemoTherapyDrugVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new ChemoTherapyDrug
                {
                    Drug_ID = Obj.Drug_ID,
                    Template_ID = Obj.Template_ID,
                    Therapy_Type = Obj.Therapy_Type,
                    Days = Obj.Days,
                    Sequence_Number = Obj.Sequence_Number,
                    Drug_Name = Obj.Drug_Name,
                    Drug_Dose = Obj.Drug_Dose,
                    Fluid_Type = Obj.Fluid_Type,
                    Fluid_Vol = Obj.Fluid_Vol,
                    Unit = Obj.Unit,
                    Route = Obj.Route,
                    Duration = Obj.Duration,
                    Note = Obj.Note,


                };
            }
            return _Obj;
        }
    
     

        internal override ChemoTherapyDrugVM Convert(ChemoTherapyDrug Obj)
        {
            ChemoTherapyDrugVM cd = new ChemoTherapyDrugVM();
            if (Obj == null)
                return null;
            else
            {
                ChemoTherapyDrugVM _Obj = new ChemoTherapyDrugVM
                {
                    Drug_ID = Obj.Drug_ID,
                    Template_ID = Obj.Template_ID,
                    Therapy_Type = Obj.Therapy_Type,
                    Days = Obj.Days,
                    Sequence_Number = Obj.Sequence_Number,
                    Drug_Name = Obj.Drug_Name,
                    Drug_Dose = Obj.Drug_Dose,
                    Fluid_Type = Obj.Fluid_Type,
                    Fluid_Vol = Obj.Fluid_Vol,
                    Unit = Obj.Unit,
                    Route = Obj.Route,
                    Duration = Obj.Duration,
                    Note = Obj.Note,


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
     

        public List<ChemoTherapyDrugVM> SelectAllByTemplateId(int TemplateId)
        {
            ChemoTherapyDrug _BClass = new ChemoTherapyDrug();
            List<ChemoTherapyDrug> dbList = _BClass.GetChemoTherapyDrugByTemplateId(TemplateId).ToList();
            return dbList.Select(z => Convert(z)).ToList();
        }

        public ChemoTherapyDrugVM SelectObject(int Id)
        {
            ChemoTherapyDrug _BClass = new ChemoTherapyDrug();
            ChemoTherapyDrugVM Object = Convert(_BClass.Getobject(Id));
            return Object;
        }









    }
}