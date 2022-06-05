
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

    public class CycleStartDateVM : BusinessBaseClass<CycleStartDate, CycleStartDateVM>
    {



        [Key]
        public int ID { get; set; }

        public Nullable<int> Patient_ID { get; set; }
        public DateTime Date { get; set; }
        //   public virtual ChemoTherapy_Template ChemoTherapy_Template { get; set; }



        internal override CycleStartDate Convert(CycleStartDateVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new CycleStartDate
                {
                    ID = Obj.ID,
                    Patient_ID = Obj.Patient_ID,
                    Date = Obj.Date,
              


                };
            }
            return _Obj;
        }


        internal override CycleStartDateVM Convert(CycleStartDate DbObj)
        {
            CycleStartDateVM pl = new CycleStartDateVM();
            pl.ID = DbObj.ID;

            pl.Patient_ID = DbObj.Patient_ID;
            pl.Date = DbObj.Date;


            return pl;
        }



        public void Create()
        {
            _Obj = Convert(this);
            _Obj.AddNew();
        }

   



      
    }
     }