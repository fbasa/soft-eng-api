using SoftEng.Domain.Model;

namespace SoftEng.Domain.Response;

public class GetCourseListResponse
{
    public IReadOnlyList<GetCourseResult> Items { get; set; } = Array.Empty<GetCourseResult>();
    public int TotalCount { get; set; }
    public int[] Pages { get; set; } = Array.Empty<int>();
}

public class GetCourseResult : CourseModel
{
}
