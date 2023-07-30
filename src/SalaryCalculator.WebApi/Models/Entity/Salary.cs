namespace SalaryCalculator.Core.Models;

public class Salary
{
    public Salary()
    {
    }

    public int Id { get; set; }
    public decimal BasicSalary { get; set; }
    public decimal Allowance { get; set; }
    public decimal Transportation { get; set; }
    public string Date { get; set; }
    public decimal ReceivedSalary { get; set; }

    private decimal CalculatorTax() => BasicSalary / 10; //how to calculate

    public void CalculateReceived(IOverTimeCalculator service)
    {
        var overTime = service.calculateOverTime(BasicSalary, Allowance);
        ReceivedSalary = (BasicSalary + Allowance + Transportation + overTime) - CalculatorTax();
    }
}