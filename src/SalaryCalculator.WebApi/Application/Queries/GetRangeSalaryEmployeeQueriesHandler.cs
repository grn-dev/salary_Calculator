using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using SalaryCalculator.WebApi.Models;

namespace SalaryCalculator.WebApi;

public class GetRangeSalaryEmployeeQueriesHandler : IRequestHandler<GetRangeSalaryEmployeeQueries,
    List<GetSalaryEmployeeQueriesResult>>
{
    private string _connectionString = string.Empty;

    public GetRangeSalaryEmployeeQueriesHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<List<GetSalaryEmployeeQueriesResult>> Handle(GetRangeSalaryEmployeeQueries request,
        CancellationToken cancellationToken)
    {
        var query = String.Format(
            @"SELECT   e.[Id] as EmployeeId ,[FirstName],[LastName],s.[Id] as SalaryId,[BasicSalary],[Allowance],
                    [Transportation],[Date],[ReceivedSalary]
        FROM [EmployeeDB].[dbo].[Employee] e 
            inner join [EmployeeDB].[dbo].Salaries s on e.Id = s.EmployeeId
        where e.FirstName = '{0}' and e.LastName = '{1}' and s.Date >= '{2}'and s.Date <= '{3}'",
            request.FirstName,
            request.LastName,
            request.FromDate,
            request.ToDate);

        using (var con = new SqlConnection(_connectionString))
        {
            return con.Query<GetSalaryEmployeeQueriesResult>(query).ToList();
        }
    }
}