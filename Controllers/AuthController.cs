using Microsoft.AspNetCore.Mvc;
using PragatiMarg.DTOs;
using PragatiMarg.Services;

namespace PragatiMarg.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequestDto request)
    {
        try
        {
            var user = _authService.Register(request);

            return Ok(new 
            {
                message = "User registered successfully",
                user.Id, 
                user.Name, 
                user.Email 
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new 
            { 
                message = ex.Message 
            });
        }
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequestDto request)
    {
        var token = _authService.Login(request);

        if (token == null)
        {
            return Unauthorized(new 
            { 
                message = "Invalid email or password" 
            });
        }

        return Ok(new 
        { 
            message = "Login successful", 
            token = token 
        });
    }
}