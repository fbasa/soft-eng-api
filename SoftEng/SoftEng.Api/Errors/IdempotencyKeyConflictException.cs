using Microsoft.AspNetCore.Http;
using SoftEng.Domain.Exception;

namespace SoftEng.Api.Errors;

public sealed class IdempotencyKeyConflictException(string message="")
    : DomainException(
        message ?? "Idempotency key conflict", 
        ErrorCodes.IdempotencyKeyConflict,
        StatusCodes.Status400BadRequest, 
        new System.Exception("The X-Idempotency-Key has already been used with different request data.")
        );