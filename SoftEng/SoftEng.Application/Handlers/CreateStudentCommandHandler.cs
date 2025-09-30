using FluentValidation;
using MediatR;
using SoftEng.Application.Caching.EventHandlers;
using SoftEng.Domain.Request;
using SoftEng.Application.Contracts;

namespace SoftEng.Application.Handlers;

public record CreateStudentCommand(CreateStudentRequest Request) : IRequest<int> { }

public sealed class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentCommandValidator() 
    { 
        RuleFor(x => x.Request.FirstName).NotEmpty().MaximumLength(50);
    }
}

public class CreateStudentCommandHandler(IMediator mediator, IStudentRepository repo) : IRequestHandler<CreateStudentCommand, int>
{
    public async Task<int> Handle(CreateStudentCommand r, CancellationToken ct)
    {
        var newId = await repo.AddStudentAsync(r.Request, ct);
        await mediator.Publish(new StudentChangedEvent());
        return newId;
    }
}
