using MediatR;
using SoftEng.Application.Caching;
using SoftEng.Domain.Request.Courses;
using SoftEng.Domain.Response;
using AutoMapper;
using SoftEng.Application.Contracts;


namespace SoftEng.Application.Handlers.Courses
{
    public record GetCourseListQuery(GetCourseListRequest Request)
        : IRequest<GetCourseListResponse>, ICacheableQuery
    {
        public string CacheKey => $"{OutputCachedKeyNames.CourseList}_{Request.Page}_{Request.Size}";
        public TimeSpan? Ttl => TimeSpan.FromSeconds(30);
    }

    public class GetCourseListQueryHandler(
        ICourseRepository repo,
        IMapper mapper) : IRequestHandler<GetCourseListQuery, GetCourseListResponse>
    {
        public async Task<GetCourseListResponse> Handle(GetCourseListQuery r, CancellationToken ct)
        {
            var (items, totalCount, totalPages) = await repo.GetCoursesAsync(r.Request, ct);

            return new GetCourseListResponse
            {
                Items = mapper.Map<IReadOnlyList<GetCourseResult>>(items),
                TotalCount = totalCount,
                Pages = totalPages > 0 ? Enumerable.Range(1, totalPages).ToArray() : Array.Empty<int>()
            };
        }
    }
}
