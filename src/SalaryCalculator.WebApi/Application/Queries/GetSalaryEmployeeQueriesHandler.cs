using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using SalaryCalculator.WebApi.Models;

namespace SalaryCalculator.WebApi;

public class GetSalaryEmployeeQueriesHandler : IRequestHandler<GetSalaryEmployeeQueries, GetSalaryEmployeeQueriesResult>
{
    private string _connectionString = string.Empty;

    public GetSalaryEmployeeQueriesHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    public async Task<GetSalaryEmployeeQueriesResult> Handle(GetSalaryEmployeeQueries request,
        CancellationToken cancellationToken)
    { 
        var query = String.Format(
            @"SELECT   e.[Id] as EmployeeId ,[FirstName],[LastName],s.[Id] as SalaryId,[BasicSalary],[Allowance]
                ,[Transportation],[Date],[ReceivedSalary]
        FROM [EmployeeDB].[dbo].[Employee] e 
            inner join [EmployeeDB].[dbo].Salaries s on e.Id = s.EmployeeId
        where e.FirstName = '{0}' and e.LastName = '{1}' and s.Date = '{2}'", request.FirstName, request.LastName,
            request.Date);

        using (var con = new SqlConnection(_connectionString))
        {
            return con.Query<GetSalaryEmployeeQueriesResult>(query).FirstOrDefault(); 
        }
    }
}