using System.Net;
using CdbCalculator.Application.Common.Validation;
using CdbCalculator.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CdbCalculator.Api.Middleware;

public sealed class GlobalExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, title, detail, extensions) = exception switch
        {
            RequestValidationException validationException => (
                HttpStatusCode.BadRequest,
                "Validation failed",
                "One or more request fields are invalid.",
                CreateValidationExtensions(validationException)),
            DomainException domainException => (
                HttpStatusCode.BadRequest,
                "Business rule violation",
                domainException.Message,
                null),
            _ => (
                HttpStatusCode.InternalServerError,
                "Unexpected error",
                "An unexpected error occurred while processing the request.",
                null)
        };

        if (statusCode == HttpStatusCode.InternalServerError)
        {
            logger.LogError(exception, "Unhandled exception while processing {Method} {Path}", context.Request.Method, context.Request.Path);
        }
        else
        {
            logger.LogWarning(exception, "Handled exception while processing {Method} {Path}", context.Request.Method, context.Request.Path);
        }

        var problemDetails = new ProblemDetails
        {
            Status = (int)statusCode,
            Title = title,
            Detail = detail,
            Instance = context.Request.Path
        };

        if (extensions is not null)
        {
            problemDetails.Extensions["errors"] = extensions;
        }

        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(problemDetails);
    }

    private static IReadOnlyCollection<ValidationFailureResponse> CreateValidationExtensions(RequestValidationException exception) =>
        exception.Errors;
}
