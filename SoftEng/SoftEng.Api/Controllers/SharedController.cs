using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SoftEng.Application.Contracts;
using SoftEng.Domain.Request;

namespace SoftEng.Api.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
public class SharedController : ControllerBase
{
    private readonly ISharedRepository _sharedRepository;
    private readonly ILogger<SharedController> _logger;

    public SharedController(ISharedRepository sharedRepository,  ILogger<SharedController> logger)
    {
        _sharedRepository = sharedRepository;
        _logger = logger;
    }
    [HttpGet("Genders")]
    public async Task<IActionResult> GetGendersAsync(CancellationToken ct)
    {
        _logger.LogInformation("Executing genders");
        var request = new GetGendersRequest();
        var genders = await _sharedRepository.GetGendersAsync(request, ct);
        return Ok(genders);
    }
}
