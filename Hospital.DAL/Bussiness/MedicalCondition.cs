using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
   public partial class MedicalCondition:DbBaseClass<MedicalCondition>
    {
        public List<MedicalCondition> GetMedicalRecordsByPatientId(int PatientId)
        {
            List<MedicalCondition> medicalList = _Db.MedicalConditions.Where(a => a.PatientId == PatientId).ToList();
            return medicalList;

        }
    }
}
