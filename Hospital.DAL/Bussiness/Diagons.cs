using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    public partial class Diagons:DbBaseClass<Diagons>
    {
        public  Diagons GetDefault()
        {
            int _default = 1;
            Diagons _diagnosis= _Db.Diagonses.Where(a => a.Id == _default).FirstOrDefault();
            return _diagnosis;
        }
    }
}
