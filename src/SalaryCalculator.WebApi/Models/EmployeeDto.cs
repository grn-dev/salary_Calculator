using System.Xml.Serialization;

namespace SalaryCalculator.WebApi.Models;

[XmlRoot(ElementName = "root")]
public class EmployeeDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal BasicSalary { get; set; }
    public decimal Allowance { get; set; }
    public decimal Transportation { get; set; }
    public string Date { get; set; }
}

public class EmployeeDtoQuery
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int SalaryId { get; set; }
    public decimal BasicSalary { get; set; }
    public decimal Allowance { get; set; }
    public decimal Transportation { get; set; }
    public string Date { get; set; }
    public string ReceivedSalary { get; set; }
}