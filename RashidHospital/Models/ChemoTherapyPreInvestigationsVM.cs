
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

    public class ChemoTherapyPreInvestigationsVM : BusinessBaseClass<ChemoTherapyPreInvestigations, ChemoTherapyPreInvestigationsVM>
    {



        [Key]
        public int ID { get; set; }
        public Nullable<int> Template_ID { get; set; }
        public string Days { get; set; }
        public string Test_Name { get; set; }
      
        public string rule { get; set; }
        //public virtual ChemoTherapy_Template ChemoTherapy_Template { get; set; }
        public int Rule_Type { get; set; } = 1;
        public string Rule_TypeValue { get; set; }
        public double Value { get; set; }

        internal override ChemoTherapyPreInvestigations Convert(ChemoTherapyPreInvestigationsVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new ChemoTherapyPreInvestigations
                {
                    ID = Obj.ID,
                    Days = Obj.Days,
                    Test_Name = Obj.Test_Name,
                    Value = Obj.Value,
                    Template_ID = Obj.Template_ID,
                    Rule_Type = Obj.Rule_Type,

                };
            }
            return _Obj;
        }


        internal override ChemoTherapyPreInvestigationsVM Convert(ChemoTherapyPreInvestigations DbObj)
        {
            ChemoTherapyPreInvestigationsVM pl = new ChemoTherapyPreInvestigationsVM();

            var Site2 = (Rule)DbObj?.Rule_Type;
            var enumType2 = EnumHelper<Rule>.GetDisplayValue(Site2);


            pl.ID = DbObj.ID;
            pl.Days = DbObj.Days;
            pl.rule = DbObj.rule;
            pl.Test_Name = DbObj.Test_Name;
            pl.Value = DbObj.Value;
            pl.Template_ID = DbObj.Template_ID;
            pl.Rule_Type = DbObj.Rule_Type;
            pl.Rule_TypeValue = enumType2.ToString();

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
        public List<ChemoTherapyPreInvestigationsVM> SelectAllByTemplateID(int templateId)
        {
            ChemoTherapyPreInvestigationsVM _Obj = new ChemoTherapyPreInvestigationsVM();
            ChemoTherapyPreInvestigations _BClass = new ChemoTherapyPreInvestigations();
            List<ChemoTherapyPreInvestigations> dbList = _BClass.GetChemoTherapyPreLabByTemplateId(templateId).ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }


        public ChemoTherapyPreInvestigationsVM SelectObject(int Id)
        {
            ChemoTherapyPreInvestigations _BClass = new ChemoTherapyPreInvestigations();
            ChemoTherapyPreInvestigationsVM ClinicObject = Convert(_BClass.Getobject(Id));
            return ClinicObject;
        }

        public List<SelectListItem> GetRuleTypsSelectList()
        {
            return System.Enum.GetValues(typeof(Rule)).Cast<Rule>().Select(v => new SelectListItem
            {
                Text = EnumHelper<Rule>.GetDisplayValue(v),
                Value = ((int)v).ToString()
            }).ToList();
        }

    }
}