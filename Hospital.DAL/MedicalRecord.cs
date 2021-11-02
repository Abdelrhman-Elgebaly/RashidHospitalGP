namespace Hospital.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MedicalRecord
    {
        public int Id { get; set; }

       
        public string Complain { get; set; }

       
        public string Diagnose { get; set; }

      
        public string Recommendation { get; set; }
        public bool IsDeleted { get; set; }

        [Required]
        public DateTime RecordDate { get; set; }
        public bool Flag { get; set; }

        public Guid DoctorID { get; set; }

        public int ClinicId { get; set; }

        public int PatientID { get; set; }
        public Guid? ModifiedBy { get; set; }

        public virtual Clinic Clinic { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
