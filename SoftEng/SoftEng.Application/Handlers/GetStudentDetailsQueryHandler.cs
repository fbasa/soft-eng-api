using FluentValidation;
using MediatR;
using SoftEng.Application.Common;
using SoftEng.Application.Contracts;
using SoftEng.Domain.Request;
using SoftEng.Domain.Response;

namespace SoftEng.Application.Handlers;

public record GetStudentDetailsQuery(GetStudentDetailsRequest Request) : IRequest<Result<GetStudentDetailsResponse>>
{

}

public sealed class GetStudentDetailsQueryValidator : AbstractValidator<GetStudentDetailsQuery>
{
    public GetStudentDetailsQueryValidator()
    {
        RuleFor(x => x.Request.Id).GreaterThanOrEqualTo(1);
    }
}

public class GetStudentDetailsQueryHandler(IStudentRepository repo) : IRequestHandler<GetStudentDetailsQuery, Result<GetStudentDetailsResponse>>
{
    public async Task<Result<GetStudentDetailsResponse>> Handle(GetStudentDetailsQuery r, CancellationToken ct)
    {
        var result = await repo.GetStudentDetailsAsync(r.Request, ct);
        if(result is null)
        {
            return Result<GetStudentDetailsResponse>.Failure($"Student with Id {r.Request.Id} not found.");
        }
        return Result<GetStudentDetailsResponse>.Success(result!);
    }
}