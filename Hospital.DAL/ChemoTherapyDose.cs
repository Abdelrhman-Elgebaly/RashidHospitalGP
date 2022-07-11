
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Hospital.DAL
{
    public partial class ChemoTherapyDose
    {

        [Key]
        public int ID { get; set; }
        public Nullable<int> Patient_ID { get; set; }
        public Nullable<int> Drug_ID { get; set; }
        public Nullable<int> Dose { get; set; }
        public Nullable<int> Note_ID { get; set; }

        

    }
}
