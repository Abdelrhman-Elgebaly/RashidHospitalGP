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
    public class ClinicVM : BusinessBaseClass<Clinic, ClinicVM>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Details { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Number")]
        public int visitsPerDay { get; set; }
        internal override Clinic Convert(ClinicVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new Clinic
                {
                    Id = Obj.Id,
                    Name = Obj.Name,
                     Details = Obj.Details,
                     IsDeleted=Obj.IsDeleted,
                     visitsPerDay=Obj.visitsPerDay,
                };
            }
            return _Obj;
        }

        internal override ClinicVM Convert(Clinic DbObj)
        {
            return new ClinicVM()
            {
                Id = DbObj.Id,
                Name = DbObj.Name,
                 Details = DbObj.Details,
                 IsDeleted=DbObj.IsDeleted,
                 visitsPerDay=DbObj.visitsPerDay

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
        public List<ClinicVM> SelectAll()
        {
            ClinicVM _Obj = new ClinicVM();
            Clinic _BClass = new Clinic();
            List<Clinic> dbList = _BClass.GetAll<Clinic>().ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }

        public ClinicVM SelectObject(int Id)
        {
            Clinic _BClass = new Clinic();

            ClinicVM ClinicObject = Convert(_BClass.Getobject(Id));
            return ClinicObject;
        }

        public List<SelectListItem> ClinicSelectList()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            List<ClinicVM> CliniList = SelectAll();
           
            foreach (ClinicVM Obj in CliniList)
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