using SoftEng.Domain.Request;
using SoftEng.Domain.Response;

namespace SoftEng.Infrastructure.Contracts;

public interface IStudentRepository
{
    Task<int> AddStudentAsync(AddStudentRequest request, CancellationToken ct);
    Task<GetStudentDetailsResponse> GetStudentDetailsAsync(GetStudentDetailsRequest request, CancellationToken cancellationToken);
    Task<IReadOnlyList<GetStudentListResponse>> GetStudentsAsync(CancellationToken ct);
}
