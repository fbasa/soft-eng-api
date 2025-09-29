using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using SoftEng.Application.Caching.Invalidator;
using SoftEng.Domain.Request;
using SoftEng.Infrastructure.Contracts;

namespace SoftEng.Application.Handlers;

public record UpdateStudentCommand(UpdateStudentRequest Request) : IRequest<int> { }

public sealed class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentCommandValidator()
    {
        RuleFor(x => x.Request.Id).GreaterThanOrEqualTo(1);
        RuleFor(x => x.Request.FirstName).NotEmpty().MaximumLength(50);
    }
}

public class UpdateStudentCommandHandler(IMediator mediator, IStudentRepository repo) : IRequestHandler<UpdateStudentCommand, int>
{
    public async Task<int> Handle(UpdateStudentCommand r, CancellationToken ct)
    {
        var id = await repo.UpdateStudentAsync(r.Request, ct);
        await mediator.Publish(new StudentListChanged());
        return id;
    }
}
