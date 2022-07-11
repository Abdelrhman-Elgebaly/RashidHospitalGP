
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

    public class LabPackageVM : BusinessBaseClass<LabPackage, LabPackageVM>
    {

        [Key]
        public int ID { get; set; }
        public Nullable<int> Package_ID { get; set; }
        public Nullable<int> Test { get; set; }
        public Nullable<int> Value { get; set; }
        public Nullable<int> Rule { get; set; }
        public string Test_TypeValue { get; set; }
        public string Rule_TypeValue { get; set; }

        internal override LabPackage Convert(LabPackageVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new LabPackage
                {
                    ID = Obj.ID,
                    Package_ID = Obj.Package_ID,
                    Test = Obj.Test,
                    Value = Obj.Value,
                    Rule = Obj.Rule,
                   
                };
            }
            return _Obj;
        }


        internal override LabPackageVM Convert(LabPackage DbObj)
        {
            LabPackageVM pl = new LabPackageVM();

            var Site = (LabTyps)DbObj?.Test;
            var enumType = EnumHelper<LabTyps>.GetDisplayValue(Site);

            var Site2 = (Rule)DbObj?.Rule;
            var enumType2 = EnumHelper<Rule>.GetDisplayValue(Site2);

            pl.ID = DbObj.ID;

          
            pl.Package_ID = DbObj.Package_ID;
            pl.Test = DbObj.Test;
            pl.Value = DbObj.Value;
            pl.Test_TypeValue = enumType.ToString();
            pl.Rule = DbObj.Rule;
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

        public List<LabPackageVM> SelectAllByPackageID(int PackageId)
        {
            LabPackageVM _Obj = new LabPackageVM();
            LabPackage _BClass = new LabPackage();
            List<LabPackage> dbList = _BClass.GetLabPackagesByPackageId(PackageId).ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }
        public LabPackageVM SelectObject(int Id)
        {
            LabPackage _BClass = new LabPackage();
            LabPackageVM ClinicObject = Convert(_BClass.Getobject(Id));
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