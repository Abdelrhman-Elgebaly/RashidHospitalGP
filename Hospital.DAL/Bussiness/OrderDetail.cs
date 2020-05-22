using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
   public partial class OrderDetail : DbBaseClass<OrderDetail>
    {
        public List<OrderDetail> GetOrderDetailByLabOrderId(int LabOrderId)
        {
            List<OrderDetail> medicalList = _Db.OrderDetails.Where(a => a.LabOrderId == LabOrderId).ToList();
            return medicalList;

        }
    }
}
