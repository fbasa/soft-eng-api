using SoftEng.Application.Contracts;
using SoftEng.Domain.Request;
using SoftEng.Domain.Request.Courses;
using SoftEng.Domain.Response;
using SoftEng.Infrastructure.Dapper;
using System.Data;


namespace SoftEng.Infrastructure.Repositories
{
    internal sealed class CourseRepository(IDapperBaseService dapper) : ICourseRepository
    {
        public async Task<(IReadOnlyList<GetCourseResult> Items, int TotalCount, int TotalPages)> GetCoursesAsync(GetCourseListRequest request, CancellationToken ct)
        {
            var parameters = RequestParameterBuilder<GetCourseListRequest>
                                .For(request)
                                .Input(i => i.Page)
                                .Input(i => i.Size)
                                .Output("TotalCount", DbType.Int32)
                                .Output("TotalPages", DbType.Int32)
                                .Build();

            // Execute stored procedure
            var items = await dapper.SqlQueryAsync<GetCourseResult>("sp_GetCourses", parameters, ct);

            // Retrieve output parameters
            int totalCount = parameters.Get<int>("TotalCount");
            int totalPages = parameters.Get<int>("TotalPages");

            return (items.ToList(), totalCount, totalPages);
        }

        public async Task<GetCourseDetailsResponse> GetCourseDetailsAsync(GetCourseDetailsRequest request, CancellationToken ct)
        {
            var parameters = RequestParameterBuilder<GetCourseDetailsRequest>
                    .For(request)
                    .Input("CourseID", x => x.CourseID)
                    .Build();

            var result = await dapper.SqlQueryAsync<GetCourseDetailsResponse>("sp_GetCourseById", parameters, ct);
            return result.FirstOrDefault()!;
        }
    }
}
