namespace SalaryCalculator.Core;

public class OverTimeCalculatorB : IOverTimeCalculator
{
    public string OverTimeCalculatorType => "CalcurlatorB";

    public decimal calculateOverTime(decimal basicSalary, decimal allowance)
    {
        return basicSalary / 172;//how to calculate OverTime
    }
}