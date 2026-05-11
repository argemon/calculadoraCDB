using CdbCalculator.Application.Common.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CdbCalculator.Api.Filters;

public sealed class FluentValidationActionFilter(IServiceProvider serviceProvider) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var validationErrors = new List<ValidationFailureResponse>();

        foreach (var argument in context.ActionArguments.Values.Where(value => value is not null))
        {
            var argumentType = argument!.GetType();
            var validatorType = typeof(IValidator<>).MakeGenericType(argumentType);

            if (serviceProvider.GetService(validatorType) is not IValidator validator)
            {
                continue;
            }

            var validationContext = new ValidationContext<object>(argument);
            var result = await validator.ValidateAsync(validationContext, context.HttpContext.RequestAborted);

            validationErrors.AddRange(result.Errors.Select(error =>
                new ValidationFailureResponse(error.PropertyName, error.ErrorMessage)));
        }

        if (validationErrors.Count > 0)
        {
            throw new RequestValidationException(validationErrors);
        }

        await next();
    }
}
