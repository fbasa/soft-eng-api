using MediatR;
using SoftEng.Domain.Request;
using SoftEng.Infrastructure.Contracts;

namespace SoftEng.Application.Handlers;

public record AddStudentCommand(AddStudentRequest Request) : IRequest<int> { }

public class AddStudentCommandHandler(IStudentRepository repo) : IRequestHandler<AddStudentCommand, int>
{
    public async Task<int> Handle(AddStudentCommand r, CancellationToken ct)
    {
        return await repo.AddStudentAsync(r.Request, ct);
    }
}
