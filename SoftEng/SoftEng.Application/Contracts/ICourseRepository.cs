using SoftEng.Domain.Request.Courses;
using SoftEng.Domain.Response;


namespace SoftEng.Application.Contracts
{
    public interface ICourseRepository
    {
        public Task<(IReadOnlyList<GetCourseResult> Items, int TotalCount, int TotalPages)> GetCoursesAsync(GetCourseListRequest request, CancellationToken ct);
        public Task<GetCourseDetailsResponse> GetCourseDetailsAsync(GetCourseDetailsRequest request, CancellationToken ct);
    }
}
