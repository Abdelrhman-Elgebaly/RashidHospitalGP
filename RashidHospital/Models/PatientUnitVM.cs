using Hospital.DAL;
using RashidHospital.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RashidHospital.Models
{
    public class PatientUnitVM: BusinessBaseClass<PatientUnit,PatientUnitVM>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        internal override PatientUnit Convert(PatientUnitVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new PatientUnit
                {
                    Id = Obj.Id,
                    Name = Obj.Name,
                    IsDeleted = Obj.IsDeleted,
                };
            }
            return _Obj;
        }

        internal override PatientUnitVM Convert(PatientUnit DbObj)
        {
            return new PatientUnitVM()
            {
                Id = DbObj.Id,
                Name = DbObj.Name,
              IsDeleted=DbObj.IsDeleted,

            };
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
        public List<PatientUnitVM> SelectAll()
        {
            PatientUnitVM _Obj = new PatientUnitVM();
            PatientUnit _BClass = new PatientUnit();
            List<PatientUnit> dbList = _BClass.GetAll<PatientUnit>().ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }

        public PatientUnitVM SelectObject(int Id)
        {
            PatientUnit _BClass = new PatientUnit();

            PatientUnitVM Object = Convert(_BClass.Getobject(Id));
            return Object;
        }

        public List<SelectListItem> GetSelectList()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            List<PatientUnitVM> _List = SelectAll();
          
            foreach (PatientUnitVM Obj in _List)
            {
                SelectListItem _item = new SelectListItem();
                _item.Text = Obj.Name;
                _item.Value = Obj.Id.ToString();
                listItems.Add(_item);
            }

            return listItems;
        }
        #endregion
    }
}