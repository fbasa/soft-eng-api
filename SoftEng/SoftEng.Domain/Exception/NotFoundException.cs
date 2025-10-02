
namespace SoftEng.Domain.Exception;

public sealed class NotFoundException(string message)
    : DomainException(message, ErrorCodes.NotFound, 404);
