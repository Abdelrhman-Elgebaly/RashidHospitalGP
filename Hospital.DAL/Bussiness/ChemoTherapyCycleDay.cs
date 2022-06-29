using System;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    public partial class ChemoTherapyCycleDay : DbBaseClass<ChemoTherapyCycleDay>
    {
        public List<ChemoTherapyCycleDay> GetLabResualtByPatientId(int PatientId)
        {
            List<ChemoTherapyCycleDay> medicalList = _Db.ChemoTherapyCycleDay.Where(a => a.Patient_ID == PatientId).ToList();

            //  medicalList.Find(PatientId);

            return medicalList;

        }






    }
}