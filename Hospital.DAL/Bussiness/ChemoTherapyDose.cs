

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    public partial class ChemoTherapyDose : DbBaseClass<ChemoTherapyDose>
    {



        public List<ChemoTherapyDose> GetDoseByNoteId(int NoteId)
        {
            List<ChemoTherapyDose> list = _Db.ChemoTherapyDoses.Where(a => a.Note_ID == NoteId).ToList();
            return list;

        }
    }
}