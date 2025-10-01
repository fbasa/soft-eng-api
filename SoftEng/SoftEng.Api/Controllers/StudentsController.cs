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
    public async Task<IActionResult> GetStudentsAsync([FromQuery] GetStudentListRequest request, CancellationToken ct)
    {
        logger.LogInformation("Executing get students...");
        return Ok(await sender.Send(new GetStudentListQuery(request), ct));
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetStudentByIdAsync([FromQuery] GetStudentDetailsRequest request, CancellationToken ct)
    {
        return Ok(await sender.Send(new GetStudentDetailsQuery(request), ct));
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudentAsync([FromBody] CreateStudentRequest request, CancellationToken ct)
    {
        return Ok(await sender.Send(new CreateStudentCommand(request), ct));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateStudentAsync([FromBody] UpdateStudentRequest request, CancellationToken ct)
    {
        return Ok(await sender.Send(new UpdateStudentCommand(request), ct));
    }
}
