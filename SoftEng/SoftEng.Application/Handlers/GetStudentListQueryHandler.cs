using MediatR;
using SoftEng.Application.Caching;
using SoftEng.Application.Common;
using SoftEng.Domain.Response;
using SoftEng.Infrastructure.Contracts;

namespace SoftEng.Application.Handlers;

public record GetStudentListQuery() : IRequest<IReadOnlyList<GetStudentListResponse>>, ICacheableQuery
{
    public string CacheKey => OutputCachedKeyNames.StudentList;

    public TimeSpan? Ttl => TimeSpan.FromSeconds(30);
}

public class GetStudentListQueryHandler(IStudentRepository repo) : IRequestHandler<GetStudentListQuery, IReadOnlyList<GetStudentListResponse>>
{
    public async Task<IReadOnlyList<GetStudentListResponse>> Handle(GetStudentListQuery request, CancellationToken ct)
    {
        return await repo.GetStudentsAsync(ct);
    }
}
