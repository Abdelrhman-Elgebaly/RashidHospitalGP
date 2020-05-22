using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    public partial class RadiologyRequest : DbBaseClass<RadiologyRequest>
    {
        public List<RadiologyRequest> GetRadiologyRequestByPatientId(int PatientId)
        {
            List<RadiologyRequest> medicalList = _Db.RadiologyRequests.Where(a => a.PateintID == PatientId).ToList();
            return medicalList;

        }
    }
}
