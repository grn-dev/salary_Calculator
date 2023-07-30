namespace SalaryCalculator.Core;

public class OverTimeServiceFactory
{
    private readonly IEnumerable<IOverTimeCalculator> _overTimeCalculators;

    public OverTimeServiceFactory(IEnumerable<IOverTimeCalculator> timeCalculators)
    {
        _overTimeCalculators = timeCalculators;
    }

    public IOverTimeCalculator GetService(string relayMode)
    {
        return _overTimeCalculators.FirstOrDefault(e => e.OverTimeCalculatorType == relayMode)
               ?? throw new NotSupportedException();
    }
}