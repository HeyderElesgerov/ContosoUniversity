using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Configurations
{
    public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder
                .Property(p => p.FirstName)
                .IsRequired();

            builder
                .Property(p => p.LastName)
                .IsRequired();

            builder
                .Property(p => p.HireDate)
                .IsRequired();

            builder
                .HasOne(p => p.OfficeAssignment)
                .WithOne(p => p.Instructor)
                .HasForeignKey<OfficeAssignment>(p => p.InstructorId);

            builder
                .HasMany(p => p.CourseAssignments)
                .WithOne(p => p.Instructor)
                .HasForeignKey(p => p.InstructorId);
        }
    }
}
