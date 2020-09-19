using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContosoUniversity.Models
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder
                .Property(p => p.CourseId)
                .IsRequired();

            builder
                .Property(p => p.StudentId)
                .IsRequired();

            builder
                .HasOne(p => p.Course)
                .WithMany(p => p.Enrollments)
                .HasForeignKey(p => p.CourseId);

            builder
                .HasOne(p => p.Student)
                .WithMany(p => p.Enrollments)
                .HasForeignKey(p => p.StudentId);

        }
    }
}
