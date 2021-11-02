using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
   public partial class Toxicty : DbBaseClass<Toxicty>
    {
        public List<Toxicty> GetAllToxictyByPatientId(int PatientId)
        {
            List<Toxicty> ToxictyList = _Db.Toxicities.Where(a => a.PatientID == PatientId&& a.IsDeleted== false).ToList();
            return ToxictyList;

        }
    }
}
