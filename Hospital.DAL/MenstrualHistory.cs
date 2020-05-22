namespace Hospital.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MenstrualHistory
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public int Duration { get; set; }

        public bool Premenopausal { get; set; }

        public bool Postmenopausal { get; set; }

        public bool Perimenopausal { get; set; }
        public bool IsDeleted { get; set; }


        public virtual Patient Patient { get; set; }
    }
}
