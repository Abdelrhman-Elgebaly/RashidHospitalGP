using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public int LabOrderId { get; set; }
        public int Type { get; set; }
        public bool IsDeleted { get; set; }

        public virtual LabOrder Orders { get; set; }

    }
}
