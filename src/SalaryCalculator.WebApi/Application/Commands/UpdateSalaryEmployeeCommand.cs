using MediatR;

namespace SalaryCalculator.WebApi;

public class UpdateSalaryEmployeeCommand : IRequest
{
    public int SalaryId { get; set; }
    public decimal BasicSalary { get; set; }
    public decimal Allowance { get; set; }
    public decimal Transportation { get; set; }
    public string Date { get; set; }
    public string OverTimeCalculator { get; set; }
}