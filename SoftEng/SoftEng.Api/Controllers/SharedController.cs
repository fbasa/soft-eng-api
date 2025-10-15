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
}
