using System.ComponentModel.DataAnnotations;
using PragatiMarg.Models;

namespace PragatiMarg.DTOs;

public class CreateTaskDto
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    public TaskPriority Priority { get; set; }

    public DateTime? DueDate { get; set; }
    
}