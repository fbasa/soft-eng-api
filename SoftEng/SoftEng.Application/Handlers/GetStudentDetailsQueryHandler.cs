using FluentValidation;
using MediatR;
using SoftEng.Domain.Request;
using SoftEng.Domain.Response;
using SoftEng.Infrastructure.Contracts;

namespace SoftEng.Application.Handlers;

public record GetStudentDetailsQuery(GetStudentDetailsRequest Request) : IRequest<GetStudentDetailsResponse>
{

}

public sealed class GetStudentDetailsQueryValidator : AbstractValidator<GetStudentDetailsQuery>
{
    public GetStudentDetailsQueryValidator()
    {
        RuleFor(x => x.Request.Id).GreaterThanOrEqualTo(1);
    }
}

public class GetStudentDetailsQueryHandler(IStudentRepository repo) : IRequestHandler<GetStudentDetailsQuery, GetStudentDetailsResponse>
{
    public async Task<GetStudentDetailsResponse> Handle(GetStudentDetailsQuery r, CancellationToken cancellationToken)
    {
        return await repo.GetStudentDetailsAsync(r.Request, cancellationToken);
    }
}