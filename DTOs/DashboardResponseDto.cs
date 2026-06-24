namespace PragatiMarg.DTOs;

public class DashboardResponseDto
{
    public int TotalProjects { get; set; }

    public int TodoProjects { get; set; }

    public int InProgressProjects { get; set; }

    public int CompletedProjects { get; set; }

    public int TotalTasks { get; set; }

    public int TodoTasks { get; set; }

    public int InProgressTasks { get; set; }

    public int CompletedTasks { get; set; }
}