﻿using ContosoUniversity.Models;
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

        public async Task<IActionResult> Details(int id)
        {
            var course = await _context.Courses.AsNoTracking()
                                    .Where(c => c.ID == id)
                                    .Include(c => c.Department)
                                    .Include(p => p.Enrollments)
                                        .ThenInclude(p => p.Student)
                                    .Include(p => p.CourseAssignments)
                                        .ThenInclude(p => p.Instructor)
                                    .FirstOrDefaultAsync();

            if (course == null)
                return NotFound();

            return View(course);
        }

        public IActionResult Create()
        {
            var newCourseViewModel = new CreateCourseViewModel();

            newCourseViewModel.DepartmentsSelectListItems = new List<SelectListItem>();

            foreach (var department in _context.Departments)
            {
                var selectListItem = new SelectListItem()
                {
                    Text = department.Name,
                    Value = department.ID.ToString()
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
                    d => d.ID == viewModel.DepartmentId).FirstOrDefaultAsync();

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
                    Value = department.ID.ToString()
                };

                viewModel.DepartmentsSelectListItems.Add(selectListItem);
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var courseInDb = await _context.Courses
                                        .Include(c => c.Department)
                                        .Where(c => c.ID == id).FirstOrDefaultAsync();

            if (courseInDb == null)
                return NotFound();

            var viewModel = new EditCourseViewModel();
            viewModel.ID = id;
            viewModel.Title = courseInDb.Title;
            viewModel.Credits = courseInDb.Credits;

            if (courseInDb.Department != null)
                viewModel.DepartmentId = courseInDb.Department.ID;

            viewModel.DepartmentsSelectListItems = new List<SelectListItem>();

            foreach (var department in _context.Departments)
            {
                var selectListItem = new SelectListItem()
                {
                    Text = department.Name,
                    Value = department.ID.ToString(),
                    Selected = department.ID == viewModel.DepartmentId
                };

                viewModel.DepartmentsSelectListItems.Add(selectListItem);
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditCourseViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (id != viewModel.ID)
                    return BadRequest();

                var courseInDb = await _context.Courses.Where(c => c.ID == id).FirstOrDefaultAsync();

                if (courseInDb == null)
                    return NotFound();

                _context.Entry(courseInDb).State = EntityState.Modified;

                courseInDb.Title = viewModel.Title;
                courseInDb.Credits = viewModel.Credits;

                var departmentInDb = await _context.Departments.Where(d => d.ID == viewModel.DepartmentId).FirstOrDefaultAsync();

                if (departmentInDb != null)
                {
                    courseInDb.Department = departmentInDb;

                    try
                    {
                        _context.Update(courseInDb);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DBConcurrencyException)
                    {
                        ModelState.AddModelError("DatabaseError", "Cannot update course. Try again later.");
                    }
                }
            }

            viewModel.DepartmentsSelectListItems = new List<SelectListItem>();

            foreach (var department in _context.Departments)
            {
                var selectListItem = new SelectListItem()
                {
                    Text = department.Name,
                    Value = department.ID.ToString(),
                    Selected = department.ID == viewModel.DepartmentId
                };

                viewModel.DepartmentsSelectListItems.Add(selectListItem);
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var course = await _context.Courses
                                        .Include(p => p.Department)
                                        .Include(p => p.Enrollments)
                                            .ThenInclude(e => e.Student)
                                        .Include(p => p.CourseAssignments)
                                            .ThenInclude(p => p.Instructor)
                                        .Where(c => c.ID == id).FirstOrDefaultAsync();

            if (course == null)
                return NotFound();

            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, Course course)
        {
            if (course.ID != id)
                return BadRequest();

            try
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (DBConcurrencyException)
            {
                return View(course);
            }
        }
    }
}
