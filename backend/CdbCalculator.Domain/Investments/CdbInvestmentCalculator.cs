namespace CdbCalculator.Domain.Investments;

public sealed class CdbInvestmentCalculator(ITaxRatePolicy taxRatePolicy) : ICdbInvestmentCalculator
{
    private const int MonetaryDecimals = 2;
    private const MidpointRounding RoundingMode = MidpointRounding.AwayFromZero;

    public CdbCalculationResult Calculate(Investment investment, CdbRates rates)
    {
        ArgumentNullException.ThrowIfNull(investment);
        ArgumentNullException.ThrowIfNull(rates);

        var grossAmount = CalculateCompoundAmount(investment, rates.MonthlyYieldRate);
        var grossProfit = grossAmount - investment.InitialAmount;
        var taxAmount = grossProfit * taxRatePolicy.GetRate(investment.Months);
        var netAmount = grossAmount - taxAmount;

        return new CdbCalculationResult(
            Round(grossAmount),
            Round(netAmount),
            Round(taxAmount),
            Round(grossProfit));
    }

    private static decimal CalculateCompoundAmount(Investment investment, decimal monthlyRate)
    {
        var factor = (decimal)Math.Pow((double)(1 + monthlyRate), investment.Months);
        return investment.InitialAmount * factor;
    }

    private static decimal Round(decimal value) => decimal.Round(value, MonetaryDecimals, RoundingMode);
}
