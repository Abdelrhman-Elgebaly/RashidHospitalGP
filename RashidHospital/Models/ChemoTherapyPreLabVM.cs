
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

    public class ChemoTherapyPreLabVM : BusinessBaseClass<ChemoTherapyPreLab, ChemoTherapyPreLabVM>
    {



        [Key]
        public int ID { get; set; }
        public Nullable<int> Template_ID { get; set; }
        public string Days { get; set; }
        public string Test_Name { get; set; }
        public Nullable<int> Value { get; set; }
        public string rule { get; set; }
        public virtual ChemoTherapy_Template ChemoTherapy_Template { get; set; }



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


                };
            }
            return _Obj;
        }


        internal override ChemoTherapyPreLabVM Convert(ChemoTherapyPreLab DbObj)
        {
            ChemoTherapyPreLabVM pl = new ChemoTherapyPreLabVM();
            pl.ID = DbObj.ID;
            pl.Days = DbObj.Days;
            pl.rule = DbObj.rule;
            pl.Test_Name = DbObj.Test_Name;
            pl.Value = DbObj.Value;
            pl.Template_ID = DbObj.Template_ID;



            return pl;
        }



        public void Create()
        {
            _Obj = Convert(this);
            _Obj.AddNew();
        }

        public List<ChemoTherapyPreLabVM> SelectAllByTemplateID(int templateId)
        {
            ChemoTherapyPreLabVM _Obj = new ChemoTherapyPreLabVM();
            ChemoTherapyPreLab _BClass = new ChemoTherapyPreLab();
            List<ChemoTherapyPreLab> dbList = _BClass.GetChemoTherapyPreLabByTemplateId(templateId).ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }





    }
}