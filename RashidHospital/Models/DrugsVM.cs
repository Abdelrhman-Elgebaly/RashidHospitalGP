
using RashidHospital.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Hospital.DAL;
using System.Web.Mvc;

namespace RashidHospital.Models
{
    public class DrugsVM : BusinessBaseClass<Drugs, DrugsVM>
    {
        public int Id { get; set; }

        public string Drugname { get; set; }
        public string Tradename { get; set; }

        internal override Drugs Convert(DrugsVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new Drugs
                {
                    Id = Obj.Id,
                    Drugname = Obj.Drugname,
                    Tradename = Obj.Tradename,

                };
            }
            return _Obj;
        }

        internal override DrugsVM Convert(Drugs DbObj)
        {
            return new DrugsVM()
            {
                Id = DbObj.Id,
                Drugname = DbObj.Drugname,
                Tradename = DbObj.Tradename,

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
        public List<DrugsVM> SelectAll()
        {
            DrugsVM _Obj = new DrugsVM();
            Drugs _BClass = new Drugs();
            List<Drugs> dbList = _BClass.GetAll<Drugs>().ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }

        public DrugsVM SelectObject(int Id)
        {
            Drugs _BClass = new Drugs();

            DrugsVM ClinicObject = Convert(_BClass.Getobject(Id));
            return ClinicObject;
        }

        public List<SelectListItem> DrugsSelectList()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            List<DrugsVM> CliniList = SelectAll();

            foreach (DrugsVM Obj in CliniList)
            {
                SelectListItem _item = new SelectListItem();
                _item.Text = Obj.Drugname;
                _item.Value = Obj.Id.ToString();
                listItems.Add(_item);
            }

            return listItems;
        }
        #endregion
    }
}