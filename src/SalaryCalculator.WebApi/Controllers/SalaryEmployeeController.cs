using System.Xml.Serialization;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SalaryCalculator.Core;
using SalaryCalculator.Core.Models;
using SalaryCalculator.WebApi.Infrastructure;
using SalaryCalculator.WebApi.Models;
using SalaryCalculator.WebApi.Services;

namespace SalaryCalculator.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SalaryEmployeeController : ControllerBase
{
    private readonly ISalaryEmployee _salaryEmployee;
    private readonly EmployeeContext _employeeContext;
    private readonly OverTimeServiceFactory _overTimeServiceFactory;


    public SalaryEmployeeController(ISalaryEmployee salaryEmployee, EmployeeContext employeeContext,
        OverTimeServiceFactory overTimeServiceFactory)
    {
        _salaryEmployee = salaryEmployee;
        _employeeContext = employeeContext;
        _overTimeServiceFactory = overTimeServiceFactory;
    }

    [HttpPost]
    [Route("{datatype}/[controller]")]
    public async Task<IActionResult> HttpPost(string datatype, RegisterSalaryEmployeeRequest employeeRequest)
    {
        EmployeeDto deserializeEmployee = new();
        switch (datatype)
        {
            case "json":
            {
                deserializeEmployee = JsonConvert.DeserializeObject<EmployeeDto>(employeeRequest.Data);
            }
                break;
            case "xml":
            {
                XmlSerializer serializer = new XmlSerializer(typeof(EmployeeDto));
                using (TextReader reader = new StringReader(employeeRequest.Data))
                {
                    deserializeEmployee = (EmployeeDto)serializer.Deserialize(reader);
                }
            }
                break;
            case "cs":
            {
                //todo what cs
            }
                break;
            case "custom":
            {
                deserializeEmployee = employeeRequest.Data.GetEmployee();
            }
                break;
            default:
                throw new ArgumentOutOfRangeException(datatype);
        }


        var employeeRegistered = await _employeeContext.Employees.FirstOrDefaultAsync(x =>
            x.FirstName == deserializeEmployee.FirstName && x.LastName == deserializeEmployee.LastName);


        IOverTimeCalculator service = _overTimeServiceFactory.GetService(employeeRequest.OverTimeCalculator);
        if (employeeRegistered is null)
        {
            var emp = new Employee()
            {
                FirstName = deserializeEmployee.FirstName,
                LastName = deserializeEmployee.LastName,
            };
            var sal = new Salary(deserializeEmployee.BasicSalary, deserializeEmployee.Allowance,
                deserializeEmployee.Transportation, deserializeEmployee.Date);
            sal.CalculateReceived(service);
            emp.addSalary(sal);
            await _employeeContext.Employees.AddAsync(emp);
        }
        else
        {
            //todo change inject  


            var sal = new Salary(deserializeEmployee.BasicSalary, deserializeEmployee.Allowance,
                deserializeEmployee.Transportation, deserializeEmployee.Date);
            employeeRegistered.addSalary(sal);
            sal.CalculateReceived(service);
        }

        await _employeeContext.SaveChangesAsync();
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetSalaryEmployeeRequest request)
    {
        var connectiStrin =
            "Server=(localdb)\\mssqllocaldb;Database=EmployeeDB;Trusted_Connection=True;MultipleActiveResultSets=true";
        var query = String.Format(
            @"SELECT   e.[Id] as EmployeeId ,[FirstName],[LastName],s.[Id] as SalaryId,[BasicSalary],[Allowance],[Transportation],[Date],[ReceivedSalary]
        FROM [EmployeeDB].[dbo].[Employee] e 
            inner join [EmployeeDB].[dbo].Salaries s on e.Id = s.EmployeeId
        where e.FirstName = '{0}' and e.LastName = '{1}' and s.Date = '{2}'", request.FirstName, request.LastName,
            request.Date);

        using (var con = new SqlConnection(connectiStrin))
        {
            var employeeDtoQuery = con.Query<EmployeeDtoQuery>(query).FirstOrDefault();
            return Ok(employeeDtoQuery);
        }
    }

    [HttpGet("Range")]
    public IActionResult Get([FromQuery] GetRangeSalaryEmployeeRequest request)
    {
        var connectiStrin =
            "Server=(localdb)\\mssqllocaldb;Database=EmployeeDB;Trusted_Connection=True;MultipleActiveResultSets=true";
        var query = String.Format(
            @"SELECT   e.[Id] as EmployeeId ,[FirstName],[LastName],s.[Id] as SalaryId,[BasicSalary],[Allowance],[Transportation],[Date],[ReceivedSalary]
        FROM [EmployeeDB].[dbo].[Employee] e 
            inner join [EmployeeDB].[dbo].Salaries s on e.Id = s.EmployeeId
        where e.FirstName = '{0}' and e.LastName = '{1}' and s.Date >= '{2}'and s.Date <= '{3}'",
            request.FirstName,
            request.LastName,
            request.FromDate,
            request.ToDate);

        using (var con = new SqlConnection(connectiStrin))
        {
            var employeeDtoQueries = con.Query<EmployeeDtoQuery>(query).ToList();
            return Ok(employeeDtoQueries);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateSalaryEmployeeRequest request)
    {
        var sss = await _employeeContext.Salaries.FirstOrDefaultAsync(x => x.Id == request.SalaryId);


        sss.Update(request.BasicSalary, request.Allowance,
            request.Transportation, request.Date);

        IOverTimeCalculator service = _overTimeServiceFactory.GetService(request.OverTimeCalculator);
        sss.CalculateReceived(service);

        await _employeeContext.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{salaryId}")]
    public IActionResult Delete(int salaryId)
    {
        Salary s = new() { Id = salaryId };
        _employeeContext.Salaries.Attach(s);
        _employeeContext.Salaries.Remove(s);
        _employeeContext.SaveChanges();
        return Ok();
    }
}