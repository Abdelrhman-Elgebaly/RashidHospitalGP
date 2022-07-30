using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Hospital.DAL
{
    public partial class ChemoTherapyDrug
    {

        [Key]
        public int Drug_ID { get; set; }
        public int Template_ID { get; set; }
        public string Therapy_Type { get; set; }
        public string Days { get; set; }
        public Nullable<int> Sequence_Number { get; set; }
        public string Drug_Name { get; set; }
        public double Drug_Dose { get; set; }
       
        public string Fluid_Vol { get; set; }

        public int Unit { get; set; }
        public int Route { get; set; }
        public int Fluid_Type { get; set; }

       
        public string Duration { get; set; }
        public string Note { get; set; }
        public int DrugType { get; set; }



    }
}
