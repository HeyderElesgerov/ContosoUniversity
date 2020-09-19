using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.ViewModels
{
    public class EnrollmentDateGroup
    {
        [Display(Name = "Enrollment Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? EnrollmentDate { get; set; }

        [Display(Name = "Student Count")]
        public int StudentCount { get; set; }
    }
}
