using FluentValidation;
using SalaryCalculator.WebApi.Application.Commands;

namespace SalaryCalculator.WebApi.Application.Validations;

public class CreateSalaryEmployeeCommandValidator : AbstractValidator<CreateSalaryEmployeeCommand>
{
    public CreateSalaryEmployeeCommandValidator()
    {
        RuleFor(order => order.Date).NotEmpty();
        RuleFor(order => order.FirstName).NotEmpty();
        RuleFor(order => order.LastName).NotEmpty();
        RuleFor(order => order.BasicSalary).NotEmpty();
        RuleFor(order => order.Transportation).NotEmpty();
        RuleFor(order => order.OverTimeCalculator).NotEmpty();
        RuleFor(order => order.Allowance).NotEmpty();
    }
}