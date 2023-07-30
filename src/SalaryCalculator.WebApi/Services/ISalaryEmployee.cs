namespace SalaryCalculator.WebApi.Services;

public interface ISalaryEmployee
{
    Task RegisterSalaryEmployee(RegisterSalaryEmployeeService salaryEmployee);
    Task GetSalaryEmployee(RegisterSalaryEmployeeService salaryEmployee);
    Task GetRangeSalaryEmployee(RegisterSalaryEmployeeService salaryEmployee);
    Task DeleteSalaryEmployee(RegisterSalaryEmployeeService salaryEmployee);
    Task UpdateSalaryEmployee(RegisterSalaryEmployeeService salaryEmployee);
}