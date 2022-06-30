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
        public List<ChemoTherapyCycleDay> GetCycleDaysByMainId(int MainCycleId)
        {
            List<ChemoTherapyCycleDay> medicalList = _Db.ChemoTherapyCycleDays.Where(a => a.MainCycle_ID == MainCycleId).ToList();

            //  medicalList.Find(PatientId);

            return medicalList;

        }






    }
}