using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoftEng.Application.Handlers;
using SoftEng.Domain.Request;

namespace SoftEng.Api.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
public class SharedController(IMediator sender, ILogger <SharedController> logger) : ControllerBase
{
    [HttpGet("Genders")]
    public async Task<IActionResult> GetGenderAsync(CancellationToken ct)
    {
        return Ok(new List<string> { "Male", "Female", "Kinley" });
    }
    [HttpGet("Schools")]
    public async Task<IActionResult> GetSchoolsAsync(CancellationToken ct)
    {
        logger.LogInformation("Fetching school types");
        return Ok(await sender.Send(new GetSchoolListQuery(), ct));
    }
    [HttpGet("Programs")]
    public async Task<IActionResult> GetProgramAsync(CancellationToken ct)
    {
        return Ok(new List<string> { "Computer Science", "Engineering", "Business", "Arts & Humanities", "Natural Sciences" });
    }
    [HttpGet("Semesters")]
    public async Task<IActionResult> GetSemesterAsync(CancellationToken ct)
    {
        return Ok(new List<string> { "1st", "2nd" });
    }
}
