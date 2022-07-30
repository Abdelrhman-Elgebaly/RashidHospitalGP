
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

    public class ChemoTherapyPreLabVM : BusinessBaseClass<ChemoTherapyPreLab, ChemoTherapyPreLabVM>
    {



        [Key]
        public int ID { get; set; }
        public Nullable<int> Template_ID { get; set; }
        public string Days { get; set; }
        public string Test_Name { get; set; }
        public Nullable<double> Value { get; set; }
        public string rule { get; set; }
        //   public virtual ChemoTherapy_Template ChemoTherapy_Template { get; set; }
        public int Test_Type { get; set; }
        public int Rule_Type { get; set; }
        public string Test_TypeValue { get; set; }

        public string Rule_TypeValue { get; set; }

        internal override ChemoTherapyPreLab Convert(ChemoTherapyPreLabVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new ChemoTherapyPreLab
                {
                    ID = Obj.ID,
                    Days = Obj.Days,
                    Test_Name = Obj.Test_Name,
                    Value = Obj.Value,
                    Template_ID = Obj.Template_ID,
                    rule = Obj.rule,

                    Test_Type = Obj.Test_Type,
                    Rule_Type = Obj.Rule_Type,
                };
            }
            return _Obj;
        }


        internal override ChemoTherapyPreLabVM Convert(ChemoTherapyPreLab DbObj)
        {
           

            ChemoTherapyPreLabVM pl = new ChemoTherapyPreLabVM();

            var Site = (LabTyps)DbObj?.Test_Type;
            var enumType = EnumHelper<LabTyps>.GetDisplayValue(Site);

            var Site2 = (Rule)DbObj?.Rule_Type;
            var enumType2 = EnumHelper<Rule>.GetDisplayValue(Site2);

            pl.ID = DbObj.ID;
            pl.Days = DbObj.Days;
            pl.rule = DbObj.rule;
            pl.Test_Name = DbObj.Test_Name;
            pl.Value = DbObj.Value;
            pl.Template_ID = DbObj.Template_ID;
            pl.Test_Type = DbObj.Test_Type;
            pl.Test_TypeValue = enumType.ToString();
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

        public List<ChemoTherapyPreLabVM> SelectAllByTemplateID(int templateId)
        {
            ChemoTherapyPreLabVM _Obj = new ChemoTherapyPreLabVM();
            ChemoTherapyPreLab _BClass = new ChemoTherapyPreLab();
            List<ChemoTherapyPreLab> dbList = _BClass.GetChemoTherapyPreLabByTemplateId(templateId).ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }



        public ChemoTherapyPreLabVM SelectObject(int Id)
        {
            ChemoTherapyPreLab _BClass = new ChemoTherapyPreLab();
            ChemoTherapyPreLabVM ClinicObject = Convert(_BClass.Getobject(Id));
            return ClinicObject;
        }



        public List<SelectListItem> GeLabTypsSelectList()
        {
            return System.Enum.GetValues(typeof(LabTyps)).Cast<LabTyps>().Select(v => new SelectListItem
            {
                Text = EnumHelper<LabTyps>.GetDisplayValue(v),
                Value = ((int)v).ToString()
            }).ToList();
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