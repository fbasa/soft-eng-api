using FluentValidation;
using MediatR;
using SoftEng.Application.Caching.EventHandlers;
using SoftEng.Domain.Request;
using SoftEng.Application.Contracts;
using SoftEng.Application.Common;

namespace SoftEng.Application.Handlers;

public record UpdateStudentCommand(UpdateStudentRequest Request) : IRequest<Result<int>> { }

public sealed class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentCommandValidator()
    {
        RuleFor(x => x.Request.Id).GreaterThanOrEqualTo(1);
        RuleFor(x => x.Request.FirstName).NotEmpty().MaximumLength(50);
    }
}

public class UpdateStudentCommandHandler(IMediator mediator, IStudentRepository repo) : IRequestHandler<UpdateStudentCommand, Result<int>>
{
    public async Task<Result<int>> Handle(UpdateStudentCommand r, CancellationToken ct)
    {
        var id = await repo.UpdateStudentAsync(r.Request, ct);
        await mediator.Publish(new StudentChangedEvent());
        return Result<int>.Success(id);
    }
}
