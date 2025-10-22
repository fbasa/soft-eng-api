using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SoftEng.Domain.Request;
using MediatR;
using SoftEng.Application.Handlers;

namespace SoftEng.Api.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
public class SharedController(IMediator sender,
    ILogger <SharedController> logger) : ControllerBase
{
    [HttpGet("Genders")]
    public async Task<IActionResult> GetGendersAsync([FromQuery] GetGendersRequest request, CancellationToken ct)
    {
        logger.LogInformation("Executing genders");
        var genders = await sender.Send(new GetGendersQuery(request), ct);
        return Ok(genders);
    }
}
