
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    public partial class ChemoTherapyCyclesDates : DbBaseClass<ChemoTherapyCyclesDates>
    {
        public List<ChemoTherapyCyclesDates> GetLabResualtByPatientId(int PatientId)
        {
            List<ChemoTherapyCyclesDates> medicalList = _Db.ChemoTherapyCyclesDates.Where(a => a.Patient_ID == PatientId).ToList();

          //  medicalList.Find(PatientId);

            return medicalList;

        }

       


       

    }
}