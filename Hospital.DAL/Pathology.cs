namespace Hospital.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Pathology
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pathology()
        {
            IHCS = new HashSet<IHC>();
        }
        public int Id { get; set; }

        public string ProcedureType { get; set; }

        public DateTime Date { get; set; }

        [StringLength(50)]
        public string pT { get; set; }

        [StringLength(50)]
        public string pN { get; set; }

        [StringLength(50)]
        public string pM { get; set; }

        public string Comment { get; set; }

        public int TumerHistology { get; set; }

        public int? TumerGrade { get; set; }

        public int? TumerSize { get; set; }

        public int? TumerNumber { get; set; }

        public int? TumerFocality { get; set; }

        public bool? TumerLVI { get; set; }

        public bool? TumerPNI { get; set; }

        public int? TumerMargin { get; set; }

        [StringLength(50)]
        public string LymphnodeTypeOfsurgery { get; set; }

        [StringLength(50)]
        public string LymphnodePositiveLN { get; set; }

        [StringLength(50)]
        public string LymphnodeTotalLN { get; set; }

        public bool? LymphnodeECE { get; set; }

        public int PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }

        public virtual AspNetUser Doctors { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual TumerHistologyTypes TumerHistologyType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<IHC> IHCS { get; set; }

    }
}
