using MediatR;
using Microsoft.EntityFrameworkCore;
using SalaryCalculator.Core;
using SalaryCalculator.Core.Models;
using SalaryCalculator.WebApi.Infrastructure;

namespace SalaryCalculator.WebApi.Application.Commands;

public class CreateSalaryEmployeeCommandHandler : IRequestHandler<CreateSalaryEmployeeCommand>
{
    private readonly EmployeeContext _employeeContext;

    private readonly OverTimeServiceFactory _overTimeServiceFactory;


    public CreateSalaryEmployeeCommandHandler(EmployeeContext employeeContext,
        OverTimeServiceFactory overTimeServiceFactory)
    {
        _employeeContext = employeeContext;
        _overTimeServiceFactory = overTimeServiceFactory;
    }

    public async Task Handle(CreateSalaryEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employeeRegistered = await _employeeContext.Employees.FirstOrDefaultAsync(x =>
                x.FirstName == request.FirstName && x.LastName == request.LastName,
            cancellationToken: cancellationToken);


        IOverTimeCalculator service = _overTimeServiceFactory.GetService(request.OverTimeCalculator);
        if (employeeRegistered is null)
        {
            var employee = new Employee()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
            };
            var salary = new Salary(request.BasicSalary, request.Allowance,
                request.Transportation, request.Date);
            salary.CalculateReceived(service);
            employee.addSalary(salary);
            await _employeeContext.Employees.AddAsync(employee, cancellationToken);
        }
        else
        {
            var sal = new Salary(request.BasicSalary, request.Allowance,
                request.Transportation, request.Date);
            employeeRegistered.addSalary(sal);
            sal.CalculateReceived(service);
        } 
        await _employeeContext.SaveChangesAsync(cancellationToken);
    }
}