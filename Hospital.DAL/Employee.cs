using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Hospital.DAL
{
    [Table("Employees", Schema = "dbo")]
    public class Employee
    {
        [Key]
        [Display(Name = "ID")]
        public int? EmployeeId { get; set; }

        [Required]
        [Display(Name = "Name")]
        [Column(TypeName = "varchar(250)")]
        public string EmployeeName { get; set; } = string.Empty;

        [Display(Name = "Image User")]
        [Column(TypeName = "varchar(250)")]
        public string ImageUser { get; set; }
        [Required]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required, Display(Name = "Salary"), Column(TypeName = "decimal(12,2)")]
        public decimal Salary { get; set; }

        [Required]
        [Display(Name = "Hiring Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime HiringDate { get; set; }

        [Required]
        [Display(Name = "National ID")]
        [Column(TypeName = "varchar(14)")]
        [MaxLength(14)]
        [MinLength(14)]
        public string NationalId { get; set; } = string.Empty;

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

    }
}
