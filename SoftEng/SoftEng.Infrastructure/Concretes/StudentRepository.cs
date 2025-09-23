using SoftEng.Domain.Response;
using SoftEng.Infrastructure.Contracts;

namespace SoftEng.Infrastructure.Concretes;

public class StudentRepository(IDapperBaseService dapper) : IStudentRepository
{
    public async Task<IReadOnlyList<StudentResponse>> GetStudentsAsync(CancellationToken ct)
    {
        var result = await dapper.SqlQueryAsync<StudentResponse>("sp_GetStudents", null, ct);
        return result.ToList();
    }
}
