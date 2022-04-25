
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

    public class ChemoTherapyPreInvestigationsVM : BusinessBaseClass<ChemoTherapyPreInvestigations, ChemoTherapyPreInvestigationsVM>
    {



        [Key]
        public int ID { get; set; }
        public Nullable<int> Template_ID { get; set; }
        public string Days { get; set; }
        public string Test_Name { get; set; }
        public Nullable<int> Value { get; set; }
        public string rule { get; set; }
        public virtual ChemoTherapy_Template ChemoTherapy_Template { get; set; }



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


                };
            }
            return _Obj;
        }


        internal override ChemoTherapyPreInvestigationsVM Convert(ChemoTherapyPreInvestigations DbObj)
        {
            ChemoTherapyPreInvestigationsVM pl = new ChemoTherapyPreInvestigationsVM();
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

        public List<ChemoTherapyPreInvestigationsVM> SelectAllByTemplateID(int templateId)
        {
            ChemoTherapyPreInvestigationsVM _Obj = new ChemoTherapyPreInvestigationsVM();
            ChemoTherapyPreInvestigations _BClass = new ChemoTherapyPreInvestigations();
            List<ChemoTherapyPreInvestigations> dbList = _BClass.GetChemoTherapyPreLabByTemplateId(templateId).ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }





    }
}