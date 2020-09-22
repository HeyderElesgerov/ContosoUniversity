using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
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

            if (selectedInstructor != null)
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

        public async Task<IActionResult> Create()
        {
            var viewModel = new NewInstructorViewModel();
            viewModel.CourseAssignments = await PopulateCourseAssignments();

            return View(viewModel);
        }
        //post
        [HttpPost]
        public async Task<IActionResult> Create(NewInstructorViewModel newInstructorViewModel)
        {
            if (ModelState.IsValid)
            {
                var newInstructor = new Instructor();
                newInstructor.FirstName = newInstructorViewModel.FirstName;
                newInstructor.LastName = newInstructorViewModel.LastName;
                newInstructor.HireDate = newInstructor.HireDate;

                if (string.IsNullOrEmpty(newInstructorViewModel.OfficeLocation))
                {
                    newInstructor.OfficeAssignment = null;
                }
                else
                {
                    newInstructor.OfficeAssignment = new OfficeAssignment()
                    {
                        Location = newInstructorViewModel.OfficeLocation,
                        Instructor = newInstructor
                    };
                }
                _context.Instructors.Add(newInstructor);

                foreach (var courseAssignment in newInstructorViewModel.CourseAssignments)
                {
                    if (courseAssignment.IsAssigned)
                    {
                        var courseInDb = await _context.Courses.Where(c => c.ID == courseAssignment.CourseID).FirstOrDefaultAsync();

                        if (courseInDb != null)
                        {
                            _context.CourseAssignments.Add(
                                new CourseAssignment()
                                {
                                    Course = courseInDb,
                                    Instructor = newInstructor
                                });
                        }
                    }
                }

                try
                {

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DBConcurrencyException)
                {
                    ModelState.AddModelError("Database Error", "Unable to insert new record. Try again later.");
                }
            }

            newInstructorViewModel.CourseAssignments = await PopulateCourseAssignments();
            return View(newInstructorViewModel);
        }

        private async Task<List<CourseAssignmentViewModel>> PopulateCourseAssignments()
        {
            var courseAssignments = new List<CourseAssignmentViewModel>();

            foreach (var course in await _context.Courses.ToListAsync())
            {
                var courseAssignmentViewModel = new CourseAssignmentViewModel
                {
                    CourseID = course.ID,
                    CourseTitle = course.Title,
                    IsAssigned = false
                };

                courseAssignments.Add(courseAssignmentViewModel);
            }

            return courseAssignments;
        }
    }
}
