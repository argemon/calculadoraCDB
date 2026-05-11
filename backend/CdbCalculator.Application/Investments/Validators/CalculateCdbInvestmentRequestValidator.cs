using CdbCalculator.Application.Investments.Contracts;
using FluentValidation;

namespace CdbCalculator.Application.Investments.Validators;

public sealed class CalculateCdbInvestmentRequestValidator : AbstractValidator<CalculateCdbInvestmentRequest>
{
    public CalculateCdbInvestmentRequestValidator()
    {
        RuleFor(request => request.InitialAmount)
            .GreaterThan(0)
            .WithMessage("O valor inicial deve ser maior que zero.");

        RuleFor(request => request.Months)
            .GreaterThan(1)
            .WithMessage("O prazo deve ser maior que 1 mes.");
    }
}
