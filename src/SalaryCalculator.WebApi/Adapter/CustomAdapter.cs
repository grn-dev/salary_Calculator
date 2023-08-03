using SalaryCalculator.WebApi.Application.Commands;

namespace SalaryCalculator.WebApi.Adapter;

public class CustomAdapter : ISalaryEmployeeAdapter
{
    public CustomAdapter(string inputData) : base(inputData)
    {
    }

    public override CreateSalaryEmployeeCommand AdapteToCreateCommand()
    {
        var split = inputData.Split("/");
        return new CreateSalaryEmployeeCommand()
        {
            FirstName = split[0],
            LastName = split[1],
            BasicSalary = decimal.TryParse(split[2], out decimal bs) ? bs : 0,
            Allowance = decimal.TryParse(split[3], out decimal a) ? a : 0,
            Transportation = decimal.TryParse(split[4], out decimal tt) ? tt : 0,
            Date = split[5],
        };
    }
}