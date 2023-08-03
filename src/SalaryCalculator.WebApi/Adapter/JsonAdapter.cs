using Newtonsoft.Json;
using SalaryCalculator.WebApi.Application.Commands;

namespace SalaryCalculator.WebApi.Adapter;

public class JsonAdapter : ISalaryEmployeeAdapter
{
    public JsonAdapter(string inputData) : base(inputData)
    {
    }

    public override CreateSalaryEmployeeCommand AdapteToCreateCommand()
    {
        return JsonConvert.DeserializeObject<CreateSalaryEmployeeCommand>(inputData);
    }
}