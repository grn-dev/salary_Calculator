using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalaryCalculator.Core.Models;

namespace SalaryCalculator.WebApi.Infrastructure.EntityConfigurations;

public class SalaryTypeConfiguration : IEntityTypeConfiguration<Salary>
{
    public void Configure(EntityTypeBuilder<Salary> builder)
    {
        builder.ToTable("Salaries");
        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Date).IsRequired().HasMaxLength(8);

        builder.Property(ci => ci.BasicSalary).HasPrecision(18, 4);
        builder.Property(ci => ci.Allowance).HasPrecision(18, 4);
        builder.Property(ci => ci.Transportation).HasPrecision(18, 4);
    }
}