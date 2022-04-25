using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    public partial class ChemoTherapyPreLab : DbBaseClass<ChemoTherapyPreLab>
    {



        public List<ChemoTherapyPreLab> GetChemoTherapyPreLabByTemplateId(int TemplateId)
        {
            List<ChemoTherapyPreLab> list = _Db.ChemoTherapyPreLab.Where(a => a.Template_ID == TemplateId).ToList();
            return list;

        }
    }
}