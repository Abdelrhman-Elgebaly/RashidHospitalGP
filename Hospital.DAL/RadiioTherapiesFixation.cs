using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
   public partial class RadiioTherapiesFixation
    {
      
        public int Id { get; set; }
        public int RadioTherapyId { get; set; }
        public int FixationsId { get; set; }
        public virtual RadioTherapy RadioTherapys { get; set; }
        public virtual Fixation Fixations { get; set; }
    }
}
