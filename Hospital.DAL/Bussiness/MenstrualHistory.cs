using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    public partial class MenstrualHistory : DbBaseClass<MenstrualHistory>
    {
        public List<MenstrualHistory> GetALlByPatientId(int PatientId)
        {
            List<MenstrualHistory> medicalList = _Db.MenstrualHistories.Where(a => a.PatientId == PatientId).ToList();
            return medicalList;

        }
    }
}
