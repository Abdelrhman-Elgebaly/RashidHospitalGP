using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    public partial class ChemoTherapyCycleInvestigation : DbBaseClass<ChemoTherapyCycleInvestigation>
    {
        public List<ChemoTherapyCycleInvestigation> GetInvestigationByCycleID(int CycleID)
        {
            List<ChemoTherapyCycleInvestigation> medicalList = _Db.ChemoTherapyCycleInvestigations.Where(a => a.Cycle_ID == CycleID).ToList();
            return medicalList;

        }









    }
}
