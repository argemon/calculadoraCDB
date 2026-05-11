namespace CdbCalculator.Domain.Exceptions;

public sealed class InvalidInvestmentException(string message) : DomainException(message);
