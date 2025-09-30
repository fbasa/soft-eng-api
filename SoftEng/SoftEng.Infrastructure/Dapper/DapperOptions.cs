namespace SoftEng.Infrastructure.Dapper;


internal sealed class DapperOptions
{
    public int CommandTimeoutSeconds { get; init; } = 30; // default
}