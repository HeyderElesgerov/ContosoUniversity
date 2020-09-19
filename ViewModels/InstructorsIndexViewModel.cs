using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.ViewModels
{
    public class InstructorsIndexViewModel
    {
        public int SelectedInstructorId { get; set; }
        public int SelectedCourseId { get; set; }

        public IEnumerable<Instructor> Instructors;
        public IEnumerable<Course> CoursesOfSelectedInstructor { get; set; }
        public IEnumerable<Enrollment> EnrollemntsOfSelectedCourse { get; set; }
    }
}
