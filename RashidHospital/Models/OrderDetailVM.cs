using Hospital.DAL;
using RashidHospital.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static RashidHospital.Helper.Enum;

namespace RashidHospital.Models
{
    public class OrderDetailVM : BusinessBaseClass<OrderDetail, OrderDetailVM>
    {
        public int Id { get; set; }
        public int LabOrderId { get; set; }
        public int Type { get; set; }
        public string TypeValue { get; set; }
        public int PatientId { get; set; }
        public LabTyps[] selectedTypes { get; set; }
        public bool IsDeleted { get; set; }

        internal override OrderDetailVM Convert(OrderDetail Obj)
        {
            var enumType = (LabTyps)Obj?.Type;

            return new OrderDetailVM
            {
                Id = Obj.Id,
                LabOrderId = Obj.LabOrderId,
                Type = Obj.Type,
                TypeValue = enumType.ToString(),
                IsDeleted = Obj.IsDeleted
            };
        }

        internal override OrderDetail Convert(OrderDetailVM Obj)
        {
            return new OrderDetail
            {
                Id = Obj.Id,
                LabOrderId = Obj.LabOrderId,
                Type = Obj.Type,
                IsDeleted=Obj.IsDeleted
            };
        }


        #region Functions
        public void Create()
        {
            _Obj = Convert(this);
            _Obj.AddNew();
        }
        public void Delete()
        {
            _Obj = Convert(this);
            _Obj.Delete();
        }
        public List<OrderDetailVM> SelectAllByLabOrderId(int LabOrderId)
        {
            OrderDetailVM _Obj = new OrderDetailVM();
            OrderDetail _BClass = new OrderDetail();
            List<OrderDetail> dbList = _BClass.GetOrderDetailByLabOrderId(LabOrderId).ToList();
            return dbList.Select(z => _Obj.Convert(z)).ToList();
        }



        #endregion
    }
}