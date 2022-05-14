
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

    public class ChemoTherapyCycleDaysVM : BusinessBaseClass<ChemoTherapyCycleDays, ChemoTherapyCycleDaysVM>
    {



        [Key]
        public int ID { get; set; }
        public Nullable<int> Template_ID { get; set; }
        public Nullable <int> Day { get; set; }

        //    public virtual ChemoTherapy_Template ChemoTherapy_Template { get; set; }



        internal override ChemoTherapyCycleDays Convert(ChemoTherapyCycleDaysVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new ChemoTherapyCycleDays
                {
                    ID = Obj.ID,
                    Day = Obj.Day,
                    Template_ID = Obj.Template_ID,
                  


                };
            }
            return _Obj;
        }


        internal override ChemoTherapyCycleDaysVM Convert(ChemoTherapyCycleDays DbObj)
        {
            ChemoTherapyCycleDaysVM pl = new ChemoTherapyCycleDaysVM();
            pl.ID = DbObj.ID;
            pl.Day = DbObj.Day;
        
            pl.Template_ID = DbObj.Template_ID;



            return pl;
        }



        public void Create()
        {
            _Obj = Convert(this);
            _Obj.AddNew();
        }

        public List<ChemoTherapyCycleDaysVM> SelectAllByTemplateID(int templateId)
        {
            ChemoTherapyCycleDaysVM _Obj = new ChemoTherapyCycleDaysVM();
            ChemoTherapyCycleDays _BClass = new ChemoTherapyCycleDays();
            List<ChemoTherapyCycleDays> dbList = _BClass.GetChemoTherapyCycleDaysByTemplateId(templateId).ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }





    }
}