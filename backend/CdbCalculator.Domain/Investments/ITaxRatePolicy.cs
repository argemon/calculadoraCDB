namespace CdbCalculator.Domain.Investments;

public interface ITaxRatePolicy
{
    decimal GetRate(int months);
}
