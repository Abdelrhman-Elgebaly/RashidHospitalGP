namespace Hospital.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IHC")]
    public partial class IHC
    {
        public int Id { get; set; }

        public int Type { get; set; }

        public string Result { get; set; }

        [Required]
        [StringLength(50)]
        public string Value { get; set; }

        public DateTime ResultDate { get; set; }

        public int PatientId { get; set; }
        public int PathologyId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Pathology Pathologies { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
