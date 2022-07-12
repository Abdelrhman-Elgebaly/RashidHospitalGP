
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
    public class DiseaseVM : BusinessBaseClass<Disease, DiseaseVM>
    {
        public int DiseaseId { get; set; }
        public string DiseaseName { get; set; }


        internal override Disease Convert(DiseaseVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new Disease
                {
                    DiseaseId = Obj.DiseaseId,
                    DiseaseName = Obj.DiseaseName,
                   
                };
            }
            return _Obj;
        }

        internal override DiseaseVM Convert(Disease DbObj)
        {
            return new DiseaseVM()
            {
                DiseaseId = DbObj.DiseaseId,
                DiseaseName = DbObj.DiseaseName,

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
        public List<DiseaseVM> SelectAll()
        {
            DiseaseVM _Obj = new DiseaseVM();
            Disease _BClass = new Disease();
            List<Disease> dbList = _BClass.GetAll<Disease>().ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }

        public DiseaseVM SelectObject(int Id)
        {
            Disease _BClass = new Disease();

            DiseaseVM ClinicObject = Convert(_BClass.Getobject(Id));
            return ClinicObject;
        }

        public List<SelectListItem> DiseaseSelectList()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            List<DiseaseVM> CliniList = SelectAll();

            foreach (DiseaseVM Obj in CliniList)
            {
                SelectListItem _item = new SelectListItem();
                _item.Text = Obj.DiseaseName;
                _item.Value = Obj.DiseaseId.ToString();
                listItems.Add(_item);
            }

            return listItems;
        }
        #endregion
    }
}