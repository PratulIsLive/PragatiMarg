using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PragatiMarg.DTOs;
using PragatiMarg.Models;
using PragatiMarg.Services;

namespace PragatiMarg.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TaskController : ControllerBase
{
    private readonly TaskService _taskService;

    public TaskController(TaskService taskService)
    {
        _taskService = taskService;
    }

    // Create Task
    [HttpPost("project/{projectId}")]
    public IActionResult CreateTask(int projectId, CreateTaskDto request)
    {
        var task = _taskService.CreateTask(projectId, request);

        return Ok(task);
    }

    // Get All Tasks Of A Project
    [HttpGet("project/{projectId}")]
    public IActionResult GetTasksByProject(int projectId)
    {
        var tasks = _taskService.GetTasksByProject(projectId);

        return Ok(tasks);
    }

    // Get Task By Id
    [HttpGet("{id}")]
    public IActionResult GetTaskById(int id)
    {
        var task = _taskService.GetTaskById(id);

        if (task == null)
        {
            return NotFound();
        }

        return Ok(task);
    }

    // Get Tasks By Status
    [HttpGet("status/{status}")]
    public IActionResult GetTasksByStatus(TaskItemStatus status)
    {
        var tasks = _taskService.GetTasksByStatus(status);

        return Ok(tasks);
    }

    // Get Tasks By Priority
    [HttpGet("priority/{priority}")]
    public IActionResult GetTasksByPriority(TaskPriority priority)
    {
        var tasks = _taskService.GetTasksByPriority(priority);

        return Ok(tasks);
    }

    // Update Task
    [HttpPut("{id}")]
    public IActionResult UpdateTask(int id, UpdateTaskDto request)
    {
        var task = _taskService.UpdateTask(id, request);

        if (task == null)
        {
            return NotFound();
        }

        return Ok(task);
    }

    // Delete Task
    [HttpDelete("{id}")]
    public IActionResult DeleteTask(int id)
    {
        var deleted = _taskService.DeleteTask(id);

        if (!deleted)
        {
            return NotFound();
        }

        return Ok(new
        {
            message = "Task deleted successfully."
        });
        
    }

    // Get Overdue Tasks
    [HttpGet("overdue")]
    public IActionResult GetOverdueTasks()
    {
        var tasks = _taskService.GetOverdueTasks();

        return Ok(tasks);
    } 
}