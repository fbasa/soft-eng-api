using Dapper;
using SoftEng.Application.Contracts;
using SoftEng.Domain.Request;
using SoftEng.Domain.Response;
using SoftEng.Infrastructure.Dapper;

namespace SoftEng.Infrastructure.Repositories;

internal sealed class SharedRepository(IDapperBaseService dapper) : ISharedRepository
{
    public async Task<IReadOnlyList<SharedItemsResponse>> GetSharedItemsAsync(GetSharedRequest request, CancellationToken ct)
    {
        var parameters = RequestParameterBuilder<GetSharedRequest>
           .For(request)
           .Input(x => x.Type)
           .Build();
        var result = await dapper.SqlQueryAsync<SharedItemsResponse>("sp_GetSharedItemsBy", parameters, ct);
        return result.ToList();
    }
}
