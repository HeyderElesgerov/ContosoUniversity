using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ContosoUniversity.Migrations;

namespace ContosoUniversity.Controllers
{
    public class CourseController : Controller
    {
        private SchoolContext _context;

        public CourseController(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses
                            .Include(p => p.Department)
                            .ToListAsync();

            return View(courses);
        }

        public IActionResult Create()
        {
            var newCourseViewModel = new CreateCourseViewModel();

            newCourseViewModel.DepartmentsSelectListItems = new List<SelectListItem>();

            foreach(var department in _context.Departments)
            {
                var selectListItem = new SelectListItem()
                {
                    Text = department.Name,
                    Value = department.DepartmentId.ToString()
                };

                newCourseViewModel.DepartmentsSelectListItems.Add(selectListItem);
            }

            return View(newCourseViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var newCourse = new Course();
                newCourse.ID = (new Random()).Next(1000, 9999);
                newCourse.Title = viewModel.Title;
                newCourse.Credits = viewModel.Credits;

                var departmentInDb = await _context.Departments.Where(
                    d => d.DepartmentId == viewModel.DepartmentId).FirstOrDefaultAsync();

                if (departmentInDb != null)
                {
                    newCourse.Department = departmentInDb;

                    try
                    {
                        await _context.Courses.AddAsync(newCourse);
                        await _context.SaveChangesAsync();

                        return RedirectToAction(nameof(Index));
                    }
                    catch (DBConcurrencyException)
                    {
                        ModelState.AddModelError("Database Error",
                            "Cannot insert to database. Try again later.");
                    }
                }
            }

            viewModel.DepartmentsSelectListItems = new List<SelectListItem>();

            foreach (var department in _context.Departments)
            {
                var selectListItem = new SelectListItem()
                {
                    Text = department.Name,
                    Value = department.DepartmentId.ToString()
                };

                viewModel.DepartmentsSelectListItems.Add(selectListItem);
            }

            return View(viewModel);
        }
    }
}
