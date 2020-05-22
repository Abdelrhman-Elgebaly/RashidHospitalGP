using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    public partial class LabResualt : DbBaseClass<LabResualt>
    {
        public List<LabResualt> GetLabResualtByPatientId(int PatientId)
        {
            List<LabResualt> medicalList = _Db.LabResualts.Where(a => a.PatientId == PatientId).ToList();
            return medicalList;

        }
    }
}
