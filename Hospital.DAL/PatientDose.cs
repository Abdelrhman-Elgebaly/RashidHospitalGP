
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Hospital.DAL
{
    public partial class PatientDose
    {

        [Key]
        public int ID { get; set; }
        public Nullable<int> Patient_ID { get; set; }
       
        public Nullable<int> NurseNote_ID { get; set; }

        public Nullable<int>Template_ID{ get; set; }
        public Nullable<int>Cycle_ID { get; set; }
        public Nullable<double> Dose_Calculated { get; set; }
   
    }
}
