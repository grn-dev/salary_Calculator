using Microsoft.EntityFrameworkCore;
using SalaryCalculator.Core.Models;
using SalaryCalculator.WebApi.Infrastructure.EntityConfigurations;

namespace SalaryCalculator.WebApi.Infrastructure;

public class EmployeeContext : DbContext
{
    public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Salary> Salaries { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new EmployeeTypeConfiguration());
        builder.ApplyConfiguration(new SalaryTypeConfiguration());
    }
}