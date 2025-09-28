using SoftEng.Domain.Request;
using SoftEng.Domain.Response;

namespace SoftEng.Infrastructure.Contracts;

public interface IStudentRepository
{
    Task<int> AddStudentAsync(AddStudentRequest request, CancellationToken ct);
    Task<IReadOnlyList<StudentResponse>> GetStudentsAsync(CancellationToken ct);
}
