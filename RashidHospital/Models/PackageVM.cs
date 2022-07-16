using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hospital.DAL;
using RashidHospital.Helper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using static RashidHospital.Helper.Enum;
namespace RashidHospital.Models
{

    public class PackageVM : BusinessBaseClass<Package, PackageVM>

    {

        [Key]
        public int ID { get; set; }
        public string Name { get; set; }


        internal override Package Convert(PackageVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new Package
                {
                   ID = Obj.ID,
                  Name = Obj.Name,
                   



                };
            }
            return _Obj;
        }



        internal override PackageVM Convert(Package Obj)
        {
          


            return new PackageVM
            {


                ID = Obj.ID,
                Name = Obj.Name,
            };
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

        public List<PackageVM> SelectAll()
        {
            PackageVM _Obj = new PackageVM();
            Package _BClass = new Package();
            List<Package> dbList = _BClass.GetAll<Package>().ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }

        public PackageVM SelectObject(int Id)
        {
            Package _BClass = new Package();

            PackageVM Object = Convert(_BClass.Getobject(Id));
            return Object;
        }


        public List<SelectListItem> PackageSelectList()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            List<PackageVM> CliniList = SelectAll();

            foreach (PackageVM Obj in CliniList)
            {
                SelectListItem _item = new SelectListItem();
                _item.Text = Obj.Name;
                _item.Value = Obj.ID.ToString();
                listItems.Add(_item);
            }

            return listItems;
        }


    }
}
