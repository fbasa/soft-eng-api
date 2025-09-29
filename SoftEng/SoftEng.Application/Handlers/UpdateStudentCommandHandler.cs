using FluentValidation;
using MediatR;
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

public class UpdateStudentCommandHandler(IStudentRepository repo) : IRequestHandler<UpdateStudentCommand, int>
{
    public async Task<int> Handle(UpdateStudentCommand r, CancellationToken ct)
    {
        return await repo.UpdateStudentAsync(r.Request, ct);
    }
}
