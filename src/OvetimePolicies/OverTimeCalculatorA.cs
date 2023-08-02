namespace SalaryCalculator.Core;

public class OverTimeCalculatorA : IOverTimeCalculator
{
    public string OverTimeCalculatorType => "CalcurlatorA";


    public decimal calculateOverTime(decimal basicSalary, decimal allowance)
    {
        return basicSalary / 172; //how to calculate OverTime 
    }
}