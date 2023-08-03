using MediatR;
using Microsoft.EntityFrameworkCore;
using SalaryCalculator.WebApi.Infrastructure;

namespace SalaryCalculator.WebApi;

public class DeleteSalaryCommandHandler : IRequestHandler<DeleteSalaryCommand>
{
    private readonly EmployeeContext _employeeContext;

    public DeleteSalaryCommandHandler(EmployeeContext employeeContext)
    {
        _employeeContext = employeeContext;
    }

    public async Task Handle(DeleteSalaryCommand request, CancellationToken cancellationToken)
    {
        var salary = await _employeeContext.Salaries.FirstOrDefaultAsync(x => x.Id == request.SalaryId,
            cancellationToken: cancellationToken);
        if (salary is null)
            throw new Exception("Not Found");
        _employeeContext.Salaries.Remove(salary);
        await _employeeContext.SaveChangesAsync(cancellationToken);
    }
}