using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
   public partial class Pathology : DbBaseClass<Pathology>
    {
        public List<Pathology> GetPathologyByPatientId(int PatientId)
        {
            List<Pathology> PathologyList = _Db.Pathologies.Where(a => a.PatientId == PatientId).ToList();
            return PathologyList;

        }

    }
}
