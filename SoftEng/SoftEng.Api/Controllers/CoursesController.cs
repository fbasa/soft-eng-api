using MediatR;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using SoftEng.Domain.Request.Courses;
using SoftEng.Application.Handlers.Courses;

namespace SoftEng.Api.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
public class CoursesController(IMediator sender, ILogger<CoursesController> logger) : ControllerBase
{
    // GET api/v1/courses
    [HttpGet]
    [OutputCache(PolicyName = "List30s")]
    public async Task<IActionResult> GetCoursesAsync([FromQuery] GetCourseListRequest request, CancellationToken ct)
    {
        logger.LogInformation("Fetching all courses...");
        var courses = await sender.Send(new GetCourseListQuery(request), ct);
        return Ok(courses);
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetCourseByIdAsync(int id, CancellationToken ct)
    {
        var request = new GetCourseDetailsRequest { CourseID = id };
        var result = await sender.Send(new GetCourseDetailsQuery(request), ct);

        if (!result.IsSuccess)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

}
