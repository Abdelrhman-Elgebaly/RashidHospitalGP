using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hospital.DAL
{
    public partial class NurseNote : DbBaseClass<NurseNote>
    {
        public List<NurseNote> GetAllNurseNoteByPatientId(int PatientId)
        {
            List<NurseNote> NurseNoteList = _Db.NurseNote.Where(a => a.Patient_ID == PatientId ).ToList();
            return NurseNoteList;

        }
    }
}
