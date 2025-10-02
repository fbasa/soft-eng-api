using MediatR;
using Microsoft.Extensions.Logging;
using SoftEng.Domain.Exception;

namespace SoftEng.Application.Behavior;

public class ExceptionHandlingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TResponse : class
{
    private readonly ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> _logger;

    public ExceptionHandlingBehavior(ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (DomainException ex)
        {
            _logger.LogWarning(ex, "Validation failed for {Request}", typeof(TRequest).Name);
            return (Activator.CreateInstance(typeof(TResponse), false, ex.Message) as TResponse)!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception for {Request}", typeof(TRequest).Name);
            // Option 1: wrap in a failure Result<T>
            return (Activator.CreateInstance(typeof(TResponse), false, "Unexpected error") as TResponse)!;
            // Option 2: rethrow and let middleware handle
        }
    }
}
