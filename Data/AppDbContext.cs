using Microsoft.EntityFrameworkCore;
using PragatiMarg.Models;

namespace PragatiMarg.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Project> Projects => Set<Project>();

    public DbSet<TaskItem> Tasks { get; set; }
    
}