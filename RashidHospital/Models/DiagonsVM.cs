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
    public class DiagonsVM : BusinessBaseClass<Diagons, DiagonsVM>
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public bool IsDeleted { get; set; }

        internal override Diagons Convert(DiagonsVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new Diagons
                {
                    Id = Obj.Id,
                    Title=Obj.Title,
                    IsDeleted=Obj.IsDeleted
                };
            }
            return _Obj;
        }

        internal override DiagonsVM Convert(Diagons DbObj)
        {
            return new DiagonsVM()
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
        public List<DiagonsVM> SelectAll()
        {
            DiagonsVM _Obj = new DiagonsVM();
            Diagons _BClass = new Diagons();
            List<Diagons> dbList = _BClass.GetAll<Diagons>().ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }

        public DiagonsVM SelectObject(int Id)
        {
            Diagons _BClass = new Diagons();

            DiagonsVM Object = Convert(_BClass.Getobject(Id));
            return Object;
        }

        public List<SelectListItem> GetSelectList()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            List<DiagonsVM> _List = SelectAll();
            foreach (DiagonsVM Obj in _List)
            {
                SelectListItem _item = new SelectListItem();
                _item.Text = Obj.Title;
                _item.Value = Obj.Id.ToString();
                listItems.Add(_item);
            }

            return listItems;
        }

        public  int GetDefault()
        {
            Diagons _Diagons = new Diagons();
            var _obj = _Diagons.GetDefault();
            return _obj != null? _obj.Id:0;
        }
        
        #endregion
    }
}