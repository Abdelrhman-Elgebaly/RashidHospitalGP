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

        public class ChemoTherapyCyclePackageVM : BusinessBaseClass<ChemoTherapyCyclePackage, ChemoTherapyCyclePackageVM>
        {

        [Key]
        public int ID { get; set; }
        public Nullable<int> Cycle_ID { get; set; }
        public Nullable<int> Patient_ID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> Test_Value { get; set; }
        public string Note { get; set; }
        public Nullable<bool> IsApproved { get; set; }

        public int Test_Type { get; set; }
        public Nullable<int> Actual_Value { get; set; }

        public int Rule_Type { get; set; }
        public string Test_TypeValue { get; set; }

        public string Rule_TypeValue { get; set; }
        public int TemplateId { get; set; }
        public Nullable<int> PreProtocol { get; set; }
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
                    Test_Type = Obj.Test_Type,
                    Test_Value = Obj.Test_Value,
                    Note = Obj.Note,
                    IsApproved=Obj.IsApproved,
                    Rule_Type= Obj.Rule_Type,
                    Actual_Value =Obj.Actual_Value,
                    TemplateId = Obj.TemplateId,
                    PreProtocol=Obj.PreProtocol,
                };
            }
            return _Obj;
        }


        internal override ChemoTherapyCyclePackageVM Convert(ChemoTherapyCyclePackage DbObj)
        {
            ChemoTherapyCyclePackageVM pl = new ChemoTherapyCyclePackageVM();

            var Site = (LabTyps)DbObj?.Test_Type;
            var enumType = EnumHelper<LabTyps>.GetDisplayValue(Site);

            var Site2 = (Rule)DbObj?.Rule_Type;
            var enumType2 = EnumHelper<Rule>.GetDisplayValue(Site2);

            pl.ID = DbObj.ID;

            pl.Patient_ID = DbObj.Patient_ID;
            pl.Date = DbObj.Date;
            pl.Cycle_ID = DbObj.Cycle_ID;
            pl.Test_Type = DbObj.Test_Type;
            pl.Test_TypeValue = enumType.ToString();
            pl.Rule_Type = DbObj.Rule_Type;
            pl.Rule_TypeValue = enumType2.ToString();
            pl.Test_Value = DbObj.Test_Value;
            pl.Note = DbObj.Note;
            pl.IsApproved = DbObj.IsApproved;
            pl.Actual_Value = DbObj.Actual_Value;
            pl.TemplateId = DbObj.TemplateId;
            pl.PreProtocol = DbObj.PreProtocol;
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

        public List<ChemoTherapyCyclePackageVM> SelectAllByCycleID(int CycleID)
        {
            ChemoTherapyCyclePackageVM _Obj = new ChemoTherapyCyclePackageVM();
            ChemoTherapyCyclePackage _BClass = new ChemoTherapyCyclePackage();
            List<ChemoTherapyCyclePackage> dbList = _BClass.GetLabPackageByCycleID(CycleID).ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }
        public ChemoTherapyCyclePackageVM SelectObject(int Id)
        {
            ChemoTherapyCyclePackage _BClass = new ChemoTherapyCyclePackage();
            ChemoTherapyCyclePackageVM ClinicObject = Convert(_BClass.Getobject(Id));
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