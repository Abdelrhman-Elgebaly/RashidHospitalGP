using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    public partial class Attachment : DbBaseClass<Attachment>
    {
        public List<Attachment> GetAttachmentsByPatientId(int PatientId)
        {
            List<Attachment> medicalList = _Db.Attachments.Where(a => a.UserId == PatientId).ToList();
            return medicalList;

        }
    }
}
