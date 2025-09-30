using MediatR;
using SoftEng.Application.Caching;
using SoftEng.Domain.Request;
using SoftEng.Domain.Response;
using SoftEng.Application.Contracts;

namespace SoftEng.Application.Handlers;

public record GetStudentListQuery(GetStudentListRequest Request) : IRequest<IReadOnlyList<GetStudentListResponse>>, ICacheableQuery
{
    public string CacheKey => $"{OutputCachedKeyNames.StudentList}_{Request.Page}_{Request.Size}";

    public TimeSpan? Ttl => TimeSpan.FromSeconds(30);
}

public class GetStudentListQueryHandler(IStudentRepository repo) : IRequestHandler<GetStudentListQuery, IReadOnlyList<GetStudentListResponse>>
{
    public async Task<IReadOnlyList<GetStudentListResponse>> Handle(GetStudentListQuery r, CancellationToken ct)
    {
        return await repo.GetStudentsAsync(r.Request, ct);
    }
}
