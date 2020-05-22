using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    public partial class AspNetUser : DbBaseClass<AspNetUser>
    {
        public static AspNetUser SelectObjectByEmail(string Email)
        {
            return _Sdb.AspNetUsers.Where(a => a.UserName.ToLower() == Email.ToLower())?.FirstOrDefault();
        }
    }
}
