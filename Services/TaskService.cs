using Microsoft.EntityFrameworkCore;
using PragatiMarg.Data;
using PragatiMarg.DTOs;
using PragatiMarg.Models;

namespace PragatiMarg.Services;

public class TaskService
{
    private readonly AppDbContext _context;

    public TaskService(AppDbContext context)
    {
        _context = context;
    }

    // Create Task
    public TaskResponseDto CreateTask(int projectId, CreateTaskDto request)
    {
        var task = new TaskItem
        {
            Title = request.Title,
            Description = request.Description,
            Priority = request.Priority,
            Status = TaskItemStatus.Todo,
            DueDate = request.DueDate,
            ProjectId = projectId
        };

        _context.Tasks.Add(task);
        _context.SaveChanges();

        return new TaskResponseDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            Priority = task.Priority,
            DueDate = task.DueDate,
            CreatedAt = task.CreatedAt,
            UpdatedAt = task.UpdatedAt
        };
    }

    // Get All Tasks By Project
    public List<TaskResponseDto> GetTasksByProject(int projectId)
    {
        return _context.Tasks
            .Where(t => t.ProjectId == projectId && !t.IsDeleted)
            .Select(t => new TaskResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                Priority = t.Priority,
                DueDate = t.DueDate,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            })
            .ToList();
    }

    // Get Task By Id
    public TaskResponseDto? GetTaskById(int id)
    {
        return _context.Tasks
            .Where(t => t.Id == id && !t.IsDeleted)
            .Select(t => new TaskResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                Priority = t.Priority,
                DueDate = t.DueDate,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            })
            .FirstOrDefault();
    }

    // Get Tasks By Status
    public List<TaskResponseDto> GetTasksByStatus(TaskItemStatus status)
    {
        return _context.Tasks
            .Where(t => t.Status == status && !t.IsDeleted)
            .Select(t => new TaskResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                Priority = t.Priority,
                DueDate = t.DueDate,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            })
            .ToList();
    }

    // Get Tasks By Priority
    public List<TaskResponseDto> GetTasksByPriority(TaskPriority priority)
    {
        return _context.Tasks
            .Where(t => t.Priority == priority && !t.IsDeleted)
            .Select(t => new TaskResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                Priority = t.Priority,
                DueDate = t.DueDate,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            })
            .ToList();
    }
    
    
    // Get Overdue Tasks
    public List<TaskResponseDto> GetOverdueTasks()
    {
        return _context.Tasks
            .Where(t =>
                !t.IsDeleted &&
                t.DueDate != null &&
                t.DueDate < DateTime.UtcNow &&
                t.Status != TaskItemStatus.Completed)
            .Select(t => new TaskResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                Priority = t.Priority,
                DueDate = t.DueDate,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            })
            .ToList();
    }


    // Update Task
    public TaskResponseDto? UpdateTask(int id, UpdateTaskDto request)
    {
        var task = _context.Tasks
            .FirstOrDefault(t => t.Id == id && !t.IsDeleted);

        if (task == null)
        {
            return null;
        }

        task.Title = request.Title;
        task.Description = request.Description;
        task.Status = request.Status;
        task.Priority = request.Priority;
        task.DueDate = request.DueDate;
        task.UpdatedAt = DateTime.UtcNow;

        _context.SaveChanges();

        return new TaskResponseDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            Priority = task.Priority,
            DueDate = task.DueDate,
            CreatedAt = task.CreatedAt,
            UpdatedAt = task.UpdatedAt
        };
    }
    

    // Soft Delete Task
    public bool DeleteTask(int id)
    {
        var task = _context.Tasks
            .FirstOrDefault(t => t.Id == id && !t.IsDeleted);

        if (task == null)
        {
            return false;
        }

        task.IsDeleted = true;

        _context.SaveChanges();

        return true;
        
    }

}