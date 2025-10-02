using SoftEng.Domain.Exception;

namespace SoftEng.Api.Errors;

public sealed class NotFoundException(string message)
    : DomainException(message, ErrorCodes.NotFound, 404);
