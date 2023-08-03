using MediatR;
using Microsoft.EntityFrameworkCore;
using SalaryCalculator.Core;
using SalaryCalculator.WebApi.Infrastructure;

namespace SalaryCalculator.WebApi;

public class UpdateSalaryEmployeeCommandHandler : IRequestHandler<UpdateSalaryEmployeeCommand>
{
    private readonly EmployeeContext _employeeContext;
    private readonly OverTimeServiceFactory _overTimeServiceFactory;

    public UpdateSalaryEmployeeCommandHandler(EmployeeContext employeeContext,
        OverTimeServiceFactory overTimeServiceFactory)
    {
        _employeeContext = employeeContext;
        _overTimeServiceFactory = overTimeServiceFactory;
    }

    public async Task Handle(UpdateSalaryEmployeeCommand request, CancellationToken cancellationToken)
    {
        var salary = await _employeeContext.Salaries.FirstOrDefaultAsync(x => x.Id == request.SalaryId,
            cancellationToken: cancellationToken);


        salary.Update(request.BasicSalary, request.Allowance,
            request.Transportation, request.Date);

        IOverTimeCalculator service = _overTimeServiceFactory.GetService(request.OverTimeCalculator);
        salary.CalculateReceived(service);

        await _employeeContext.SaveChangesAsync(cancellationToken);
    }
}