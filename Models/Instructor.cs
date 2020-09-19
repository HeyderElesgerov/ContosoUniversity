using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public class Instructor
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(maximumLength: 30, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(maximumLength: 50, MinimumLength = 2)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [Required]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        [Display(Name = "Office")]
        public OfficeAssignment OfficeAssignment { get; set; }
        public ICollection<Department> Departments { get; set; }

        [Display(Name = "Courses")]
        public ICollection<CourseAssignment> CourseAssignments { get; set; }
    }
}
