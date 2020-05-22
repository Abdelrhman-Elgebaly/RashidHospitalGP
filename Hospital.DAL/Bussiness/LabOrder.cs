using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
   public partial class LabOrder : DbBaseClass<LabOrder>
    {
        public List<LabOrder> GetLabOrderByPatientId(int PatientId)
        {
            List<LabOrder> medicalList = _Db.LabOrders.Where(a => a.PatinentId == PatientId).ToList();
            return medicalList;

        }
    }
}
