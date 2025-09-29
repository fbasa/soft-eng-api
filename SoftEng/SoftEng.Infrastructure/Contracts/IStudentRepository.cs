using SoftEng.Domain.Request;
using SoftEng.Domain.Response;

namespace SoftEng.Infrastructure.Contracts;

public interface IStudentRepository
{
    Task<int> AddStudentAsync(CreateStudentRequest request, CancellationToken ct);
    Task<GetStudentDetailsResponse> GetStudentDetailsAsync(GetStudentDetailsRequest request, CancellationToken cancellationToken);
    Task<IReadOnlyList<GetStudentListResponse>> GetStudentsAsync(GetStudentListRequest request, CancellationToken ct);
    Task<int> UpdateStudentAsync(UpdateStudentRequest request, CancellationToken ct);
}
