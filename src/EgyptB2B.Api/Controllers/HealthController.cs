using Microsoft.AspNetCore.Mvc;

namespace EgyptB2B.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            status = "Healthy",
            timestampUtc = DateTime.UtcNow
        });
    }
}
