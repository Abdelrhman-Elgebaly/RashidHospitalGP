using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    public partial class RadiologyResult : DbBaseClass<RadiologyResult>
    {
        public List<RadiologyResult> GetRadiologyResultByPatientId(int PatientId)
        {
            List<RadiologyResult> medicalList = _Db.RadiologyResults.Where(a => a.PateintID == PatientId).ToList();
            return medicalList;

        }
    }
}
