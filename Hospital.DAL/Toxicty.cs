using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    [Table("Toxicities")]

    public partial class Toxicty
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        public string Description { get; set; }
        public int Condition { get; set; }
        public string Note { get; set; }
        public string Pharmacist_Note { get; set; }

        public int ToxictyTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? DoctorId { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PatientID { get; set; }
        public DateTime ToxictyDate { get; set; }
        public Nullable<bool> IsEditByPharmacy { get; set; }

        public virtual ToxictyType ToxictyTypes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual Patient Patient { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual AspNetUser AspNetUser { get; set; }

    }
}
