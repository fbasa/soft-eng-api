using MediatR;
using FluentValidation;
using SoftEng.Application.Caching.EventHandlers;
using SoftEng.Application.Contracts;
using SoftEng.Domain.Request;

namespace SoftEng.Application.Handlers;

public record UpdateStudentCommand(UpdateStudentRequest Request) : IRequest<int> { }

public sealed class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentCommandValidator()
    {
        RuleFor(x => x.Request.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Request.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Request.EmailAddress).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Request.PhoneNumber).NotEmpty().MaximumLength(12);
        RuleFor(x => x.Request.DOB).NotEmpty().LessThan(DateTime.Now);
    }
}

public class UpdateStudentCommandHandler(IMediator mediator, IStudentRepository repo) : IRequestHandler<UpdateStudentCommand, int>
{
    public async Task<int> Handle(UpdateStudentCommand r, CancellationToken ct)
    {
        var id = await repo.UpdateStudentAsync(r.Request, ct);
        await mediator.Publish(new StudentChangedEvent());
        return id;
    }
}
