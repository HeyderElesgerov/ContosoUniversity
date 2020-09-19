using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContosoUniversity.Models
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder
                .Property(p => p.ID)
                .ValueGeneratedNever();

            builder
                .Property(p => p.Title)
                .IsRequired();

            builder
                .Property(p => p.Credits)
                .IsRequired();

            builder
                .HasMany(p => p.Enrollments)
                .WithOne(p => p.Course)
                .HasForeignKey(p => p.CourseId);

            builder
                .HasOne(p => p.Department)
                .WithMany(p => p.Courses);

            builder
                .HasMany(p => p.CourseAssignments)
                .WithOne(p => p.Course)
                .HasForeignKey(p => p.CourseId);
        }
    }
}
