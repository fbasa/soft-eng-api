using MediatR;
using SoftEng.Application.Contracts;

namespace SoftEng.Application.Handlers;

public record GetSchoolListQuery() : IRequest<IReadOnlyList<string>>;

public class GetSchoolListQueryHandler(IStudentRepository studentRepository) : IRequestHandler<GetSchoolListQuery, IReadOnlyList<string>>
{
    public async Task<IReadOnlyList<string>> Handle(GetSchoolListQuery request, CancellationToken ct)
    {
        return await studentRepository.GetSchoolsAsync(ct);
    }
}