
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

    public class ChemoTherapyCycleDayVM : BusinessBaseClass<ChemoTherapyCycleDay, ChemoTherapyCycleDayVM>
    {



        [Key]
        public int ID { get; set; }


        public Nullable<int> MainCycle_ID { get; set; }
        public Nullable<int> Patient_ID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public int TemplateId { get; set; }

        internal override ChemoTherapyCycleDay Convert(ChemoTherapyCycleDayVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new ChemoTherapyCycleDay
                {
                    ID = Obj.ID,
                    Patient_ID = Obj.Patient_ID,
                    Date = Obj.Date,
                    MainCycle_ID = Obj.MainCycle_ID,
                 TemplateId = Obj.TemplateId,


                };
            }
            return _Obj;
        }


        internal override ChemoTherapyCycleDayVM Convert(ChemoTherapyCycleDay DbObj)
        {
            ChemoTherapyCycleDayVM pl = new ChemoTherapyCycleDayVM();
            pl.ID = DbObj.ID;

            pl.Patient_ID = DbObj.Patient_ID;

            pl.Date = DbObj.Date;

            pl.MainCycle_ID = DbObj.MainCycle_ID;
            pl.TemplateId = DbObj.TemplateId;

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

        public List<ChemoTherapyCycleDayVM> SelectAllByMainCycleId(int MainCycleId)
        {
            ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
            ChemoTherapyCycleDay _BClass = new ChemoTherapyCycleDay();
            List<ChemoTherapyCycleDay> dbList = _BClass.GetCycleDaysByMainId(MainCycleId).ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }
        public ChemoTherapyCycleDayVM SelectObject(int Id)
        {
            ChemoTherapyCycleDay _BClass = new ChemoTherapyCycleDay();
            ChemoTherapyCycleDayVM ClinicObject = Convert(_BClass.Getobject(Id));
            return ClinicObject;
        }




    }
}