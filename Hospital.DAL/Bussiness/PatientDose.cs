using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hospital.DAL
{
    public partial class PatientDose : DbBaseClass<PatientDose>
    {
        public List<PatientDose> GetAllPatientDoseByPatientId(int PatientId)
        {
            List<PatientDose> PatientDoseList = _Db.PatientDose.Where(a => a.Patient_ID == PatientId).ToList();
            return PatientDoseList;

        }
    }
}
