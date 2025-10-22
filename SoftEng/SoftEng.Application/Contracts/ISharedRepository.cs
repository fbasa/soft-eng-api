using SoftEng.Domain.Request;
using SoftEng.Domain.Response;
namespace SoftEng.Application.Contracts;

public interface ISharedRepository
{
    Task<IReadOnlyList<SharedItemsResponse>> GetSharedItemsAsync(GetSharedRequest request, CancellationToken ct);
}