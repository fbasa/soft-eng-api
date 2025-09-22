using SoftEng.Domain.Response;

namespace SoftEng.Infrastructure.Contracts;

public interface IStudentRepository
{
    Task<IEnumerable<StudentResponse>> GetStudents(CancellationToken ct);
}
