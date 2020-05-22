using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    public partial class Patient : DbBaseClass<Patient>
    {
        static readonly object _locker = new object();

        public static string NextPatientId()
        {
            lock (_locker)
            {
                var patientList = _Sdb.Patients.OrderByDescending(a => a.Id).FirstOrDefault();

                string _Last = patientList != null? patientList.MedicalID: "ACOD-0";
                var result = _Last.Substring(_Last.LastIndexOf('-') + 1);
                int count = int.Parse(result);
                 count = count+1;
                string Next = "ACOD-" + count.ToString();
                return Next;
            }
        }
    }
}
