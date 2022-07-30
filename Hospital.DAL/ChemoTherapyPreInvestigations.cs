using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Hospital.DAL
{
    public partial class ChemoTherapyPreInvestigations
    {
        [Key]
        public int ID { get; set; }
        public Nullable<int> Template_ID { get; set; }
        public string Days { get; set; }
        public string Test_Name { get; set; }
        public string rule { get; set; }
        public int Rule_Type { get; set; } = 1;
        public double Value { get; set; }

    }
}
