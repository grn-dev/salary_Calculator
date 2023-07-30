using SalaryCalculator.WebApi.Models;

namespace SalaryCalculator.WebApi.Controllers;

public static class CustomExtension
{
    public static EmployeeDto GetEmployee(this string input)
    {
        var split = input.Split("/");
        var emp = new EmployeeDto()
        {
            FirstName = split[0],
            LastName = split[1],
            BasicSalary = decimal.TryParse(split[2], out decimal bs) ? bs : 0,
            Allowance = decimal.TryParse(split[3], out decimal a) ? a : 0,
            Transportation = decimal.TryParse(split[4], out decimal tt) ? tt : 0,
            Date = split[5],
        };
        return emp;
    }
}