
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    public partial class LabPackage : DbBaseClass<LabPackage>
    {



        public List<LabPackage> GetLabPackagesByPackageId(int PackageId)
        {
            List<LabPackage> list = _Db.LabPackages.Where(a => a.Package_ID == PackageId).ToList();
            return list;

        }
    }
}