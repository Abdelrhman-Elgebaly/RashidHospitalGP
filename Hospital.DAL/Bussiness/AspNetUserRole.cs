using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    public partial class AspNetUserRole :DbBaseClass<AspNetUserRole>
    {
        public static bool CheckExist(Guid RoleId, Guid UserId)
        {
            return _Sdb.AspNetUserRoles.Where(a => a.UserId == UserId && a.RoleId == RoleId)?.FirstOrDefault() == null ? true : false;
        }
        public static string GetId(Guid RoleId, Guid UserId)
        {

            AspNetUserRole _obj = _Sdb.AspNetUserRoles.Where(a => a.UserId == UserId && a.RoleId == RoleId)?.FirstOrDefault();
            return _obj == null ? "" : _obj.Id.ToString();
        }
    }
}
