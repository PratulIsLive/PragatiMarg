using Microsoft.EntityFrameworkCore;
using PragatiMarg.Data;
using PragatiMarg.DTOs;
using PragatiMarg.Models;

namespace PragatiMarg.Services;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public User? GetUserByEmail(string email)
    {
        return _context.Users
            .FirstOrDefault(u => u.Email == email);
    }

    public User? UpdateProfile(string email, UpdateProfileDto request)
    {
        var user = _context.Users
            .FirstOrDefault(u => u.Email == email);

        if (user == null)
        {
            return null;
        }

        user.Name = request.Name;

        _context.SaveChanges();

        return user;
    }
}