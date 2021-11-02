using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    [Table("RadioTherapies")]

    public partial class RadioTherapy
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public Guid DoctorId { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PatientID { get; set; }
        public int RTXSite { get; set; }
        public int RTXType { get; set; }
        public int TotalDose { get; set; }
        public int FractionNumber { get; set; }

        public int DoseFractionNumberResult { get; set; }

        public int Fixation { get; set; }
        public bool CocomittantChemoTherapy { get; set; }
        public int TypeOfTechnique { get; set; }
        public string Note { get; set; }
        public string Attachment { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public bool Done { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual Patient Patient { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual AspNetUser AspNetUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RadiioTherapiesFixation> RadiioTherapiesFixations { get; set; }
    }
}
