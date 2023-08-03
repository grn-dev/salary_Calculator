using MediatR;
using SalaryCalculator.WebApi.Models;

namespace SalaryCalculator.WebApi;

public class GetRangeSalaryEmployeeQueries : IRequest<List<GetSalaryEmployeeQueriesResult>>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FromDate { get; set; }
    public string ToDate { get; set; }
}