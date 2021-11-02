namespace Hospital.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SurgicalCondition
    {
        public int Id { get; set; }

        public string Condition { get; set; }

        public DateTime ConditionDate { get; set; }

        public int PatientId { get; set; }

        public int ConditionType { get; set; }
        public bool IsDeleted { get; set; }


        public virtual Patient Patient { get; set; }
    }
}
