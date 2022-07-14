
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    public partial class ChemoTherapyCyclesDates : DbBaseClass<ChemoTherapyCyclesDates>
    {
        public List<ChemoTherapyCyclesDates> GetCyclesByTemplateId(int TemplateId)
        {
            List<ChemoTherapyCyclesDates> medicalList = _Db.ChemoTherapyCyclesDates.Where(a => a.TemplateId == TemplateId).ToList();

          //  medicalList.Find(PatientId);

            return medicalList;

        }

       


       

    }
}