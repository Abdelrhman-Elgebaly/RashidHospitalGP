
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

    public class ChemoTherapyCyclesDatesVM : BusinessBaseClass<ChemoTherapyCyclesDates, ChemoTherapyCyclesDatesVM>
    {



        [Key]
        public int ID { get; set; }

        public Nullable<int> Patient_ID { get; set; }
        public int TemplateId { get; set; }
        public DateTime Date { get; set; }
        //   public virtual ChemoTherapy_Template ChemoTherapy_Template { get; set; }
        public int Cycles_Number { get; set; }
        public List<DateTime> cycleDates { get; set; }

        internal override ChemoTherapyCyclesDates Convert(ChemoTherapyCyclesDatesVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new ChemoTherapyCyclesDates
                {
                    ID = Obj.ID,
                    Patient_ID = Obj.Patient_ID,
                    Date = Obj.Date,
                    Cycles_Number = Obj.Cycles_Number,
                    TemplateId = Obj.TemplateId,

                };
            }
            return _Obj;
        }


        internal override ChemoTherapyCyclesDatesVM Convert(ChemoTherapyCyclesDates DbObj)
        {
            ChemoTherapyCyclesDatesVM pl = new ChemoTherapyCyclesDatesVM();
            pl.ID = DbObj.ID;

            pl.Patient_ID = DbObj.Patient_ID;
            pl.Date = DbObj.Date;
            pl.Cycles_Number = DbObj.Cycles_Number;
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

        public List<ChemoTherapyCyclesDatesVM> SelectAllByTemplateId(int TemplateId)
        {
            ChemoTherapyCyclesDatesVM _Obj = new ChemoTherapyCyclesDatesVM();
            ChemoTherapyCyclesDates _BClass = new ChemoTherapyCyclesDates();
            List<ChemoTherapyCyclesDates> dbList = _BClass.GetCyclesByTemplateId(TemplateId).ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }
        public ChemoTherapyCyclesDatesVM SelectObject(int Id)
        {
            ChemoTherapyCyclesDates _BClass = new ChemoTherapyCyclesDates();
            ChemoTherapyCyclesDatesVM ClinicObject = Convert(_BClass.Getobject(Id));
            return ClinicObject;
        }




    }
     }