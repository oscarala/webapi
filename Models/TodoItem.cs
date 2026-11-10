using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class TodoItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsDone { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public record CreateTodoDto(
    [Required, StringLength(200, MinimumLength = 1)] string Title);

public record UpdateTodoDto(
    [Required, StringLength(200, MinimumLength = 1)] string Title,
    bool IsDone);
