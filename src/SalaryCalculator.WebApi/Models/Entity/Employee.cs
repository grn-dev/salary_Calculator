namespace SalaryCalculator.Core.Models;

public class Employee
{
    public int Id { get; set; }
    public string EmployeeCode { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Salary> Salaries { get; set; }

    public void addSalary(Salary salary)
    {
        (Salaries ??= new List<Salary>()).Add(salary);
        //Salaries.Add(salary);
    }
}