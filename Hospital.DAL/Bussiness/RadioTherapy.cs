using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    public partial  class RadioTherapy : DbBaseClass<RadioTherapy>
    {
        public List<RadioTherapy> GetRadioTherapyByPatientId(int PatientId)
        {
            List<RadioTherapy> medicalList = _Db.RadioTherapies.Where(a => a.PatientID == PatientId && a.IsDeleted== false).ToList();
            return medicalList;

        }
    }
}
