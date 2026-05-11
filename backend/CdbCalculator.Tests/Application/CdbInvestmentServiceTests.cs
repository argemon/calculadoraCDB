using CdbCalculator.Application.Common.Settings;
using CdbCalculator.Application.Investments.Contracts;
using CdbCalculator.Application.Investments.Services;
using CdbCalculator.Domain.Investments;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;

namespace CdbCalculator.Tests.Application;

public sealed class CdbInvestmentServiceTests
{
    [Fact]
    public void Calculate_ShouldMapRequestToDomainCalculator_AndReturnResponse()
    {
        var calculator = new Mock<ICdbInvestmentCalculator>(MockBehavior.Strict);
        var settings = Options.Create(new CdbCalculatorSettings { Cdi = 0.009m, BankRate = 1.08m });
        var service = new CdbInvestmentService(calculator.Object, settings);
        var request = new CalculateCdbInvestmentRequest(1000m, 12);

        calculator
            .Setup(instance => instance.Calculate(
                It.Is<Investment>(investment => investment.InitialAmount == 1000m && investment.Months == 12),
                It.Is<CdbRates>(rates => rates.Cdi == 0.009m && rates.BankRate == 1.08m)))
            .Returns(new CdbCalculationResult(1123.08m, 1098.46m, 24.62m, 123.08m));

        var response = service.Calculate(request);

        response.Should().BeEquivalentTo(new CdbInvestmentResponse(1123.08m, 1098.46m, 24.62m, 123.08m));
        calculator.VerifyAll();
    }
}
