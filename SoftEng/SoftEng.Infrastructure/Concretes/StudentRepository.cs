using SoftEng.Domain.Response;
using SoftEng.Infrastructure.Contracts;

namespace SoftEng.Infrastructure.Concretes;

public class StudentRepository(IDapperBaseService dapper) : IStudentRepository
{
    public async Task<IEnumerable<StudentResponse>> GetStudents(CancellationToken ct)
    {
        return await dapper.SqlQueryAsync<StudentResponse>("sp_GetStudents", null, ct);
    }
}
