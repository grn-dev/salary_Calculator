using MediatR;
using SalaryCalculator.WebApi.Models;

namespace SalaryCalculator.WebApi;

public class GetSalaryEmployeeQueries : IRequest<GetSalaryEmployeeQueriesResult>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Date { get; set; }
}