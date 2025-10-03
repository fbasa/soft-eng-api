using AutoMapper;
using MediatR;
using SoftEng.Application.Caching;
using SoftEng.Application.Contracts;
using SoftEng.Domain.Request;
using SoftEng.Domain.Response;

namespace SoftEng.Application.Handlers;

public record GetStudentListQuery(GetStudentListRequest Request) : IRequest<GetStudentListResponse>, ICacheableQuery
{
    public string CacheKey => $"{OutputCachedKeyNames.StudentList}_{Request.Page}_{Request.Size}";

    public TimeSpan? Ttl => TimeSpan.FromSeconds(30);
}

public class GetStudentListQueryHandler(IStudentRepository repo, IMapper mapper) : IRequestHandler<GetStudentListQuery, GetStudentListResponse>
{
    public async Task<GetStudentListResponse> Handle(GetStudentListQuery r, CancellationToken ct)
    {
        var result = await repo.GetStudentsAsync(r.Request, ct);
        var firstOrDefault = result.FirstOrDefault();
        return new GetStudentListResponse()
        {
            Items = mapper.Map<List<GetStudentResult>>(result),
            TotalCount = firstOrDefault?.TotalCount ?? 0,
            TotalPages = firstOrDefault?.TotalPages ?? 0
        };
    }
}
