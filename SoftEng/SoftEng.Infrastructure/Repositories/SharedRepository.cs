using SoftEng.Application.Contracts;
using SoftEng.Domain.Model;
using SoftEng.Domain.Request;
using SoftEng.Infrastructure.Dapper;

namespace SoftEng.Infrastructure.Repositories;

internal sealed class SharedRepository(IDapperBaseService dapper) : ISharedRepository
{
    public async Task<IReadOnlyList<SharedModel>> GetGendersAsync(CancellationToken ct)
    {
        const string sql = "sp_GetGenders";
        var genders = await dapper.SqlQueryAsync<SharedModel>(sql,null, ct);
        return genders.ToList();
    }
}
