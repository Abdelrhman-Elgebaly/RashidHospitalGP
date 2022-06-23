
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Hospital.DAL
{
    public partial class ChemoTherapyCyclesDates
    {

        [Key]
        public int ID { get; set; }
    
        public Nullable<int> Patient_ID { get; set; }
        public DateTime Date { get; set; }
        public int Cycles_Number { get; set; }
        // 
    }
}
