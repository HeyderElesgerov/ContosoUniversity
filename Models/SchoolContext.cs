using ContosoUniversity.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Models
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<CourseAssignment> CourseAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Student>(new StudentConfiguration());
            modelBuilder.ApplyConfiguration<Course>(new CourseConfiguration());
            modelBuilder.ApplyConfiguration<Enrollment>(new EnrollmentConfiguration());
            modelBuilder.ApplyConfiguration<Department>(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration<Instructor>(new InstructorConfiguration());
            modelBuilder.ApplyConfiguration<OfficeAssignment>(new OfficeAssignmentConfiguration());
            modelBuilder.ApplyConfiguration<CourseAssignment>(new CourseAssignmentConfiguration());
        }
    }
}
