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
    public class TumerHistologyTypesVM : BusinessBaseClass<TumerHistologyTypes, TumerHistologyTypesVM>
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public bool IsDeleted { get; set; }

        internal override TumerHistologyTypes Convert(TumerHistologyTypesVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new TumerHistologyTypes
                {
                    Id = Obj.Id,
                    Title = Obj.Title,
                    IsDeleted = Obj.IsDeleted
                };
            }
            return _Obj;
        }

        internal override TumerHistologyTypesVM Convert(TumerHistologyTypes DbObj)
        {
            return new TumerHistologyTypesVM()
            {
                Id = DbObj.Id,
                Title = DbObj.Title,
                IsDeleted = DbObj.IsDeleted

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
        public List<TumerHistologyTypesVM> SelectAll()
        {
            TumerHistologyTypesVM _Obj = new TumerHistologyTypesVM();
            TumerHistologyTypes _BClass = new TumerHistologyTypes();
            List<TumerHistologyTypes> dbList = _BClass.GetAll<TumerHistologyTypes>().ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }

        public TumerHistologyTypesVM SelectObject(int Id)
        {
            TumerHistologyTypes _BClass = new TumerHistologyTypes();

            TumerHistologyTypesVM Object = Convert(_BClass.Getobject(Id));
            return Object;
        }

        public List<SelectListItem> GetSelectList()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            List<TumerHistologyTypesVM> _List = SelectAll().Where(a => a.IsDeleted == false).ToList();
            foreach (TumerHistologyTypesVM Obj in _List)
            {
                SelectListItem _item = new SelectListItem();
                _item.Text = Obj.Title;
                _item.Value = Obj.Id.ToString();
                listItems.Add(_item);
            }

            return listItems;
        }

        #endregion
    }
}