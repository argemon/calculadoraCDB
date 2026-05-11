namespace CdbCalculator.Application.Common.Validation;

public sealed class RequestValidationException(IReadOnlyCollection<ValidationFailureResponse> errors)
    : Exception("One or more validation errors occurred.")
{
    public IReadOnlyCollection<ValidationFailureResponse> Errors { get; } = errors;
}
