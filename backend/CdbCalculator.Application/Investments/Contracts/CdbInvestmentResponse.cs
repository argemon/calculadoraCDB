namespace CdbCalculator.Application.Investments.Contracts;

public sealed record CdbInvestmentResponse(
    decimal GrossAmount,
    decimal NetAmount,
    decimal TaxAmount,
    decimal GrossProfit);
