namespace SalaryCalculator.Core;

public class OverTimeServiceFactory
{
    private readonly IEnumerable<IOverTimeCalculator> _overTimeCalculators;

    public OverTimeServiceFactory(IEnumerable<IOverTimeCalculator> timeCalculators)
    {
        _overTimeCalculators = timeCalculators;
    }

    public IOverTimeCalculator GetService(string calculatorType)
    {
        return _overTimeCalculators.FirstOrDefault(e => e.OverTimeCalculatorType == calculatorType)
               ?? throw new NotSupportedException();
    }
}