using MediatR;
using SoftEng.Domain.Request;
using SoftEng.Infrastructure.Contracts;

namespace SoftEng.Application.Handlers;

public record UpdateStudentCommand(UpdateStudentRequest Request) : IRequest<int> { }

public class UpdateStudentCommandHandler(IStudentRepository repo) : IRequestHandler<UpdateStudentCommand, int>
{
    public async Task<int> Handle(UpdateStudentCommand r, CancellationToken ct)
    {
        return await repo.UpdateStudentAsync(r.Request, ct);
    }
}
