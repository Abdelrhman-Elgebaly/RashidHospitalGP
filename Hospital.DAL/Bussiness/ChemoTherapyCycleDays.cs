using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    public partial class ChemoTherapyCycleDays : DbBaseClass<ChemoTherapyCycleDays>
    {



        public List<ChemoTherapyCycleDays> GetChemoTherapyCycleDaysByTemplateId(int TemplateId)
        {
            List<ChemoTherapyCycleDays> list = _Db.ChemoTherapyCycleDays.Where(a => a.Template_ID == TemplateId).ToList();
            return list;

        }
    }
}