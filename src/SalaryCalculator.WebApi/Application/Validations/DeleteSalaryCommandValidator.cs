using FluentValidation;

namespace SalaryCalculator.WebApi.Application.Validations;

public class DeleteSalaryCommandValidator : AbstractValidator<DeleteSalaryCommand>
{
    public DeleteSalaryCommandValidator()
    {
        RuleFor(order => order.SalaryId).NotEmpty();
    }
}