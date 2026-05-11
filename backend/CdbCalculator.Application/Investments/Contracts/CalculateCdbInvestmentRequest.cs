namespace CdbCalculator.Application.Investments.Contracts;

public sealed record CalculateCdbInvestmentRequest(decimal InitialAmount, int Months);
