namespace CdbCalculator.Domain.Investments;

public sealed record CdbCalculationResult(
    decimal GrossAmount,
    decimal NetAmount,
    decimal TaxAmount,
    decimal GrossProfit);
