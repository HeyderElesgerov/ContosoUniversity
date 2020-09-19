using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContosoUniversity.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(p => p.EnrollmentDate)
                .IsRequired();

            builder
                .HasMany(p => p.Enrollments)
                .WithOne(p => p.Student)
                .HasForeignKey(p => p.StudentId);
        }
    }
}
