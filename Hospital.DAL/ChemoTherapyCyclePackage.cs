
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Hospital.DAL
{
    public partial class ChemoTherapyCyclePackage
    {

        [Key]
        public int ID { get; set; }
        public Nullable<int> Cycle_ID { get; set; }
        public Nullable<int> Patient_ID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> Test_Value { get; set; }
        public string Note { get; set; }
        public Nullable<bool> IsApproved { get; set; }
      
        public int Test_Type { get; set; }
        public Nullable<int> Actual_Value { get; set; }

        public int Rule_Type { get; set; }
        public int TemplateId { get; set; }
        public Nullable<int> PreProtocol { get; set; }



    }
}
