


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
    public class DrugOrderVM : BusinessBaseClass<DrugOrder, DrugOrderVM>
    {
        public int ID { get; set; }
        public string Name { get; set; }


        internal override DrugOrder Convert(DrugOrderVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new DrugOrder
                {
                    ID = Obj.ID,
                  Name = Obj.Name,

                };
            }
            return _Obj;
        }

        internal override DrugOrderVM Convert(DrugOrder DbObj)
        {
            return new DrugOrderVM()
            {
                ID = DbObj.ID,
                Name = DbObj.Name,

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
        public List<DrugOrderVM> SelectAll()
        {
            DrugOrderVM _Obj = new DrugOrderVM();
            DrugOrder _BClass = new DrugOrder();
            List<DrugOrder> dbList = _BClass.GetAll<DrugOrder>().ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }

        public DrugOrderVM SelectObject(int Id)
        {
            DrugOrder _BClass = new DrugOrder();

            DrugOrderVM ClinicObject = Convert(_BClass.Getobject(Id));
            return ClinicObject;
        }

        public List<SelectListItem> OrderSelectList()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            List<DrugOrderVM> CliniList = SelectAll();

            foreach (DrugOrderVM Obj in CliniList)
            {
                SelectListItem _item = new SelectListItem();
                _item.Text = Obj.Name;
                _item.Value = Obj.ID.ToString();
                listItems.Add(_item);
            }

            return listItems;
        }
        #endregion
    }
}