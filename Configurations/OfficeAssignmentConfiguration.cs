using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContosoUniversity.Configurations
{
    public class OfficeAssignmentConfiguration : IEntityTypeConfiguration<OfficeAssignment>
    {
        public void Configure(EntityTypeBuilder<OfficeAssignment> builder)
        {
            builder
                .HasKey(p => p.InstructorId);

            builder
                .Property(p => p.Location)
                .IsRequired();

            builder
                .HasOne(p => p.Instructor)
                .WithOne(p => p.OfficeAssignment)
                .HasForeignKey<OfficeAssignment>(p => p.InstructorId);
        }
    }
}
