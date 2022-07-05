using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Hospital.DAL
{
    public partial class ChemoTherapyTemplate
    {

        [Key]
        public int Template_ID { get; set; }
        public string Protocol_Name { get; set; }
        public Nullable<int> Frequency { get; set; }
        public string Cycle_days { get; set; }
        public Nullable<int> Maximum_cycles { get; set; }
        public Nullable<int> FN_risk { get; set; }
        public Nullable<int> Emetogenic_Level { get; set; }
        public string Link { get; set; }
        public string Date_Created { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Date_Modified { get; set; }
        public Nullable<System.Guid> Modified_By { get; set; }
        public string Instruction { get; set; }
        public Nullable<int> Admin_Day { get; set; }
        public Nullable<System.DateTime> Admin_Date { get; set; }
        public string Date_Entered { get; set; }
        public string Disease { get; set; }
    }
}
