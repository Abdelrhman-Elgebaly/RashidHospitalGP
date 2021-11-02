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
    public class FixationVM : BusinessBaseClass<Fixation, FixationVM>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        internal override FixationVM Convert(Fixation Obj)
        {
            FixationVM _obj = new FixationVM();
            _obj.Id = Obj.Id;
            _obj.Name = Obj.Name;

            return _obj;
        }

        internal override Fixation Convert(FixationVM Obj)
        {
            Fixation _obj = new Fixation();
            _obj.Id = Obj.Id;
            _obj.Name = Obj.Name;
            
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
        public List<FixationVM> SelectAll()
        {
            FixationVM _Obj = new FixationVM();
            Fixation _BClass = new Fixation();
            List<Fixation> dbList = _BClass.GetAll<Fixation>().ToList();
            return dbList != null ? dbList.Select(z => _Obj.Convert(z)).ToList() : null;
        }


        public FixationVM SelectObject(int Id)
        {
            Fixation _BClass = new Fixation();

            FixationVM _Object = Convert(_BClass.Getobject(Id));
            return _Object;
        }
        public List<SelectListItem> GetFixtionSelectList()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            List<FixationVM> _List = SelectAll();
            foreach (FixationVM Obj in _List)
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