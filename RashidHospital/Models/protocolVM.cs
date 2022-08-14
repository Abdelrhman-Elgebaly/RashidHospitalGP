using System;
using System.Collections.Generic;
using System.Linq;


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
    public class protocolVM : BusinessBaseClass<protocol, protocolVM>
    {
        [Key]
        public int ProtocolID { get; set; }
        public string ProtocolName { get; set; }
        public Nullable<int> DiseaseId { get; set; }


        internal override protocol Convert(protocolVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new protocol
                {
                    ProtocolID = Obj.ProtocolID,
                    ProtocolName = Obj.ProtocolName,
                    DiseaseId= Obj.DiseaseId,
                };
            }
            return _Obj;
        }

        internal override protocolVM Convert(protocol DbObj)
        {
            return new protocolVM()
            {
                ProtocolID = DbObj.ProtocolID,
                ProtocolName = DbObj.ProtocolName,
                DiseaseId = DbObj.DiseaseId,

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
        public List<protocolVM> SelectAll()
        {
            protocolVM _Obj = new protocolVM();
            protocol _BClass = new protocol();
            List<protocol> dbList = _BClass.GetAll<protocol>().ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }

        public protocolVM SelectObject(int Id)
        {
            protocol _BClass = new protocol();

            protocolVM ClinicObject = Convert(_BClass.Getobject(Id));
            return ClinicObject;
        }

        public List<SelectListItem> ProtocolSelectList(int DiseaseId)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            List<protocolVM> CliniList = SelectAll().Where(x => x.DiseaseId == DiseaseId).ToList();
            foreach (protocolVM Obj in CliniList)
            {
                SelectListItem _item = new SelectListItem();
                _item.Text = Obj.ProtocolName;
                _item.Value = Obj.ProtocolID.ToString();
                listItems.Add(_item);
            }

            return listItems;
        }
        #endregion
    }
}