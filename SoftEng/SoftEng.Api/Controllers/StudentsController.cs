using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Polly;
using SoftEng.Application.Handlers;
using SoftEng.Infrastructure.Contracts;

namespace SoftEng.Api.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
public class StudentsController(IMediator sender, 
    ILogger<StudentsController> logger) : ControllerBase
{
    [HttpGet]
    [OutputCache(PolicyName = "List30s")]
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        logger.LogInformation("Executing get students...");
        return Ok(await sender.Send(new StudentListQuery(), ct));
    }
}
