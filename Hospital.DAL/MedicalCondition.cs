namespace Hospital.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MedicalCondition
    {
        public int Id { get; set; }

        public string Condition { get; set; }

        public DateTime? ConditionDate { get; set; }

        public int PatientId { get; set; }
        public bool IsDeleted { get; set; }


        public int ConditionType { get; set; }
        public int HistroryType { get; set; }
        public Guid DoctorId { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
