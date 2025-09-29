using MediatR;
using Asp.Versioning;
using SoftEng.Domain.Request;
using Microsoft.AspNetCore.Mvc;
using SoftEng.Application.Handlers;
using Microsoft.AspNetCore.OutputCaching;

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
        return Ok(await sender.Send(new GetStudentListQuery(), ct));
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetById([FromQuery] GetStudentDetailsRequest request, CancellationToken ct)
    {
        return Ok(await sender.Send(new GetStudentDetailsQuery(request), ct));
    }

    [HttpPost]
    public async Task<IActionResult> AddNew([FromBody] AddStudentRequest request, CancellationToken ct)
    {
        return Ok(await sender.Send(new AddStudentCommand(request), ct));
    }
}
