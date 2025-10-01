using MediatR;
using SoftEng.Application.Caching.EventHandlers;
using SoftEng.Application.Common;
using SoftEng.Application.Contracts;
using SoftEng.Domain.Request;

namespace SoftEng.Application.Handlers;

public record DeleteStudentCommand(DeleteStudentRequest Request) : IRequest<Result<bool>>;
public sealed class DeleteStudentCommandHandler(IMediator mediator, IStudentRepository repo) : IRequestHandler<DeleteStudentCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteStudentCommand r, CancellationToken ct)
    {
        var student = await repo.GetStudentDetailsAsync(new GetStudentDetailsRequest { Id = r.Request.Id }, ct);
        if (student == null)
            return Result<bool>.Failure($"Student with ID {r.Request.Id} not found");

        var deleted = await repo.DeleteStudentAsync(r.Request, ct);
        if (deleted)
        {
            await mediator.Publish(new StudentChangedEvent());
        }
        return Result<bool>.Success(true);
    }
}
