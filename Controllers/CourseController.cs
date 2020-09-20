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

            newCourseViewModel.DepartmentsSelectList = new List<SelectListItem>();

            foreach (var department in _context.Departments)
            {
                var selectListItem = new SelectListItem()
                {
                    Text = department.Name,
                    Value = department.DepartmentId.ToString()
                };
                newCourseViewModel.DepartmentsSelectList.Add(selectListItem);
            }

            return View(newCourseViewModel);
        }
    }
}
