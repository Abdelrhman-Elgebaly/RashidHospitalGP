
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    public partial class ChemoTherapyProtocol : DbBaseClass<ChemoTherapyProtocol>
    {



        public List<ChemoTherapyProtocol> GetProtocolsByPatientId(int PatientId)
        {
            List<ChemoTherapyProtocol> list = _Db.ChemoTherapyProtocols.Where(a => a.Patient_ID == PatientId).ToList();
            return list;

        }



        public List<ChemoTherapyProtocol> SelectAll()
        {
            List<ChemoTherapyProtocol> list = _Db.ChemoTherapyProtocols.ToList();
            return list;

        }
    }
}