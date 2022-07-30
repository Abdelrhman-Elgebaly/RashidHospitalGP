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
using System.Web.Mvc;
using static RashidHospital.Helper.Enum;
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
        public double Drug_Dose { get; set; }
        public string Fluid_Type_Value { get; set; }
        public string Fluid_Vol { get; set; }
        public string Unit_Value { get; set; }
        public string Route_Value { get; set; }
        public string Duration { get; set; }
        public string Note { get; set; }
        public int Unit { get; set; }
        public int Route { get; set; }
        public int Fluid_Type { get; set; }
        public int DrugType { get; set; }



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
                    DrugType = Obj.DrugType,

                };
            }
            return _Obj;
        }
    
     

        internal override ChemoTherapyDrugVM Convert(ChemoTherapyDrug Obj)
        {
            DrugsVM clinicVm = new DrugsVM();
            DrugsVM clinicObj = clinicVm.SelectObject(Obj.DrugType);
            ChemoTherapyDrugVM pl = new ChemoTherapyDrugVM();
            var Site = (Unit)Obj?.Unit;
                var enumType = EnumHelper<Unit>.GetDisplayValue(Site);

                var Site1 = (Route)Obj?.Route;
                var enumType1 = EnumHelper<Route>.GetDisplayValue(Site1);

                var Site2 = (Fluid_Type)Obj?.Fluid_Type;
                var enumType2 = EnumHelper<Fluid_Type>.GetDisplayValue(Site2);
       



            pl.Drug_ID = Obj.Drug_ID;
            pl.Template_ID = Obj.Template_ID;
            pl.Therapy_Type = Obj.Therapy_Type;
            pl.Days = Obj.Days;
            pl.Sequence_Number = Obj.Sequence_Number;
            pl.DrugType = Obj.DrugType;
            pl.Drug_Dose = Obj.Drug_Dose;
            pl.Fluid_Type = Obj.Fluid_Type;
            pl.Fluid_Vol = Obj.Fluid_Vol;
            pl.Unit = Obj.Unit;
            pl.Route = Obj.Route;
            pl.Duration = Obj.Duration;
            pl.Note = Obj.Note;
            pl.Unit_Value = enumType.ToString();
            pl.Route_Value = enumType1.ToString();
            pl.Fluid_Type_Value = enumType2.ToString();
            pl.Drug_Name = clinicObj.Drugname;
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




        public List<SelectListItem> GetUnitSelectList()
        {
            return System.Enum.GetValues(typeof(Unit)).Cast<Unit>().Select(v => new SelectListItem
            {
                Text = EnumHelper<Unit>.GetDisplayValue(v),
                Value = ((int)v).ToString()
            }).ToList();
        }

        public List<SelectListItem> GetRouteSelectList()
        {
            return System.Enum.GetValues(typeof(Route)).Cast<Route>().Select(v => new SelectListItem
            {
                Text = EnumHelper<Route>.GetDisplayValue(v),
                Value = ((int)v).ToString()
            }).ToList();
        }

         public List<SelectListItem> GetFluidTSelectList()
        {
            return System.Enum.GetValues(typeof(Fluid_Type)).Cast<Fluid_Type>().Select(v => new SelectListItem
            {
                Text = EnumHelper<Fluid_Type>.GetDisplayValue(v),
                Value = ((int)v).ToString()
            }).ToList();
        }




    }
}