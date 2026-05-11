namespace CdbCalculator.Domain.Investments;

public sealed record CdbRates(decimal Cdi, decimal BankRate)
{
    public decimal MonthlyYieldRate => Cdi * BankRate;
}
