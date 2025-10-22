using Dapper;
using SoftEng.Application.Contracts;
using SoftEng.Domain.Model;
using SoftEng.Domain.Request;
using SoftEng.Infrastructure.Dapper;

namespace SoftEng.Infrastructure.Repositories;

internal sealed class SharedRepository(IDapperBaseService dapper) : ISharedRepository
{
    public async Task<IReadOnlyList<SharedModel>> GetSharedItemsAsync(GetSharedRequest request, CancellationToken ct)
    {
        const string spName = "sp_GetSharedItemsBy";
        var param = new DynamicParameters();
        param.Add("@Type", request.Type);

        var result = await dapper.SqlQueryAsync<SharedModel>(sql: spName, parameters: param, ct);
        return result.ToList();
    }
}
