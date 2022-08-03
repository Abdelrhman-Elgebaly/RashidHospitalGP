
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Hospital.DAL
{
    public partial class ChemoTherapyCycleDay
    {

        [Key]
        public int ID { get; set; }
        public Nullable<int> MainCycle_ID { get; set; }
        public int Patient_ID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
       
         public int TemplateId { get; set; }

        public bool IsReleased { get; set; }
        public bool IsPending { get; set; }
        public bool IsApproved { get; set; }

        public bool IsStart { get; set; }
        public Nullable<bool> IsDeleted { get; set; }


    }
}
