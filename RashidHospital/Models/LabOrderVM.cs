using Hospital.DAL;
using RashidHospital.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static RashidHospital.Helper.Enum;

namespace RashidHospital.Models
{
    public class LabOrderVM : BusinessBaseClass<LabOrder, LabOrderVM>
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        public int PatinentId { get; set; }

        public Guid DoctorId { get; set; }
        public string DoctorName { get; set; }
        public bool IsDeleted { get; set; }


        internal override LabOrder Convert(LabOrderVM Obj)
        {
           return new LabOrder
            {
                Id = Obj.Id,
               DoctorId=Obj.DoctorId,
               PatinentId=Obj.PatinentId,
               OrderDate=Obj.OrderDate,
               IsDeleted=Obj.IsDeleted
            };
        }

        internal override LabOrderVM Convert(LabOrder Obj)
        {
            return new LabOrderVM
            {
                Id = Obj.Id,
                DoctorId = Obj.DoctorId,
                PatinentId = Obj.PatinentId,
                OrderDate = Obj.OrderDate,
                IsDeleted=Obj.IsDeleted,
                DoctorName = Obj.AspNetUser.FirstName + " " + Obj.AspNetUser.SecondName + "  " + Obj.AspNetUser.ThirdName

            };
        }

        #region Functions
        public int Create()
        {
            _Obj = Convert(this);
            _Obj.AddNew();
            return _Obj.Id;
        }
        public void Delete()
        {
            _Obj = Convert(this);
            _Obj.Delete();
        }

        public List<LabOrderVM> SelectAllByPatientId(int PatientId)
        {
            LabOrderVM _Obj = new LabOrderVM();
            LabOrder _BClass = new LabOrder();
            List<LabOrder> dbList = _BClass.GetLabOrderByPatientId(PatientId).ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }

        public LabOrderVM SelectObject(int Id)
        {
            LabOrder _BClass = new LabOrder();

            LabOrderVM ClinicObject = Convert(_BClass.Getobject(Id));
            return ClinicObject;
        }

        #endregion
    }
}