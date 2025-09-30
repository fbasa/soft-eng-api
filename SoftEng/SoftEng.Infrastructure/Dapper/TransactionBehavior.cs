using MediatR;
using SoftEng.Application.Common;

namespace SoftEng.Infrastructure.Dapper;

internal sealed class TransactionBehavior<TRequest, TResponse>(IUnitOfWork uow) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        if (request is not ITransactionalRequest)
            return await next();

        await uow.BeginAsync(ct);
        try
        {
            var result = await next();
            await uow.CommitAsync(ct);
            return result;
        }
        catch
        {
            await uow.RollbackAsync(ct);
            throw;
        }
    }
}

