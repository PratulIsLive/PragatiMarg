using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PragatiMarg.DTOs;
using PragatiMarg.Services;
using System.Security.Claims;

namespace PragatiMarg.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpGet("profile")]
    public IActionResult GetProfile()
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;

        if (email == null)
        {
            return Unauthorized();
        }

        var user = _userService.GetUserByEmail(email);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [Authorize]
    [HttpPut("profile")]
    public IActionResult UpdateProfile(UpdateProfileDto request)
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;

        if (email == null)
        {
            return Unauthorized();
        }

        var updatedUser = _userService.UpdateProfile(email, request);

        if (updatedUser == null)
        {
            return NotFound();
        }

        return Ok(updatedUser);
    }
}