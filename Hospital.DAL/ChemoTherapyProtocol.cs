
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Hospital.DAL
{
    public partial class ChemoTherapyProtocol
    {

        [Key]
        public int ID { get; set; }
        public int Patient_ID { get; set; }
        public int Template_ID { get; set; }
        public Nullable<int> Cycles_Number { get; set; }
        public Nullable<int> ProtocolId { get; set; }
        public Nullable<int> DiseaseId { get; set; }

    }
}
