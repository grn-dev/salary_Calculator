namespace SalaryCalculator.Core.Models;

public class Salary
{
    public Salary()
    {
    }

    public Salary(decimal basicSalary, decimal allowance, decimal transportation, string date)
    {
        BasicSalary = basicSalary;
        Allowance = allowance;
        Transportation = transportation;
        Date = date;
    }

    public int Id { get; set; }
    public decimal BasicSalary { get; private set; }
    public decimal Allowance { get; private set; }
    public decimal Transportation { get; private set; }
    public string Date { get; private set; }
    public decimal ReceivedSalary { get; private set; }

    private decimal CalculatorTax() => BasicSalary / 10; //how to calculate

    public void CalculateReceived(IOverTimeCalculator service)
    {
        var overTime = service.calculateOverTime(BasicSalary, Allowance);
        ReceivedSalary = (BasicSalary + Allowance + Transportation + overTime) - CalculatorTax();
    }

    public void Update(decimal basicSalary, decimal allowance, decimal transportation, string date)
    {
        BasicSalary = basicSalary;
        Allowance = allowance;
        Transportation = transportation;
        Date = date;
    }
}