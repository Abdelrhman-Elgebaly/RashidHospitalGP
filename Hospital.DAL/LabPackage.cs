using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Hospital.DAL
{
    public partial class LabPackage
    {

        [Key]
        public int ID { get; set; }
        public Nullable<int> Package_ID { get; set; }
        public Nullable<int> Test { get; set; }
        public Nullable<int> Value { get; set; }
        public Nullable<int> Rule { get; set; }



    }
}
