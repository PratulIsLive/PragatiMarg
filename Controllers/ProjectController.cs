using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PragatiMarg.DTOs;
using PragatiMarg.Services;
using System.Security.Claims;

namespace PragatiMarg.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProjectController : ControllerBase
{
    private readonly ProjectService _projectService;
    private readonly UserService _userService;

    public ProjectController(
        ProjectService projectService,
        UserService userService)
    {
        _projectService = projectService;
        _userService = userService;
    }

    // Create Project
    [HttpPost]
    public IActionResult CreateProject(CreateProjectDto request)
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;

        if (email == null)
        {
            return Unauthorized();
        }

        var user = _userService.GetUserByEmail(email);

        if (user == null)
        {
            return Unauthorized();
        }

        var project = _projectService.CreateProject(user.Id, request);

        return Ok(project);
    }

    // Get All Projects (Search + Pagination + Sorting)
    [HttpGet]
    public IActionResult GetAllProjects(
        [FromQuery] ProjectQueryDto query)
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;

        if (email == null)
        {
            return Unauthorized();
        }

        var user = _userService.GetUserByEmail(email);

        if (user == null)
        {
            return Unauthorized();
        }

        var projects = _projectService.GetAllProjects(
            user.Id,
            query);

        return Ok(projects);
    }

    // Get Project By Id
    [HttpGet("{id}")]
    public IActionResult GetProjectById(int id)
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;

        if (email == null)
        {
            return Unauthorized();
        }

        var user = _userService.GetUserByEmail(email);

        if (user == null)
        {
            return Unauthorized();
        }

        var project = _projectService.GetProjectById(id, user.Id);

        if (project == null)
        {
            return NotFound();
        }

        return Ok(project);
    }

    // Update Project
    [HttpPut("{id}")]
    public IActionResult UpdateProject(
        int id,
        UpdateProjectDto request)
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;

        if (email == null)
        {
            return Unauthorized();
        }

        var user = _userService.GetUserByEmail(email);

        if (user == null)
        {
            return Unauthorized();
        }

        var project = _projectService.UpdateProject(
            id,
            user.Id,
            request);

        if (project == null)
        {
            return NotFound();
        }

        return Ok(project);
    }

    // Delete Project
    [HttpDelete("{id}")]
    public IActionResult DeleteProject(int id)
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;

        if (email == null)
        {
            return Unauthorized();
        }

        var user = _userService.GetUserByEmail(email);

        if (user == null)
        {
            return Unauthorized();
        }

        var deleted = _projectService.DeleteProject(
            id,
            user.Id);

        if (!deleted)
        {
            return NotFound();
        }

        return Ok(new
        {
            message = "Project deleted successfully."
        });
    }
}