using System;
using System.Collections.Generic;
using System.Linq;

namespace ContosoUniversity.Models
{
    public static class SeedData
    {
        private static SchoolContext _context;

        public static void Seed(SchoolContext context)
        {
            _context = context;

            if (_context.Students.Any())
                return;

            SeedStudents();
            SeedInstructors();
            SeedDepartments();
            SeedCourses();
            SeedOfficeAssignments();
            SeedCourseAssignments();
            SeedEnrollments();
        }

        public static void SeedStudents()
        {
            _context.Students.AddRange(
                    new Student
                    {
                        FirstName = "Carson",
                        LastName = "Alexander",
                        EnrollmentDate = DateTime.Parse("2010-09-01")
                    },
                    new Student
                    {
                        FirstName = "Meredith",
                        LastName = "Alonso",
                        EnrollmentDate = DateTime.Parse("2012-09-01")
                    },
                    new Student
                    {
                        FirstName = "Arturo",
                        LastName = "Anand",
                        EnrollmentDate = DateTime.Parse("2013-09-01")
                    },
                    new Student
                    {
                        FirstName = "Gytis",
                        LastName = "Barzdukas",
                        EnrollmentDate = DateTime.Parse("2012-09-01")
                    },
                    new Student
                    {
                        FirstName = "Yan",
                        LastName = "Li",
                        EnrollmentDate = DateTime.Parse("2012-09-01")
                    },
                    new Student
                    {
                        FirstName = "Peggy",
                        LastName = "Justice",
                        EnrollmentDate = DateTime.Parse("2011-09-01")
                    },
                    new Student
                    {
                        FirstName = "Laura",
                        LastName = "Norman",
                        EnrollmentDate = DateTime.Parse("2013-09-01")
                    },
                    new Student
                    {
                        FirstName = "Nino",
                        LastName = "Olivetto",
                        EnrollmentDate = DateTime.Parse("2005-09-01")
                    }
               );

            _context.SaveChanges();
        }

        public static void SeedInstructors()
        {
            _context.Instructors.AddRange(
                    new Instructor
                    {
                        FirstName = "Kim",
                        LastName = "Abercrombie",
                        HireDate = DateTime.Parse("1995-03-11")
                    },
                    new Instructor
                    {
                        FirstName = "Fadi",
                        LastName = "Fakhouri",
                        HireDate = DateTime.Parse("2002-07-06")
                    },
                    new Instructor
                    {
                        FirstName = "Roger",
                        LastName = "Harui",
                        HireDate = DateTime.Parse("1998-07-01")
                    },
                    new Instructor { 
                        FirstName = "Candace", 
                        LastName = "Kapoor", 
                        HireDate = DateTime.Parse("2001-01-15") },
                    new Instructor { 
                        FirstName = "Roger", 
                        LastName = "Zheng", 
                        HireDate = DateTime.Parse("2004-02-12") });

            _context.SaveChanges();
        }

        public static void SeedDepartments()
        {
            _context.Departments.AddRange(
                    new Department
                    {
                        Name = "English",
                        Budget = 350000,
                        StartDate = DateTime.Parse("2007-09-01"),
                        AdminstratorId = _context.Instructors.Single(i => i.LastName == "Abercrombie").ID
                    },
                    new Department
                    {
                        Name = "Mathematics",
                        Budget = 100000,
                        StartDate = DateTime.Parse("2007-09-01"),
                        AdminstratorId = _context.Instructors.Single(i => i.LastName == "Fakhouri").ID
                    },
                    new Department
                    {
                        Name = "Engineering",
                        Budget = 350000,
                        StartDate = DateTime.Parse("2007-09-01"),
                        AdminstratorId = _context.Instructors.Single(i => i.LastName == "Harui").ID
                    },
                    new Department
                    {
                        Name = "Economics",
                        Budget = 100000,
                        StartDate = DateTime.Parse("2007-09-01"),
                        AdminstratorId = _context.Instructors.Single(i => i.LastName == "Kapoor").ID
                    });

            _context.SaveChanges();
        }

        public static void SeedCourses()
        {
            _context.Courses.AddRange(
                new Course { ID = 1050, Title = "Chemistry", Credits = 3 },
                new Course { ID = 4022, Title = "Microeconomics", Credits = 3 },
                new Course { ID = 4041, Title = "Macroeconomics", Credits = 3 },
                new Course { ID = 1045, Title = "Calculus", Credits = 4 },
                new Course { ID = 3141, Title = "Trigonometry", Credits = 4 },
                new Course { ID = 2021, Title = "Composition", Credits = 3 },
                new Course { ID = 2042, Title = "Literature", Credits = 4 });

            _context.SaveChanges();
        }

        public static void SeedOfficeAssignments()
        {
            _context.OfficeAssignments.AddRange(
                new OfficeAssignment
                {
                    InstructorId = _context.Instructors.Single(i => i.LastName == "Fakhouri").ID,
                    Location = "Smith 17"
                },
                new OfficeAssignment
                {
                    InstructorId = _context.Instructors.Single(i => i.LastName == "Harui").ID,
                    Location = "Gowan 27"
                },
                new OfficeAssignment
                {
                    InstructorId = _context.Instructors.Single(i => i.LastName == "Kapoor").ID,
                    Location = "Thompson 304"
                }
                );

            _context.SaveChanges();
        }

        public static void SeedCourseAssignments()
        {
            _context.CourseAssignments.AddRange(
                   new CourseAssignment()
                   {
                       CourseId = _context.Courses.Single(c => c.Title == "Chemistry").ID,
                       InstructorId = _context.Instructors.Single(i => i.LastName == "Kapoor").ID
                   },
                   new CourseAssignment()
                   {
                       CourseId = _context.Courses.Single(c => c.Title == "Chemistry").ID,
                       InstructorId = _context.Instructors.Single(i => i.LastName == "Harui").ID
                   },
                   new CourseAssignment()
                   {
                       CourseId = _context.Courses.Single(c => c.Title == "Microeconomics").ID,
                       InstructorId = _context.Instructors.Single(i => i.LastName == "Zheng").ID
                   },
                   new CourseAssignment()
                   {
                       CourseId = _context.Courses.Single(c => c.Title == "Macroeconomics").ID,
                       InstructorId = _context.Instructors.Single(i => i.LastName == "Zheng").ID
                   },
                   new CourseAssignment()
                   {
                       CourseId = _context.Courses.Single(c => c.Title == "Calculus").ID,
                       InstructorId = _context.Instructors.Single(i => i.LastName == "Fakhouri").ID
                   },
                   new CourseAssignment()
                   {
                       CourseId = _context.Courses.Single(c => c.Title == "Trigonometry").ID,
                       InstructorId = _context.Instructors.Single(i => i.LastName == "Harui").ID
                   },
                   new CourseAssignment()
                   {
                       CourseId = _context.Courses.Single(c => c.Title == "Composition").ID,
                       InstructorId = _context.Instructors.Single(i => i.LastName == "Abercrombie").ID
                   },
                   new CourseAssignment()
                   {
                       CourseId = _context.Courses.Single(c => c.Title == "Literature").ID,
                       InstructorId = _context.Instructors.Single(i => i.LastName == "Abercrombie").ID
                   }
                   );

            _context.SaveChanges();
        }

        public static void SeedEnrollments()
        {
            var enrollemnts = new List<Enrollment>()
            {
                new Enrollment
                   {
                       StudentId = _context.Students.FirstOrDefault(s => s.LastName == "Alexander").ID,
                       CourseId = _context.Courses.FirstOrDefault(c => c.Title == "Chemistry").ID,
                       Grade = Grade.A
                   },
                   new Enrollment
                   {
                       StudentId = _context.Students.FirstOrDefault(s => s.LastName == "Alexander").ID,
                       CourseId = _context.Courses.FirstOrDefault(c => c.Title == "Microeconomics").ID,
                       Grade = Grade.C
                   },
                   new Enrollment
                   {
                       StudentId = _context.Students.FirstOrDefault(s => s.LastName == "Alexander").ID,
                       CourseId = _context.Courses.FirstOrDefault(c => c.Title == "Macroeconomics").ID,
                       Grade = Grade.B
                   },
                   new Enrollment
                   {
                       StudentId = _context.Students.FirstOrDefault(s => s.LastName == "Alonso").ID,
                       CourseId = _context.Courses.FirstOrDefault(c => c.Title == "Calculus").ID,
                       Grade = Grade.B
                   },
                   new Enrollment
                   {
                       StudentId = _context.Students.FirstOrDefault(s => s.LastName == "Alonso").ID,
                       CourseId = _context.Courses.FirstOrDefault(c => c.Title == "Trigonometry").ID,
                       Grade = Grade.B
                   },
                   new Enrollment
                   {
                       StudentId = _context.Students.FirstOrDefault(s => s.LastName == "Alonso").ID,
                       CourseId = _context.Courses.FirstOrDefault(c => c.Title == "Composition").ID,
                       Grade = Grade.B
                   },
                   new Enrollment
                   {
                       StudentId = _context.Students.FirstOrDefault(s => s.LastName == "Anand").ID,
                       CourseId = _context.Courses.FirstOrDefault(c => c.Title == "Chemistry").ID
                   },
                   new Enrollment
                   {
                       StudentId = _context.Students.FirstOrDefault(s => s.LastName == "Anand").ID,
                       CourseId = _context.Courses.FirstOrDefault(c => c.Title == "Microeconomics").ID,
                       Grade = Grade.B
                   },
                   new Enrollment
                   {
                       StudentId = _context.Students.FirstOrDefault(s => s.LastName == "Barzdukas").ID,
                       CourseId = _context.Courses.FirstOrDefault(c => c.Title == "Chemistry").ID,
                       Grade = Grade.B
                   },
                   new Enrollment
                   {
                       StudentId = _context.Students.FirstOrDefault(s => s.LastName == "Li").ID,
                       CourseId = _context.Courses.FirstOrDefault(c => c.Title == "Composition").ID,
                       Grade = Grade.B
                   },
                   new Enrollment
                   {
                       StudentId = _context.Students.FirstOrDefault(s => s.LastName == "Justice").ID,
                       CourseId = _context.Courses.FirstOrDefault(c => c.Title == "Literature").ID,
                       Grade = Grade.B
                   }
            };

            foreach(var enrollemnt in enrollemnts)
            {
                var enrollmentInDb = _context.Enrollments.Where(
                    e => e.StudentId == enrollemnt.StudentId &&
                         e.CourseId == enrollemnt.CourseId).FirstOrDefault();

                if(enrollmentInDb == null)
                {
                    _context.Enrollments.Add(enrollemnt);
                }
            }

            _context.SaveChanges();
        }
    }
}
