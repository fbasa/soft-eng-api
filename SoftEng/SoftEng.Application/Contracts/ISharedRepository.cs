using SoftEng.Domain.Model;

namespace SoftEng.Application.Contracts;

public interface ISharedRepository
{
    Task<IReadOnlyList<SharedModel>> GetGendersAsync(CancellationToken ct);
}