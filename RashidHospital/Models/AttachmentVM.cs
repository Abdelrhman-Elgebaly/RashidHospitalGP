using Hospital.DAL;
using RashidHospital.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RashidHospital.Models
{
    public class AttachmentVM : BusinessBaseClass<Attachment, AttachmentVM>
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public HttpPostedFileBase fileUploded { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{dd/mm/yyyy}")]
        public DateTime FileDate { get; set; }
        public string FileDetails { get; set; }
        public int UserId { get; set; }

        internal override Attachment Convert(AttachmentVM Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new Attachment
                {
                    Id = Obj.Id,
                    FileDate=Obj.FileDate,
                    FileDetails=Obj.FileDetails,
                    FileName=Obj.FileName,
                    UserId=Obj.UserId
                };
            }
            return _Obj;
        }

        internal override AttachmentVM Convert(Attachment _Obj)
        {
            AttachmentVM Obj = new AttachmentVM();
            if (_Obj == null)
                return null;
            else
            {
                Obj = new AttachmentVM
                {
                    Id = _Obj.Id,
                    FileDate = _Obj.FileDate,
                    FileDetails = _Obj.FileDetails,
                    FileName = _Obj.FileName,
                    UserId = _Obj.UserId
                };
            }
            return Obj;
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
        public List<AttachmentVM> GetAttachmentsByPatientId(int patientId)
        {
            AttachmentVM _Obj = new AttachmentVM();
            Attachment _BClass = new Attachment();
            List<Attachment> dbList = _BClass.GetAttachmentsByPatientId(patientId).ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }
        #endregion
    }
}