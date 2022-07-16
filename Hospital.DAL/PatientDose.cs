
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
        public string Therapy_Type { get; set; }

        public string Drug_Name { get; set; }
        public int Drug_Dose { get; set; }

        public string Fluid_Vol { get; set; }
        public string Unit_Value { get; set; }

    }
}
