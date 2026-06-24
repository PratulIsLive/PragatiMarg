namespace PragatiMarg.DTOs;

public class ProjectQueryDto
{
    public string? Search { get; set; }

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 5;

    public string SortBy { get; set; } = "CreatedAt";

    public bool Descending { get; set; } = true;
}