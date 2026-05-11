namespace CdbCalculator.Domain.Investments;

public interface ICdbInvestmentCalculator
{
    CdbCalculationResult Calculate(Investment investment, CdbRates rates);
}
