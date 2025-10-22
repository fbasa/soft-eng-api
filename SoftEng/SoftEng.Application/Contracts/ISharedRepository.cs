using SoftEng.Domain.Model;
using SoftEng.Domain.Request;
namespace SoftEng.Application.Contracts;

public interface ISharedRepository
{
    Task<IReadOnlyList<SharedModel>> GetSharedItemsAsync(GetSharedRequest request, CancellationToken ct);
}