using MediatR;
using SoftEng.Application.Caching;
using SoftEng.Application.Common;
using SoftEng.Domain.Response;
using SoftEng.Infrastructure.Contracts;

namespace SoftEng.Application.Handlers;

public record StudentListQuery() : IRequest<IReadOnlyList<StudentResponse>>, ICacheableQuery
{
    public string CacheKey => OutputCachedKeyNames.StudentList;

    public TimeSpan? Ttl => TimeSpan.FromSeconds(30);
}

public class StudentListQueryHandler(IStudentRepository repo) : IRequestHandler<StudentListQuery, IReadOnlyList<StudentResponse>>
{
    public async Task<IReadOnlyList<StudentResponse>> Handle(StudentListQuery request, CancellationToken ct)
    {
        return await repo.GetStudentsAsync(ct);
    }
}
