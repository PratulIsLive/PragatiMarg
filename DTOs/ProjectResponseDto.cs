using PragatiMarg.Models;
public class ProjectResponseDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public ProjectStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public double CompletionPercentage { get; set; }

    public int TotalTasks { get; set; }

    public int CompletedTasks { get; set; }

    public int PendingTasks { get; set; }
    
}