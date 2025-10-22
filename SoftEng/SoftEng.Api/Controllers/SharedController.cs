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
    [HttpGet("items")]
    public async Task<IActionResult> GetSharedItemsAsync([FromQuery] GetSharedRequest request, CancellationToken ct)
    {
        logger.LogInformation("Executing shared items query for type: {Type}", request.Type);

        var items = await sender.Send(new GetSharedQuery(request), ct);
        return Ok(items);
    }
}
