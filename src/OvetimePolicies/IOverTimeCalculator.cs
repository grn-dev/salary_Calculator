namespace SalaryCalculator.Core;

public interface IOverTimeCalculator
{
    public string OverTimeCalculatorType { get; }
    public decimal calculateOverTime(decimal basicSalary, decimal allowance);
}