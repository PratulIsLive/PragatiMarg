using Microsoft.AspNetCore.Mvc;

namespace PragatiMarg.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            status = "PragatiMarg API is running"
        });
    }
}