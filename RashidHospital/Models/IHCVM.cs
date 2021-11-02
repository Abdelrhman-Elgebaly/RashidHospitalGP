using Hospital.DAL;
using RashidHospital.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static RashidHospital.Helper.Enum;

namespace RashidHospital.Models
{
    public class IHCVM : BusinessBaseClass<IHC, IHCVM>
    {
        public int Id { get; set; }

        public int Type { get; set; }
        public string TypeName { get; set; }

        public string Result { get; set; }
        public bool IsDeleted { get; set; }


        [StringLength(50)]
        public string Value { get; set; }

        public DateTime ResultDate { get; set; }
        public int PathologyId { get; set; }

        public int PatientId { get; set; }
        public string PatientName { get; set; }
        internal override IHCVM Convert(IHC Obj)
        {
            IHCVM _obj = new IHCVM();
            _obj.Id = Obj.Id;
            _obj.PatientId = Obj.PatientId;
            _obj.PatientName = Obj.Patient?.Name;
            _obj.Result = Obj.Result;
            _obj.ResultDate = Obj.ResultDate;
            _obj.Type = Obj.Type;
            _obj.Value = Obj.Value;
            _obj.PathologyId = Obj.PathologyId;
            IHCTypesVM _type = new IHCTypesVM();
            _type= _type.SelectObject(Obj.Type);
            _obj.TypeName = _type.Title;
            _obj.IsDeleted = Obj.IsDeleted;

            return _obj;
        }

        internal override IHC Convert(IHCVM Obj)
        {
            IHC _obj = new IHC();
            _obj.Id = Obj.Id;
            _obj.PatientId = Obj.PatientId;
            _obj.Result = Obj.Result;
            _obj.ResultDate = Obj.ResultDate;
            _obj.Type = Obj.Type;
            _obj.Value = Obj.Value;
            _obj.PathologyId = Obj.PathologyId;
            _obj.IsDeleted = Obj.IsDeleted;
            return _obj;
        }

        #region Functions
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
        public List<IHCVM> SelectAllByPathologyId(int PathologyId)
        {
            IHCVM _Obj = new IHCVM();
            IHC _BClass = new IHC();
            List<IHC> dbList = _BClass.GetIHCByPathologyId(PathologyId).ToList();
            return dbList!= null? dbList.Select(z => _Obj.Convert(z)).ToList(): null;
        }

        
        public IHCVM SelectObject(int Id)
        {
            IHC _BClass = new IHC();

            IHCVM _Object = Convert(_BClass.Getobject(Id));
            return _Object;
        }
        public List<SelectListItem> GetIHCTypeSelectList()
        {
            IHCTypesVM _ihcType = new IHCTypesVM();
          return  _ihcType.GetSelectList();
        }
        #endregion
    }
}