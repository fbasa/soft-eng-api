using MediatR;
using SoftEng.Domain.Request.Courses;
using SoftEng.Domain.Response;
using SoftEng.Application.Common;
using FluentValidation;
using SoftEng.Application.Contracts;

namespace SoftEng.Application.Handlers.Courses
{
    public record GetCourseDetailsQuery(GetCourseDetailsRequest Request) : IRequest<Result<GetCourseDetailsResponse>>;

    public sealed class GetCourseDetailsQueryValidator : AbstractValidator<GetCourseDetailsQuery>
    {
        public GetCourseDetailsQueryValidator()
        {
            RuleFor(x => x.Request.CourseID).GreaterThanOrEqualTo(1)
                .WithMessage("CourseID must be greater than 0.");
        }
    }

    public class GetCourseDetailsQueryHandler(
    ICourseRepository repo) : IRequestHandler<GetCourseDetailsQuery, Result<GetCourseDetailsResponse>>
    {
        public async Task<Result<GetCourseDetailsResponse>> Handle(GetCourseDetailsQuery r, CancellationToken ct)
        {
            var course = await repo.GetCourseDetailsAsync(r.Request, ct);

            if (course is null)
            {
                return Result<GetCourseDetailsResponse>.Failure($"Course with Id {r.Request.CourseID} not found.");
            }

            return Result<GetCourseDetailsResponse>.Success(course!);
        }
    }
}
