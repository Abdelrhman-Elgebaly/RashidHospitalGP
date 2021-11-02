namespace Hospital.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CallBoard
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public DateTime VisitDate { get; set; }

        public int PatientNumber { get; set; }

        public bool IsOnCall { get; set; }

        public int AppointmentId { get; set; }
        public int ClinicId { get; set; }

        public int? CallsNo { get; set; }

        public DateTime? LastCallTime { get; set; }
        public bool Done { get; set; }
        public Guid? DoctorId { get; set; }
        public Guid? ModifiedBy { get; set; }
        
        public bool IsDeleted { get; set; }
        public virtual Appointment Appointment { get; set; }
        public virtual Clinic Clinic { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
