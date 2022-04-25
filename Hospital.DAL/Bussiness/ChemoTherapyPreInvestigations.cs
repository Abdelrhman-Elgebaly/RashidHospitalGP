

    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
    {
        public partial class ChemoTherapyPreInvestigations : DbBaseClass<ChemoTherapyPreInvestigations>
        {



            public List<ChemoTherapyPreInvestigations> GetChemoTherapyPreLabByTemplateId(int TemplateId)
            {
                List<ChemoTherapyPreInvestigations> list = _Db.ChemoTherapyPreInvestigations.Where(a => a.Template_ID == TemplateId).ToList();
                return list;

            }
        }
    }