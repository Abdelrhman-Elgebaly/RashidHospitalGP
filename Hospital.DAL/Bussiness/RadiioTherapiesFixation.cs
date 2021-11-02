using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    public partial class RadiioTherapiesFixation : DbBaseClass<RadiioTherapiesFixation>
    {
        public List<RadiioTherapiesFixation> GetAllRadioTherapyId(int RadioTherapyId)
        {
            List<RadiioTherapiesFixation> medicalList = _Db.RadiioTherapiesFixations.Where(a => a.RadioTherapyId == RadioTherapyId).ToList();
            return medicalList;

        }
    }
}
