namespace Hospital.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RadiologyResult
    {
        public int Id { get; set; }
        public int ProcedureType { get; set; }

        public int Site { get; set; }

        public bool Contrast { get; set; }

        public DateTime RadiologyDate { get; set; }

        public int Recist { get; set; }

        [Required]
        [StringLength(50)]
        public string T { get; set; }

        [Required]
        [StringLength(50)]
        public string N { get; set; }

        [Required]
        [StringLength(10)]
        public string M { get; set; }
        public int PateintID { get; set; }
        public Guid CreatedBy { get; set; }
        public string Note { get; set; }
        public bool IsDeleted { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
