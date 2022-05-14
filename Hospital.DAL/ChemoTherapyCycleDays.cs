
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Hospital.DAL
{
    public partial class ChemoTherapyCycleDays
    {
        [Key]
        public int ID { get; set; }
        public Nullable<int> Template_ID { get; set; }
        public Nullable<int> Day { get; set; }

    }
}
