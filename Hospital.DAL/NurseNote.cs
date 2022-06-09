using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Hospital.DAL
{
    public partial class NurseNote
    {

        [Key]
        public int ID { get; set; }
        public Nullable<int> Patient_ID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> Temperature { get; set; }
        public Nullable<int> Pulse { get; set; }
        public Nullable<int> BP { get; set; }
        public Nullable<double> Weight { get; set; }
        public Nullable<double> Height { get; set; }
        public Nullable<double> SA { get; set; }
        public bool IsApproved { get; set; }
        public Nullable<double> Dose_Calculated { get; set; }
        
    }
}
