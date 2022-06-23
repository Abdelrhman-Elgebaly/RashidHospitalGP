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

        public class ChemoTherapyCyclePackageVM : BusinessBaseClass<ChemoTherapyCyclePackage, ChemoTherapyCyclePackageVM>
        {

        [Key]
        public int ID { get; set; }
        public Nullable<int> Cycle_ID { get; set; }
        public Nullable<int> Patient_ID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Test_Name { get; set; }
        public Nullable<int> Test_Value { get; set; }
        public string Note { get; set; }
        public bool IsApproved { get; set; }




        internal override ChemoTherapyCyclePackage Convert(ChemoTherapyCyclePackageVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new ChemoTherapyCyclePackage
                {
                    ID = Obj.ID,
                    Patient_ID = Obj.Patient_ID,
                    Date = Obj.Date,
                    Cycle_ID=Obj.Cycle_ID,
                    Test_Name = Obj.Test_Name,
                    Test_Value = Obj.Test_Value,
                    Note = Obj.Note,
                    IsApproved=Obj.IsApproved,

                };
            }
            return _Obj;
        }


        internal override ChemoTherapyCyclePackageVM Convert(ChemoTherapyCyclePackage DbObj)
        {
            ChemoTherapyCyclePackageVM pl = new ChemoTherapyCyclePackageVM();
            pl.ID = DbObj.ID;

            pl.Patient_ID = DbObj.Patient_ID;
            pl.Date = DbObj.Date;
            pl.Cycle_ID = DbObj.Cycle_ID;
            pl.Test_Name = DbObj.Test_Name;
            pl.Test_Value = DbObj.Test_Value;
            pl.Note = DbObj.Note;
            pl.IsApproved = DbObj.IsApproved;

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

        public List<ChemoTherapyCyclePackageVM> SelectAllByPatientId(int PatientId)
        {
            ChemoTherapyCyclePackageVM _Obj = new ChemoTherapyCyclePackageVM();
            ChemoTherapyCyclePackage _BClass = new ChemoTherapyCyclePackage();
            List<ChemoTherapyCyclePackage> dbList = _BClass.GetLabPackageByPatientId(PatientId).ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }
        public ChemoTherapyCyclePackageVM SelectObject(int Id)
        {
            ChemoTherapyCyclePackage _BClass = new ChemoTherapyCyclePackage();
            ChemoTherapyCyclePackageVM ClinicObject = Convert(_BClass.Getobject(Id));
            return ClinicObject;
        }





















    }
    }