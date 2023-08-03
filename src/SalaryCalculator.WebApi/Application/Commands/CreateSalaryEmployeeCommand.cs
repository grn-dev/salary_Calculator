using System.Xml.Serialization;
using MediatR;

namespace SalaryCalculator.WebApi.Application.Commands;

[XmlRoot(ElementName = "root")]
public class CreateSalaryEmployeeCommand : IRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal BasicSalary { get; set; }
    public decimal Allowance { get; set; }
    public decimal Transportation { get; set; }
    public string Date { get; set; }
    public string OverTimeCalculator { get; set; }
}
