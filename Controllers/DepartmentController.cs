using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ContosoUniversity.Controllers
{
    public class DepartmentController : Controller
    {
        private SchoolContext _context;

        public DepartmentController(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var departments = await _context.Departments
                                    .Include(p => p.Adminstrator)
                                    .AsNoTracking()
                                    .ToListAsync();
            return View(departments);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var department = await _context.Departments
                                            .FirstOrDefaultAsync(d => d.ID == id);

            if (department == null)
                return NotFound();

            ViewData["Adminstrators"] = await PopulateInstructors();
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Department department)
        {
            var departmentInDb = await _context.Departments
                                               .FirstOrDefaultAsync(d => d.ID == department.ID);

            if (departmentInDb == null)
            {
                var deletedDepartment = new Department();
                await TryUpdateModelAsync(deletedDepartment);

                ViewData["Adminstrators"] = await PopulateInstructors();
                ModelState.AddModelError("", "This department not found. It may be delete by another user.");
                return View(deletedDepartment);
            }

            _context.Entry(departmentInDb).Property(p => p.RowVersion).OriginalValue = department.RowVersion;

            if (await TryUpdateModelAsync(
                    departmentInDb,
                    "",
                    m => m.Name, m => m.Budget, m => m.StartDate, m => m.AdminstratorId))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var thrownEntry = ex.Entries.Single();
                    var userValues = (Department)thrownEntry.Entity;
                    var dbEntry = thrownEntry.GetDatabaseValues();

                    if (dbEntry == null)
                    {
                        ModelState.AddModelError("", "This Department was deleted by another user");
                    }
                    else
                    {
                        var dbValues = (Department)dbEntry.ToObject();

                        if (dbValues.Name != userValues.Name)
                            ModelState.AddModelError("Name", $"Current name: {dbValues.Name}");

                        if (dbValues.Budget != userValues.Budget)
                            ModelState.AddModelError("Budget", $"Current budget: {dbValues.Budget:c}");

                        if (dbValues.StartDate != userValues.StartDate)
                            ModelState.AddModelError("StartDate", $"Current date: {dbValues.StartDate:d}");

                        var adminstratorSelectedByUser = await _context.Instructors
                                                                       .FirstOrDefaultAsync(
                                                                             a => a.ID == userValues.AdminstratorId);
                        var adminstratorInDb = await _context.Instructors
                                                             .FirstOrDefaultAsync(
                                                                a => a.ID == dbValues.AdminstratorId);

                        if (adminstratorSelectedByUser != adminstratorInDb)
                        {
                            if (adminstratorInDb != null)
                            {
                                ModelState.AddModelError("AdminstratorId", $"Current adminstrator: {adminstratorInDb.FirstName} {adminstratorInDb.LastName}");
                            }
                            else
                            {
                                ModelState.AddModelError("AdminstratorId", $"There is no adminstrator");
                            }
                        }

                        ModelState.AddModelError(string.Empty,
                           "The record you attempted to edit was modified by another user after you got the original value. The edit operation was canceled and the current values in the database have been displayed. If you still want to edit this record, click the Save button again. Otherwise click the Back to List hyperlink.");

                        departmentInDb.RowVersion = dbValues.RowVersion;
                        ModelState.Remove("RowVersion");
                    }
                }
            }
            ViewData["Adminstrators"] = await PopulateInstructors();
            return View(departmentInDb);

        }

        public async Task<IActionResult> Create()
        {
            ViewData["Adminstrators"] = await PopulateInstructors();
            return View(new Department() { StartDate = DateTime.Today });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                await _context.Departments.AddAsync(department);

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            ViewData["Adminstrators"] = await PopulateInstructors();
            return View(department);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var department = await _context.Departments
                                           .Include(p => p.Adminstrator)
                                           .FirstOrDefaultAsync(d => d.ID == id);

            if (department == null)
                return NotFound();

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Department department)
        {
            var departmentInDb = await _context.Departments.FirstOrDefaultAsync(d => d.ID == department.ID);

            if (departmentInDb == null)
            {
                ModelState.AddModelError("", "This department was deleted by another user");
                return View(department);
            }
            else
            {
                foreach (var course in _context.Courses.Include(c => c.Department)
                                                       .Where(c => c.Department.ID == department.ID))
                {
                    course.Department = null;
                }

                _context.Departments.Remove(departmentInDb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        private async Task<IEnumerable<SelectListItem>> PopulateInstructors()
        {
            var selectList = new List<SelectListItem>();

            foreach (var instructor in await _context.Instructors.ToListAsync())
            {
                selectList.Add(
                    new SelectListItem()
                    {
                        Text = instructor.FirstName + " " + instructor.LastName,
                        Value = instructor.ID.ToString()
                    });
            }

            return selectList;
        }
    }
}
