using PragatiMarg.Data;
using PragatiMarg.DTOs;
using PragatiMarg.Models;

namespace PragatiMarg.Services;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly PasswordHasherService _passwordHasher;
    private readonly JwtService _jwtService;

    public AuthService(
        AppDbContext context,
        PasswordHasherService passwordHasher,
        JwtService jwtService)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    // Registering the User
    public User Register(RegisterRequestDto request)
    {
        var existingUser = _context.Users
            .FirstOrDefault(u => u.Email == request.Email);

        if (existingUser != null)
        {
            throw new Exception("User with this email already exists.");
        }

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash = _passwordHasher.HashPassword(request.Password)
        };

        _context.Users.Add(user);

        _context.SaveChanges();

        return user;
    }

    // Login the User
    public string? Login(LoginRequestDto request)
    {
        var user = _context.Users
            .FirstOrDefault(u => u.Email == request.Email);

        if (user == null)
        {
            return null;
        }

        var hashedPassword =
            _passwordHasher.HashPassword(request.Password);

        if (user.PasswordHash != hashedPassword)
        {
            return null;
        }

        return _jwtService.GenerateToken(user);
    }
}