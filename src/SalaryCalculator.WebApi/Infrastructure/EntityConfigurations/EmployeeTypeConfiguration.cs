using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalaryCalculator.Core.Models;

namespace SalaryCalculator.WebApi.Infrastructure.EntityConfigurations;

public class EmployeeTypeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employee");

        builder.HasKey(ci => ci.Id);
        
        builder.Property(ci => ci.EmployeeCode)
            .IsRequired(false)
            .HasMaxLength(10);

        builder.Property(ci => ci.FirstName)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(ci => ci.LastName)
            .IsRequired()
            .HasMaxLength(70);
    }
}