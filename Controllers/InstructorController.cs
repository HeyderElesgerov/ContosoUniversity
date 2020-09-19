using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Controllers
{
    public class InstructorController : Controller
    {
        private SchoolContext _context;

        public InstructorController(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int selectedInstructorId = 0, int selectedCourseId = 0)
        {
            InstructorsIndexViewModel viewModel = new InstructorsIndexViewModel();
            viewModel.SelectedInstructorId = selectedInstructorId;
            viewModel.SelectedCourseId = selectedCourseId;

            var instructors = await _context.Instructors
                                    .Include(p => p.OfficeAssignment)
                                    .Include(p => p.CourseAssignments)
                                        .ThenInclude(p => p.Course)
                                            .ThenInclude(p => p.Department)
                                    .Include(p => p.CourseAssignments)
                                        .ThenInclude(p => p.Course)
                                            .ThenInclude(p => p.Enrollments)
                                                .ThenInclude(e => e.Student)
                                    .ToListAsync();

            viewModel.Instructors = instructors;

            var selectedInstructor = instructors.Where(i => i.ID == selectedInstructorId).FirstOrDefault();

            if(selectedInstructor != null)
            {
                viewModel.CoursesOfSelectedInstructor = from courseAssignment in selectedInstructor.CourseAssignments
                                                        select courseAssignment.Course;

                var selectedCourse = viewModel.CoursesOfSelectedInstructor.Where(c => c.ID == selectedCourseId).FirstOrDefault();

                if (selectedCourse != null)
                {
                    viewModel.EnrollemntsOfSelectedCourse = selectedCourse.Enrollments;
                }
                else
                {
                    viewModel.EnrollemntsOfSelectedCourse = null;
                }
            }
            else
            {
                viewModel.CoursesOfSelectedInstructor = null;
                viewModel.EnrollemntsOfSelectedCourse = null;
            }

            return View(viewModel);
        }
    }
}
