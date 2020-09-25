using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContosoUniversity.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder
                .Property(p => p.Name)
                .IsRequired();

            builder
                .Property(p => p.Budget)
                .IsRequired();

            builder.
                Property(p => p.RowVersion)
                .IsConcurrencyToken();

            builder
                .HasOne(p => p.Adminstrator)
                .WithMany(p => p.Departments)
                .HasForeignKey(p => p.AdminstratorId);
        }
    }
}
