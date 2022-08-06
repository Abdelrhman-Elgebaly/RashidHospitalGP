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


        public List<ChemoTherapyCycleDay> GetAllReleasedCycleDays()
        {
            List<ChemoTherapyCycleDay> medicalList = _Db.ChemoTherapyCycleDays.Where(a => a.IsReleased == true).ToList();

            //  medicalList.Find(PatientId);

            return medicalList;

        }
        public List<ChemoTherapyCycleDay> GetAllPendingCycleDays()
        {
            List<ChemoTherapyCycleDay> medicalList = _Db.ChemoTherapyCycleDays.Where(a => a.IsPending == true).ToList();

            //  medicalList.Find(PatientId);

            return medicalList;

        }
        public List<ChemoTherapyCycleDay> GetAllFinalApprovedCycleDays()
        {
            List<ChemoTherapyCycleDay> medicalList = _Db.ChemoTherapyCycleDays.Where(a => a.IsApproved == true).ToList();

            //  medicalList.Find(PatientId);

            return medicalList;

        }

        public List<ChemoTherapyCycleDay> GetCyclesByTemplateId(int TemplateId)
        {
            List<ChemoTherapyCycleDay> medicalList = _Db.ChemoTherapyCycleDays.Where(a => a.TemplateId == TemplateId).ToList();

            //  medicalList.Find(PatientId);

            return medicalList;

        }




    }
}