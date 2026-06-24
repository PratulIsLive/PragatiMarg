using PragatiMarg.Data;
using PragatiMarg.DTOs;
using PragatiMarg.Models;

namespace PragatiMarg.Services;

public class DashboardService
{
    private readonly AppDbContext _context;

    public DashboardService(AppDbContext context)
    {
        _context = context;
    }

    public DashboardResponseDto GetDashboard(int userId)
    {
        return new DashboardResponseDto
        {
            TotalProjects = _context.Projects
                .Count(p => p.UserId == userId && !p.IsDeleted),

            TodoProjects = _context.Projects
                .Count(p => p.UserId == userId &&
                            p.Status == ProjectStatus.Todo &&
                            !p.IsDeleted),

            InProgressProjects = _context.Projects
                .Count(p => p.UserId == userId &&
                            p.Status == ProjectStatus.InProgress &&
                            !p.IsDeleted),

            CompletedProjects = _context.Projects
                .Count(p => p.UserId == userId &&
                            p.Status == ProjectStatus.Completed &&
                            !p.IsDeleted),

            TotalTasks = _context.Tasks
                .Count(t => !t.IsDeleted),

            TodoTasks = _context.Tasks
                .Count(t => t.Status == TaskItemStatus.Todo &&
                            !t.IsDeleted),

            InProgressTasks = _context.Tasks
                .Count(t => t.Status == TaskItemStatus.InProgress &&
                            !t.IsDeleted),

            CompletedTasks = _context.Tasks
                .Count(t => t.Status == TaskItemStatus.Completed &&
                            !t.IsDeleted)
        };
    }
}