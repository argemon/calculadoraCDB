namespace CdbCalculator.Application.Common.Validation;

public sealed record ValidationFailureResponse(string Field, string Message);
