using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Controllers
{
    public class StudentController : Controller
    {
        private readonly SchoolContext _context;

        public StudentController(SchoolContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString, string sortby, int currentPage)
        {
            IEnumerable<Student> students = await _context.Students.AsNoTracking().ToListAsync();

            //
            //  Searching
            //

            SortedAndFilteredStudentsViewModel viewModel = new SortedAndFilteredStudentsViewModel();
            viewModel.SearchString = searchString;

            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                               s.LastName.ToLower().Contains(searchString.ToLower()));
            }

            //
            //  Paging
            //

            int studentCount = students.Count();
            int maxPageCount;//1 based, pageIndex - 0 based

            if (studentCount % 3 == 0)
                maxPageCount = studentCount / 3;
            else
                maxPageCount = (studentCount / 3) + 1;

            if (currentPage < 1)
            {
                currentPage = 1;
            }
            if (currentPage > maxPageCount)
            {
                currentPage = maxPageCount;
            }

            viewModel.MaxPageCount = maxPageCount;
            viewModel.CurrentPage = currentPage;

            students = students.Skip(3 * (viewModel.CurrentPage - 1)).Take(3);

            //
            //  Sorting
            //

            SortStudentsParams sortingParam = SortStudentsParams.FirstNameAscending;//default if invalid data -> sortby 

            Enum.TryParse<SortStudentsParams>(sortby, true, out sortingParam);

            switch (sortingParam)
            {
                case SortStudentsParams.FirstNameAscending:
                    students = students.OrderBy(s => s.FirstName);
                    viewModel.SortyByFirstName = SortStudentsParams.FirstNameDescending.ToString();

                    viewModel.SortByLastName = SortStudentsParams.LastNameAscending.ToString();
                    viewModel.SortyByEnollmentDate = SortStudentsParams.EnrollmentDateAscending.ToString();
                    break;

                case SortStudentsParams.FirstNameDescending:
                    students = students.OrderByDescending(s => s.FirstName);
                    viewModel.SortyByFirstName = SortStudentsParams.FirstNameAscending.ToString();

                    viewModel.SortByLastName = SortStudentsParams.LastNameAscending.ToString();
                    viewModel.SortyByEnollmentDate = SortStudentsParams.EnrollmentDateAscending.ToString();
                    break;

                case SortStudentsParams.LastNameAscending:
                    students = students.OrderBy(s => s.LastName);
                    viewModel.SortByLastName = SortStudentsParams.LastNameDescending.ToString();

                    viewModel.SortyByFirstName = SortStudentsParams.FirstNameAscending.ToString();
                    viewModel.SortyByEnollmentDate = SortStudentsParams.EnrollmentDateAscending.ToString();
                    break;

                case SortStudentsParams.LastNameDescending:
                    students = students.OrderByDescending(s => s.LastName);
                    viewModel.SortByLastName = SortStudentsParams.LastNameAscending.ToString();

                    viewModel.SortyByFirstName = SortStudentsParams.FirstNameAscending.ToString();
                    viewModel.SortyByEnollmentDate = SortStudentsParams.EnrollmentDateAscending.ToString();
                    break;

                case SortStudentsParams.EnrollmentDateAscending:
                    students = students.OrderBy(s => s.EnrollmentDate);
                    viewModel.SortyByEnollmentDate = SortStudentsParams.EnrollmentDateDescending.ToString();

                    viewModel.SortyByFirstName = SortStudentsParams.FirstNameAscending.ToString();
                    viewModel.SortByLastName = SortStudentsParams.LastNameAscending.ToString();
                    break;

                case SortStudentsParams.EnrollmentDateDescending:
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    viewModel.SortyByEnollmentDate = SortStudentsParams.EnrollmentDateAscending.ToString();

                    viewModel.SortyByFirstName = SortStudentsParams.FirstNameAscending.ToString();
                    viewModel.SortByLastName = SortStudentsParams.LastNameAscending.ToString();
                    break;
            }


            //init fields of view model
            viewModel.Students = students;

            return View(viewModel);
        }



        public async Task<IActionResult> Details(int id)
        {
            var student = await _context.Students
                                        .Include(e => e.Enrollments)
                                        .ThenInclude(c => c.Course)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(s => s.ID == id);

            if (student == null)
                return NotFound();


            return View(student);
        }

        public IActionResult Create()
        {
            return View(new Student() { EnrollmentDate = DateTime.Today });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                var newStudent = new Student();
                newStudent.FirstName = student.FirstName;
                newStudent.LastName = student.LastName;
                newStudent.EnrollmentDate = student.EnrollmentDate;

                await _context.Students.AddAsync(newStudent);

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DBConcurrencyException)
                {
                    ModelState.AddModelError("Database Error", "Cannot add. Please, try again.");
                }
            }

            return View(student);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _context.Students
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(s => s.ID == id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id)
        {
            var studentInDb = await _context.Students.FirstOrDefaultAsync(s => s.ID == id);

            if (studentInDb == null)
                return NotFound();

            if (await TryUpdateModelAsync(
                studentInDb,
                "",
                s => s.FirstName, s => s.LastName, s => s.EnrollmentDate))
            {
                _context.Entry(studentInDb).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DBConcurrencyException)
                {
                    ModelState.AddModelError("Database Error", "Data changed before trying to update");
                }
            }

            return View(studentInDb);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.ID == id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            var studentInDb = await _context.Students.FirstOrDefaultAsync(s => s.ID == id);

            if (studentInDb == null)
                return NotFound();

            _context.Students.Remove(studentInDb);

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DBConcurrencyException)
            {
                ModelState.AddModelError("Database Error", "Unable delete this students. Please, try again. " +
                    "If problem persists, please, contact system adminstrator");
            }

            return View(studentInDb);
        }
    }
}
