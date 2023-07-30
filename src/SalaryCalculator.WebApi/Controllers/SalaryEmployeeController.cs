using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> HttpPost(string datatype, RegisterSalaryEmployeeRequestDto employeeRequestDto)
    {
        EmployeeDto deserializeEmployee = new();
        switch (datatype)
        {
            case "json":
            {
                deserializeEmployee = JsonConvert.DeserializeObject<EmployeeDto>(employeeRequestDto.Data);
            }
                break;
            case "xml":
            {
                XmlSerializer serializer = new XmlSerializer(typeof(EmployeeDto));
                using (TextReader reader = new StringReader(employeeRequestDto.Data))
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
                deserializeEmployee = employeeRequestDto.Data.GetEmployee();
            }
                break;
            default:
                throw new ArgumentOutOfRangeException(datatype);
        }

        //todo persist employee
        if (deserializeEmployee is null)
            throw new NullReferenceException("can't desrila");

        var employeeRegistered = await _employeeContext.Employees.FirstOrDefaultAsync(x =>
            x.FirstName == deserializeEmployee.FirstName && x.LastName == deserializeEmployee.LastName);

        
        IOverTimeCalculator service = _overTimeServiceFactory.GetService(employeeRequestDto.OverTimeCalculator);
        if (employeeRegistered is null)
        {
            var emp = new Employee()
            {
                FirstName = deserializeEmployee.FirstName,
                LastName = deserializeEmployee.LastName,
            };
            var sal = new Salary()
            {
                BasicSalary = deserializeEmployee.BasicSalary,
                Allowance = deserializeEmployee.Allowance,
                Transportation = deserializeEmployee.Transportation,
                Date = deserializeEmployee.Date,
            };
            sal.CalculateReceived(service);
            emp.addSalary(sal);
            await _employeeContext.Employees.AddAsync(emp);
        }
        else
        {
            //todo change inject  
            
            
            var sal = new Salary()
            {
                BasicSalary = deserializeEmployee.BasicSalary,
                Allowance = deserializeEmployee.Allowance,
                Transportation = deserializeEmployee.Transportation,
                Date = deserializeEmployee.Date,
            };
            employeeRegistered.addSalary(sal);
            sal.CalculateReceived(service);
        }

        await _employeeContext.SaveChangesAsync();
        return Ok();
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}