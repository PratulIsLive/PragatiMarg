using Microsoft.EntityFrameworkCore;
using PragatiMarg.Data;
using PragatiMarg.DTOs;
using PragatiMarg.Models;

namespace PragatiMarg.Services;

public class ProjectService
{
    private readonly AppDbContext _context;

    public ProjectService(AppDbContext context)
    {
        _context = context;
    }

    // Create Project
    public ProjectResponseDto CreateProject(
        int userId,
        CreateProjectDto request)
    {
        var project = new Project
        {
            Title = request.Title,
            Description = request.Description,
            Status = ProjectStatus.Todo,
            UserId = userId
        };

        _context.Projects.Add(project);
        _context.SaveChanges();

        return MapToResponse(project);
    }

    // Get All Projects
    public List<ProjectResponseDto> GetAllProjects(
        int userId,
        ProjectQueryDto query)
    {
        var projects = _context.Projects
            .Include(p => p.Tasks)
            .Where(p => p.UserId == userId && !p.IsDeleted);

        // Search
        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            projects = projects.Where(p =>
                p.Title.Contains(query.Search) ||
                p.Description.Contains(query.Search));
        }

        // Sorting
        projects = query.Descending
            ? projects.OrderByDescending(p => p.CreatedAt)
            : projects.OrderBy(p => p.CreatedAt);

        // Pagination
        projects = projects
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize);

        return projects
            .ToList()
            .Select(MapToResponse)
            .ToList();
    }

    // Get Project By Id
    public ProjectResponseDto? GetProjectById(
        int id,
        int userId)
    {
        var project = _context.Projects
            .Include(p => p.Tasks)
            .FirstOrDefault(p =>
                p.Id == id &&
                p.UserId == userId &&
                !p.IsDeleted);

        if (project == null)
        {
            return null;
        }

        return MapToResponse(project);
    }

    // Update Project
    public ProjectResponseDto? UpdateProject(
        int id,
        int userId,
        UpdateProjectDto request)
    {
        var project = _context.Projects
            .Include(p => p.Tasks)
            .FirstOrDefault(p =>
                p.Id == id &&
                p.UserId == userId &&
                !p.IsDeleted);

        if (project == null)
        {
            return null;
        }

        project.Title = request.Title;
        project.Description = request.Description;
        project.Status = request.Status;
        project.UpdatedAt = DateTime.UtcNow;

        _context.SaveChanges();

        return MapToResponse(project);
    }

    // Soft Delete Project
    public bool DeleteProject(int id, int userId)
    {
        var project = _context.Projects
            .FirstOrDefault(p =>
                p.Id == id &&
                p.UserId == userId &&
                !p.IsDeleted);

        if (project == null)
        {
            return false;
        }

        project.IsDeleted = true;

        _context.SaveChanges();

        return true;
    }

    // Helper Method
    private ProjectResponseDto MapToResponse(Project project)
    {
        var totalTasks = project.Tasks
            .Count(t => !t.IsDeleted);

        var completedTasks = project.Tasks
            .Count(t =>
                !t.IsDeleted &&
                t.Status == TaskItemStatus.Completed);

        double completionPercentage = 0;

        if (totalTasks > 0)
        {
            completionPercentage =
                (double)completedTasks / totalTasks * 100;
        }

        return new ProjectResponseDto
        {
            Id = project.Id,
            Title = project.Title,
            Description = project.Description,
            Status = project.Status,
            CreatedAt = project.CreatedAt,
            UpdatedAt = project.UpdatedAt,
            CompletionPercentage = Math.Round(completionPercentage, 2),
            TotalTasks = totalTasks,
            CompletedTasks = completedTasks,
            PendingTasks = totalTasks - completedTasks

        };
    }
}