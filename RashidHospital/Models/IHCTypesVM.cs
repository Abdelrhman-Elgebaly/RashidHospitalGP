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
    public class IHCTypesVM : BusinessBaseClass<IHCType, IHCTypesVM>
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public bool IsDeleted { get; set; }

        internal override IHCType Convert(IHCTypesVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new IHCType
                {
                    Id = Obj.Id,
                    Title=Obj.Title,
                    IsDeleted=Obj.IsDeleted
                };
            }
            return _Obj;
        }

        internal override IHCTypesVM Convert(IHCType DbObj)
        {
            
            return new IHCTypesVM()
            {
                Id = DbObj.Id,
                Title=DbObj.Title ,
                IsDeleted=DbObj.IsDeleted

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
        public List<IHCTypesVM> SelectAll()
        {
            IHCTypesVM _Obj = new IHCTypesVM();
            IHCType _BClass = new IHCType();
            List<IHCType> dbList = _BClass.GetAll<IHCType>().ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }

        public IHCTypesVM SelectObject(int Id)
        {
            IHCType _BClass = new IHCType();

            IHCTypesVM Object = Convert(_BClass.Getobject(Id));
            return Object;
        }

        public List<SelectListItem> GetSelectList()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            List<IHCTypesVM> _List = SelectAll().Where(a => a.IsDeleted == false).ToList();
            foreach (IHCTypesVM Obj in _List)
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