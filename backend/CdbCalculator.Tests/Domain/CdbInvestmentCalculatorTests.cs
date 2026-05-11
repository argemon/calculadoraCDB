using CdbCalculator.Domain.Exceptions;
using CdbCalculator.Domain.Investments;
using FluentAssertions;

namespace CdbCalculator.Tests.Domain;

public sealed class CdbInvestmentCalculatorTests
{
    private static readonly CdbRates DefaultRates = new(0.009m, 1.08m);

    private readonly CdbInvestmentCalculator _calculator = new(new RegressiveIncomeTaxRatePolicy());

    [Theory]
    [InlineData(2, 0.225)]
    [InlineData(6, 0.225)]
    [InlineData(7, 0.20)]
    [InlineData(12, 0.20)]
    [InlineData(13, 0.175)]
    [InlineData(24, 0.175)]
    [InlineData(25, 0.15)]
    [InlineData(60, 0.15)]
    public void TaxPolicy_ShouldReturnExpectedRate_ForEachInvestmentTerm(int months, decimal expectedRate)
    {
        var policy = new RegressiveIncomeTaxRatePolicy();

        var rate = policy.GetRate(months);

        rate.Should().Be(expectedRate);
    }

    [Fact]
    public void Calculate_ShouldReturnGrossAmount_WithCompoundInterest()
    {
        var investment = new Investment(1000m, 12);

        var result = _calculator.Calculate(investment, DefaultRates);

        result.GrossAmount.Should().Be(1123.08m);
        result.GrossProfit.Should().Be(123.08m);
    }

    [Fact]
    public void Calculate_ShouldApplyTaxOnlyOnProfit()
    {
        var investment = new Investment(1000m, 12);

        var result = _calculator.Calculate(investment, DefaultRates);

        result.TaxAmount.Should().Be(24.62m);
        result.NetAmount.Should().Be(1098.47m);
    }

    [Fact]
    public void Calculate_ShouldRoundMonetaryValues_ToTwoDecimals()
    {
        var investment = new Investment(1234.56m, 8);

        var result = _calculator.Calculate(investment, DefaultRates);

        result.GrossAmount.Should().Be(decimal.Round(result.GrossAmount, 2));
        result.NetAmount.Should().Be(decimal.Round(result.NetAmount, 2));
        result.TaxAmount.Should().Be(decimal.Round(result.TaxAmount, 2));
        result.GrossProfit.Should().Be(decimal.Round(result.GrossProfit, 2));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void Investment_ShouldRejectInvalidInitialAmount(decimal initialAmount)
    {
        var action = () => new Investment(initialAmount, 12);

        action.Should().Throw<InvalidInvestmentException>()
            .WithMessage("Investment initial amount must be greater than zero.");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(-5)]
    public void Investment_ShouldRejectInvalidMonths(int months)
    {
        var action = () => new Investment(1000m, months);

        action.Should().Throw<InvalidInvestmentException>()
            .WithMessage("Investment term must be greater than one month.");
    }
}
