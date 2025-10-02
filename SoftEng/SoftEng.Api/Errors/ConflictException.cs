using SoftEng.Domain.Exception;

namespace SoftEng.Api.Errors;

public sealed class ConflictException(string message, string code = ErrorCodes.ConcurrencyConflict)
    : DomainException(message, code, 409);
