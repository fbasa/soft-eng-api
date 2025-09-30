using SoftEng.Infrastructure.Dapper;
using System.Data;
using System.Data.Common;

namespace SoftEng.Infrastructure;
internal interface IUnitOfWork
{
    Task BeginAsync(CancellationToken ct = default);
    Task CommitAsync(CancellationToken ct = default);
    Task RollbackAsync(CancellationToken ct = default);
    DbConnection Connection { get; }
    DbTransaction Transaction { get; }
}

internal sealed class SqlUnitOfWork(ISqlConnectionFactory cf) : IUnitOfWork, IAsyncDisposable
{
    public DbConnection Connection { get; private set; } = default!;
    public DbTransaction Transaction { get; private set; } = default!;

    public async Task BeginAsync(CancellationToken ct = default)
    {
        Connection = await cf.OpenAsync(ct);
        Transaction = await Connection.BeginTransactionAsync(ct);
    }

    public Task CommitAsync(CancellationToken ct = default) => Transaction.CommitAsync(ct);
    public Task RollbackAsync(CancellationToken ct = default) => Transaction.RollbackAsync(ct);

    public async ValueTask DisposeAsync()
    {
        if (Transaction is not null) await Transaction.DisposeAsync();
        if (Connection is not null) await Connection.DisposeAsync();
    }
}

