using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public int Budget { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        public int? AdminstratorId { get; set; }

        public Instructor Adminstrator { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
