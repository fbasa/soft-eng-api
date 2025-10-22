using SoftEng.Domain.Request;

namespace SoftEng.Application.Contracts;

public interface ISharedRepository
{
    Task<IReadOnlyList<string>> GetGendersAsync(GetGendersRequest request, CancellationToken ct);
}