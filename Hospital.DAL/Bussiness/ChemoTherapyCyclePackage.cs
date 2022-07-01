using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
    {
        public partial class ChemoTherapyCyclePackage : DbBaseClass<ChemoTherapyCyclePackage>
        {
            public List<ChemoTherapyCyclePackage> GetLabPackageByCycleID(int CycleID)
        {
            List<ChemoTherapyCyclePackage> medicalList = _Db.ChemoTherapyCyclePackages.Where(a => a.Cycle_ID == CycleID).ToList();
            return medicalList;

        }









    }
}
