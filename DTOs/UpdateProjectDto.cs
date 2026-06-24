using System.ComponentModel.DataAnnotations;
using PragatiMarg.Models;

namespace PragatiMarg.DTOs;

public class UpdateProjectDto
{
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public string Description { get; set; } = string.Empty;

    public ProjectStatus Status { get; set; }
}