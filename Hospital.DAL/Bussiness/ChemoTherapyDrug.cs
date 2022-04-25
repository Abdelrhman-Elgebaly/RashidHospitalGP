using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hospital.DAL
{
    public partial class ChemoTherapyDrug : DbBaseClass<ChemoTherapyDrug>
    {
        public List<ChemoTherapyDrug> GetChemoTherapyDrugByTemplateId(int TemplateId)
        {
            List<ChemoTherapyDrug> druglList = _Db.ChemoTherapyDrug.Where(a => a.Template_ID == TemplateId).ToList();
            return druglList;

        }
    }
}
