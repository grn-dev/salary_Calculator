using MediatR;

namespace SalaryCalculator.WebApi;

public class DeleteSalaryCommand : IRequest
{
    public int SalaryId { get; set; }
}