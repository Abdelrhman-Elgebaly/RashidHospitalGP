
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Hospital.DAL
{
    public partial class ChemoTherapyCycleInvestigation
    {

        [Key]
        public int ID { get; set; }
        public Nullable<int> Cycle_ID { get; set; }
        public Nullable<int> Patient_ID { get; set; }
       
        public string Inves_Type { get; set; }
        public Nullable<double> Actual_Value { get; set; }

        public Nullable<int> Rule_Type { get; set; }
        public int TemplateId { get; set; }
        public double Value { get; set; }
        public string Note { get; set; }


    }
}
