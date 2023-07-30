using SalaryCalculator.WebApi;

namespace SalaryCalculatoring.IntegrationTests.Extensions;

public static class TestDataExtensions
{
    public static RegisterSalaryEmployeeRequestDto GetRegisterSalaryEmployeeRequestDto()
    {
        return new RegisterSalaryEmployeeRequestDto()
        {
            Data = "Ali/Ahmadi/1200000/400000/350000/14010801",
            OverTimeCalculator = "CalcurlatorB"
        };
    }
}