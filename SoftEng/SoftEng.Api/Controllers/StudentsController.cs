using Microsoft.AspNetCore.Mvc;
using SoftEng.Domain.Response;

namespace SoftEng.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly ILogger<StudentsController> _logger;

    public StudentsController(ILogger<StudentsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<StudentResponse> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new StudentResponse
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Gender = Random.Shared.Next(-20, 55),
            FirstName = "Frank"
        })
        .ToArray();
    }
}
