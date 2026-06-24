using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PragatiMarg.Services;

namespace PragatiMarg.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly DashboardService _dashboardService;

    public DashboardController(
        DashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet]
    public IActionResult GetDashboard()
    {
        var userId = int.Parse(
        User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);

        var dashboard = _dashboardService.GetDashboard(userId);

        return Ok(dashboard);
    }

}