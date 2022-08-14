using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Hospital.DAL
{
    public partial class protocol
    {
        [Key]
        public int ProtocolID { get; set; }
        public string ProtocolName { get; set; }
        public Nullable<int> DiseaseId { get; set; }

        public virtual Disease Disease { get; set; }
    }
}
