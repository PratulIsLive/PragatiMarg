namespace PragatiMarg.Models;

public class Project
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public ProjectStatus Status { get; set; } = ProjectStatus.Todo;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; } = false;

    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public List<TaskItem> Tasks { get; set; } = new();
}