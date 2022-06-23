using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
    {
        public partial class ChemoTherapyCyclePackage : DbBaseClass<ChemoTherapyCyclePackage>
        {
            public List<ChemoTherapyCyclePackage> GetLabPackageByPatientId(int PatientId)
        {
            List<ChemoTherapyCyclePackage> medicalList = _Db.ChemoTherapyCyclePackage.Where(a => a.Patient_ID == PatientId).ToList();
            return medicalList;

        }









    }
}
