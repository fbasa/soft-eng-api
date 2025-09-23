using SoftEng.Domain.Response;

namespace SoftEng.Infrastructure.Contracts;

public interface IStudentRepository
{
    Task<IReadOnlyList<StudentResponse>> GetStudentsAsync(CancellationToken ct);
}
