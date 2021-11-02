using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.DAL
{
    [Table("TumerHistologyTypes")]
    public partial  class TumerHistologyTypes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TumerHistologyTypes()
        {
            this.pathologies = new HashSet<Pathology>();

        }
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public bool IsDeleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pathology> pathologies { get; set; }
    }
}
