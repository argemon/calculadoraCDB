using CdbCalculator.Domain.Investments;
using Microsoft.Extensions.DependencyInjection;

namespace CdbCalculator.Infrastructure.DependencyInjection;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ITaxRatePolicy, RegressiveIncomeTaxRatePolicy>();
        services.AddSingleton<ICdbInvestmentCalculator, CdbInvestmentCalculator>();

        return services;
    }
}
