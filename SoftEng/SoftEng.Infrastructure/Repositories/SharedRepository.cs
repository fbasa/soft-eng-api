using SoftEng.Application.Contracts;
using SoftEng.Domain.Request;

namespace SoftEng.Infrastructure.Repositories;

internal sealed class SharedRepository() : ISharedRepository
{
    public async Task<IReadOnlyList<string>> GetGendersAsync(GetGendersRequest request, CancellationToken ct)
    {
        var genders = new List<string> { "Male", "Female", "JKL Kinley"};
        return await Task.FromResult(genders);
    }
}
