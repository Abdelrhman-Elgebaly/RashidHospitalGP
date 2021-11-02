namespace Hospital.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LabResualt
    {
        public int Id { get; set; }

        public string Note { get; set; }
        public string Unit { get; set; }
        public DateTime ResualtDate { get; set; }

        public int PatientId { get; set; }
        public bool IsDeleted { get; set; }

        public int LabType { get; set; }
        public Guid DoctorId { get; set; }
        public virtual AspNetUser Doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
