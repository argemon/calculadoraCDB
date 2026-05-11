using CdbCalculator.Application.Common.Settings;
using CdbCalculator.Application.Investments.Services;
using CdbCalculator.Application.Investments.Validators;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CdbCalculator.Application.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<CdbCalculatorSettings>()
            .Bind(configuration.GetSection(CdbCalculatorSettings.SectionName))
            .Validate(settings => settings.Cdi > 0, "CDI must be greater than zero.")
            .Validate(settings => settings.BankRate > 0, "Bank rate must be greater than zero.")
            .ValidateOnStart();

        services.AddScoped<ICdbInvestmentService, CdbInvestmentService>();
        services.AddValidatorsFromAssemblyContaining<CalculateCdbInvestmentRequestValidator>();

        return services;
    }
}
