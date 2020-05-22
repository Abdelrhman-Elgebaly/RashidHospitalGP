namespace Hospital.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RadiologyRequest
    {
        public int Id { get; set; }
        public int ProcedureType { get; set; }

        public int Site { get; set; }

        public bool Contrast { get; set; }

        public string Note { get; set; }

        public DateTime RequestDate { get; set; }
        public int PateintID { get; set; }
        public bool IsDeleted { get; set; }

        public Guid CreatedBy { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
