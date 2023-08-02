namespace SalaryCalculator.Core;

public class OverTimeCalculatorC : IOverTimeCalculator
{
    public string OverTimeCalculatorType => "CalcurlatorC";

    public decimal calculateOverTime(decimal basicSalary, decimal allowance)
    {
        return basicSalary / 172;//how to calculate OverTime
    }
}