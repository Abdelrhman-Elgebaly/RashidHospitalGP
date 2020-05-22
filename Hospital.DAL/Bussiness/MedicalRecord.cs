using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
   public partial class MedicalRecord : DbBaseClass<MedicalRecord>
    {
        public List<MedicalRecord> GetMedicalRecordsByPatientId(int PatientId)
        {
            List<MedicalRecord> medicalList = _Db.MedicalRecords.Where(a => a.PatientID == PatientId).ToList();
            return medicalList;

        }
    }
}
