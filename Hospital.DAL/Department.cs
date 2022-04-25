using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Hospital.DAL
{
    [Table("Departments", Schema = "dbo")]
    public class Department
    {
        [Key]
        [Display(Name = "ID")]
        public int DepartmentId { get; set; }

        [Required]
        [Display(Name = "Department Name")]
        [Column(TypeName = "varchar(200)")]
        public string DepartmentName { get; set; } = string.Empty;
    }
}
