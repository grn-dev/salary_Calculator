using SalaryCalculator.WebApi.Application.Commands;

namespace SalaryCalculator.WebApi.Adapter;

public abstract class ISalaryEmployeeAdapter
{
    protected ISalaryEmployeeAdapter(string inputData)
    {
        this.inputData = inputData;
    }

    public string inputData { get; set; }
    public abstract CreateSalaryEmployeeCommand AdapteToCreateCommand();
}
 