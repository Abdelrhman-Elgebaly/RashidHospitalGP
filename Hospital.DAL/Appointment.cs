namespace Hospital.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Appointment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Appointment()
        {
            CallBoards = new HashSet<CallBoard>();
        }

        public int Id { get; set; }

        public DateTime AppointmentDate { get; set; }

        public bool Done { get; set; }

        public int ClinicId { get; set; }

        public int PatientId { get; set; }
        public Guid? DoctorId { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }


        public virtual Clinic Clinic { get; set; }

        public virtual Patient Patient { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CallBoard> CallBoards { get; set; }

    }
}
