
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hospital.DAL
{
    public partial class Package : DbBaseClass<Package>
    {

        public List<Package> GetPackages()
        {
            List<Package> list = _Db.Packages.ToList();
            return list;

        }

    }
}

