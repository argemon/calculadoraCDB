namespace CdbCalculator.Domain.Investments;

public sealed class RegressiveIncomeTaxRatePolicy : ITaxRatePolicy
{
    private const decimal UpToSixMonthsRate = 0.225m;
    private const decimal UpToTwelveMonthsRate = 0.20m;
    private const decimal UpToTwentyFourMonthsRate = 0.175m;
    private const decimal AboveTwentyFourMonthsRate = 0.15m;

    public decimal GetRate(int months) => months switch
    {
        <= 6 => UpToSixMonthsRate,
        <= 12 => UpToTwelveMonthsRate,
        <= 24 => UpToTwentyFourMonthsRate,
        _ => AboveTwentyFourMonthsRate
    };
}
