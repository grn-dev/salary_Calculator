using SalaryCalculator.WebApi.Application.Commands;

namespace SalaryCalculator.WebApi.Adapter;

public class CsAdapter: ISalaryEmployeeAdapter
{
    public CsAdapter(string inputData) : base(inputData)
    {
    }

    public override CreateSalaryEmployeeCommand AdapteToCreateCommand()
    {
        throw new NotImplementedException();
    }
}