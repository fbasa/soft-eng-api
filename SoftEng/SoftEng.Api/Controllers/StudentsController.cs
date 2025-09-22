using Microsoft.AspNetCore.Mvc;
using SoftEng.Infrastructure.Contracts;

namespace SoftEng.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController(IStudentRepository repo, 
    ILogger<StudentsController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        logger.LogInformation("Executing get students...");
        return Ok(await repo.GetStudents(ct));
    }
}
