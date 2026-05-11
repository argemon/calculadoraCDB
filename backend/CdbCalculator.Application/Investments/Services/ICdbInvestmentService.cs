using CdbCalculator.Application.Investments.Contracts;

namespace CdbCalculator.Application.Investments.Services;

public interface ICdbInvestmentService
{
    CdbInvestmentResponse Calculate(CalculateCdbInvestmentRequest request);
}
