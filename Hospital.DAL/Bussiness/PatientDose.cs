using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hospital.DAL
{
    public partial class PatientDose : DbBaseClass<PatientDose>
    {
       



        public List<PatientDose> GetAllPatientDoses(int NoteId, int CycleId)
        {
            List<PatientDose> PatientDoseList = _Db.PatientDose.Where(a => a.NurseNote_ID == NoteId && a.Cycle_ID == CycleId).ToList();
            return PatientDoseList;

        }



    }
}
