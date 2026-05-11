using CdbCalculator.Application.Investments.Contracts;
using CdbCalculator.Application.Investments.Validators;
using FluentAssertions;

namespace CdbCalculator.Tests.Application;

public sealed class CalculateCdbInvestmentRequestValidatorTests
{
    private readonly CalculateCdbInvestmentRequestValidator _validator = new();

    [Fact]
    public void Validate_ShouldAcceptValidRequest()
    {
        var result = _validator.Validate(new CalculateCdbInvestmentRequest(1000m, 2));

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_ShouldRejectInvalidInitialAmount(decimal initialAmount)
    {
        var result = _validator.Validate(new CalculateCdbInvestmentRequest(initialAmount, 12));

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(error => error.PropertyName == nameof(CalculateCdbInvestmentRequest.InitialAmount));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void Validate_ShouldRejectInvalidMonths(int months)
    {
        var result = _validator.Validate(new CalculateCdbInvestmentRequest(1000m, months));

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(error => error.PropertyName == nameof(CalculateCdbInvestmentRequest.Months));
    }
}
