
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
        public int Patient_ID { get; set; }
        public DateTime Date { get; set; }
        public int TemplateId { get; set; }
        public bool IsReleased { get; set; }
        public bool IsPending { get; set; }
        public bool IsApproved { get; set; }
        //
        public string PatientName { get; set; }
        public string Disease { get; set; }
        public string Protocol { get; set; }
        public string Note { get; set; }
        public bool IsStart { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> SelectedNnote { get; set; }
        //
        public string DrStatues { get; set; }
        public string Reason { get; set; }
        public Nullable<bool> IsRescuedeled { get; set; }
        public Nullable<int> CycleNumber { get; set; }
        //
        public Guid? DoctorId { get; set; }
        public Guid? PharmacistId { get; set; }
        public string DoctorName { get; set; }
        public string PharmacistName { get; set; }


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

                    IsReleased = Obj.IsReleased,
                    IsPending = Obj.IsPending,
                    IsApproved = Obj.IsApproved,
                    IsStart = Obj.IsStart,
                    IsDeleted = Obj.IsDeleted,
                    SelectedNnote = Obj.SelectedNnote,
                    DrStatues = Obj.DrStatues,
                    Note = Obj.Note,
                    Reason = Obj.Reason,
                    IsRescuedeled = Obj.IsRescuedeled,
                    CycleNumber = Obj.CycleNumber,
                    DoctorId = Obj.DoctorId,
                    PharmacistId = Obj.PharmacistId,

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

            pl.IsReleased = DbObj.IsReleased;
            pl.IsPending = DbObj.IsPending;
            pl.IsApproved = DbObj.IsApproved;
            pl.IsStart = DbObj.IsStart;
            pl.IsDeleted = DbObj.IsDeleted;
            pl.SelectedNnote = DbObj.SelectedNnote;
            pl.DrStatues = DbObj.DrStatues;
            pl.Note = DbObj.Note;
            pl.Reason = DbObj.Reason;
            pl.IsRescuedeled = DbObj.IsRescuedeled;
            pl.CycleNumber = DbObj.CycleNumber;
            pl.DoctorId = DbObj?.DoctorId;
            pl.PharmacistId = DbObj?.PharmacistId;
            if (DbObj.DoctorId != null)
            {
                AspNetUser user = new AspNetUser();
                AspNetUser _user = user.Getobject(DbObj.DoctorId);
                pl.DoctorName = _user?.FirstName + " " + _user?.SecondName + " " + _user?.ThirdName;
            }
            if (DbObj.PharmacistId != null)
            {
                AspNetUser user = new AspNetUser();
                AspNetUser _user = user.Getobject(DbObj.PharmacistId);
                pl.PharmacistName = _user?.FirstName + " " + _user?.SecondName + " " + _user?.ThirdName;
            }








            //
            PatientVM Obj = new PatientVM();
            
            PatientVM Objm = Obj.SelectObject(DbObj.Patient_ID);
            pl.PatientName = Objm.Name;

            ChemoTherapyProtocolVM _cObj = new ChemoTherapyProtocolVM();
            ChemoTherapyProtocolVM _cobjVM = _cObj.SelectObject(DbObj.TemplateId);
            pl.Disease = _cobjVM.DiseaseName;
            pl.Protocol = _cobjVM.ProtocolName;
         



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


        public List<ChemoTherapyCycleDayVM> SelectAllReleased()
        {
            ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
            ChemoTherapyCycleDay _BClass = new ChemoTherapyCycleDay();
            List<ChemoTherapyCycleDay> dbList = _BClass.GetAllReleasedCycleDays().ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }
        public List<ChemoTherapyCycleDayVM> SelectAllPending()
        {
            ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
            ChemoTherapyCycleDay _BClass = new ChemoTherapyCycleDay();
            List<ChemoTherapyCycleDay> dbList = _BClass.GetAllPendingCycleDays().ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }

        public List<ChemoTherapyCycleDayVM> SelectAllFinalApproved()
        {
            ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
            ChemoTherapyCycleDay _BClass = new ChemoTherapyCycleDay();
            List<ChemoTherapyCycleDay> dbList = _BClass.GetAllFinalApprovedCycleDays().ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }


        public ChemoTherapyCycleDayVM SelectObject(int Id)
        {
            ChemoTherapyCycleDay _BClass = new ChemoTherapyCycleDay();
            ChemoTherapyCycleDayVM ClinicObject = Convert(_BClass.Getobject(Id));
            return ClinicObject;
        }

        public List<ChemoTherapyCycleDayVM> SelectAllByTemplateId(int TemplateId)
        {
            ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
            ChemoTherapyCycleDay _BClass = new ChemoTherapyCycleDay();
            List<ChemoTherapyCycleDay> dbList = _BClass.GetCyclesByTemplateId(TemplateId).ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }


    }
}