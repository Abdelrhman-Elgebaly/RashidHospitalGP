
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hospital.DAL
{
    public partial class ChemoTherapyTemplate : DbBaseClass<ChemoTherapyTemplate>
    {

        public List<ChemoTherapyTemplate> GetChemoTherapyTemplate()
        {
            List<ChemoTherapyTemplate> list = _Db.ChemoTherapyTemplate.ToList();
            return list;

        }

    }
}

