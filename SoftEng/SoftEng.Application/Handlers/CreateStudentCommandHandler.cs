using MediatR;
using FluentValidation;
using SoftEng.Application.Caching.EventHandlers;
using SoftEng.Application.Contracts;

namespace SoftEng.Application.Handlers;

public record CreateStudentCommand(CreateStudentRequest Request) : IRequest<int> { }

public sealed class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentCommandValidator() 
    { 
        RuleFor(x => x.Request.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Request.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Request.EmailAddress).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Request.PhoneNumber).NotEmpty().MaximumLength(12);
        RuleFor(x => x.Request.DOB).NotEmpty().LessThan(DateTime.Now);
    }
}

public class CreateStudentCommandHandler(IMediator mediator, IStudentRepository repo) : IRequestHandler<CreateStudentCommand, int>
{
    public async Task<int> Handle(CreateStudentCommand r, CancellationToken ct)
    {
        var newId = await repo.CreateStudentAsync(r.Request, ct);
        await mediator.Publish(new StudentChangedEvent());
        return newId;
    }
}
