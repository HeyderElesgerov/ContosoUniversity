using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.ViewModels
{
    public class CourseAssignmentViewModel
    {
        public int CourseID { get; set; }
        public string CourseTitle { get; set; }
        public bool IsAssigned { get; set; }
    }
}
