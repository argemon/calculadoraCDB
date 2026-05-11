using CdbCalculator.Application.Common.Settings;
using CdbCalculator.Application.Investments.Contracts;
using CdbCalculator.Domain.Investments;
using Microsoft.Extensions.Options;

namespace CdbCalculator.Application.Investments.Services;

public sealed class CdbInvestmentService(
    ICdbInvestmentCalculator calculator,
    IOptions<CdbCalculatorSettings> settings) : ICdbInvestmentService
{
    public CdbInvestmentResponse Calculate(CalculateCdbInvestmentRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var investment = new Investment(request.InitialAmount, request.Months);
        var rates = new CdbRates(settings.Value.Cdi, settings.Value.BankRate);
        var result = calculator.Calculate(investment, rates);

        return new CdbInvestmentResponse(
            result.GrossAmount,
            result.NetAmount,
            result.TaxAmount,
            result.GrossProfit);
    }
}
