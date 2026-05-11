using CdbCalculator.Domain.Exceptions;

namespace CdbCalculator.Domain.Investments;

public sealed record Investment
{
    public Investment(decimal initialAmount, int months)
    {
        if (initialAmount <= 0)
        {
            throw new InvalidInvestmentException("Investment initial amount must be greater than zero.");
        }

        if (months <= 1)
        {
            throw new InvalidInvestmentException("Investment term must be greater than one month.");
        }

        InitialAmount = initialAmount;
        Months = months;
    }

    public decimal InitialAmount { get; }

    public int Months { get; }
}
