using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    public partial  class IHC : DbBaseClass<IHC>
    {
        public List<IHC> GetIHCByPathologyId(int PathologyId)
        {
            List<IHC> IHCList = _Db.IHCs.Where(a => a.PathologyId == PathologyId).ToList();
            return IHCList;

        }

    }
}
