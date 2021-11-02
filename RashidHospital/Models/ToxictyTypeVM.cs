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
    public class ToxictyTypeVM : BusinessBaseClass<ToxictyType, ToxictyTypeVM>
    {
        public int Id { get; set; }

      
        public string Name { get; set; }
        public string Grade1 { get; set; }
        public string Grade2 { get; set; }
        public string Grade3 { get; set; }
        public string Grade4 { get; set; }
        public string Grade5 { get; set; }

        internal override ToxictyType Convert(ToxictyTypeVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new ToxictyType
                {
                    Id = Obj.Id,
                    Name = Obj.Name,
                    Grade1 = Obj.Grade1,
                    Grade2 = Obj.Grade2,
                    Grade3 = Obj.Grade3,
                    Grade4 = Obj.Grade4,
                    Grade5 = Obj.Grade5
                };
            }
            return _Obj;
        }

        internal override ToxictyTypeVM Convert(ToxictyType Obj)
        {

            if (Obj == null)
                return null;
            else
            {
                ToxictyTypeVM _Obj = new ToxictyTypeVM
                {
                    Id = Obj.Id,
                    Name = Obj.Name,
                    Grade1 = Obj.Grade1,
                    Grade2 = Obj.Grade2,
                    Grade3 = Obj.Grade3,
                    Grade4 = Obj.Grade4,
                    Grade5 = Obj.Grade5
                };
                return _Obj;

            }
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
        public List<ToxictyTypeVM> SelectAll()
        {
            ToxictyType _BClass = new ToxictyType();
            List<ToxictyType> dbList = _BClass.GetAll<ToxictyType>().ToList();
            return dbList.Select(z => Convert(z)).ToList();
        }
        public ToxictyTypeVM GetById(int id) {

            ToxictyType _BClass = new ToxictyType();
            ToxictyType dbobject = _BClass.Getobject(id);
            return Convert(dbobject);
        }

        public  List<SelectListItem> GetSelectList()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            List<ToxictyTypeVM> _List = SelectAll().ToList();
            foreach (ToxictyTypeVM Obj in _List)
            {
                SelectListItem _item = new SelectListItem();
                _item.Text = Obj.Name;
                _item.Value = Obj.Id.ToString();
                listItems.Add(_item);
            }

            return listItems;
        }

    }
}