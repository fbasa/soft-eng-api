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
        if (result.Any())
        {
            var item = result.First();
            return new GetStudentListResponse()
            {
                Items = mapper.Map<IReadOnlyList<GetStudentResult>>(result),
                TotalCount = item.TotalCount,
                TotalPages = item.TotalPages
            };
        }
        return new GetStudentListResponse();
    }
}
