using FluentValidation;

namespace SalaryCalculator.WebApi.Application.Validations;

public class UpdateSalaryEmployeeCommandValidator : AbstractValidator<UpdateSalaryEmployeeCommand>
{
    public UpdateSalaryEmployeeCommandValidator()
    {
        RuleFor(order => order.Date).NotEmpty();
        RuleFor(order => order.BasicSalary).NotEmpty();
        RuleFor(order => order.Transportation).NotEmpty();
        RuleFor(order => order.OverTimeCalculator).NotEmpty();
        RuleFor(order => order.Allowance).NotEmpty();
        RuleFor(order => order.SalaryId).NotEmpty();
    }
}