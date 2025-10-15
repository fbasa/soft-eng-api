using MediatR;
using Asp.Versioning;
using SoftEng.Domain.Request;
using Microsoft.AspNetCore.Mvc;

namespace SoftEng.Api.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
public class SharedController() : ControllerBase
{
    [HttpGet("Genders")]
    public async Task<IActionResult> GetGenderAsync(CancellationToken ct)
    {
        return Ok(new List<string> { "Male", "Female" });
    }
    [HttpGet("Schools")]
    public async Task<IActionResult> GetSchoolAsync(CancellationToken ct)
    {
        return Ok(new List<string> { "High School", "College", "University", "Other" });
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
