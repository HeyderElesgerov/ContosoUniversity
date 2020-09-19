using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Configurations
{
    public class CourseAssignmentConfiguration : IEntityTypeConfiguration<CourseAssignment>
    {
        public void Configure(EntityTypeBuilder<CourseAssignment> builder)
        {
            builder
                .HasKey(p => new { p.CourseId, p.InstructorId });

            builder
                .HasOne(p => p.Instructor)
                .WithMany(p => p.CourseAssignments)
                .HasForeignKey(p => p.InstructorId)
                .IsRequired();

            builder
                .HasOne(p => p.Course)
                .WithMany(p => p.CourseAssignments)
                .HasForeignKey(p => p.CourseId)
                .IsRequired();
        }
    }
}
