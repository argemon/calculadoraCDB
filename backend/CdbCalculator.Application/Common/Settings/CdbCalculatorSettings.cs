namespace CdbCalculator.Application.Common.Settings;

public sealed class CdbCalculatorSettings
{
    public const string SectionName = "CdbCalculator";

    public decimal Cdi { get; init; } = 0.009m;

    public decimal BankRate { get; init; } = 1.08m;
}
