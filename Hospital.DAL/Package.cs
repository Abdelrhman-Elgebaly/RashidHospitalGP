
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Hospital.DAL
{
    public partial class Package
    {

        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

    }
}
