using System.ComponentModel.DataAnnotations.Schema;

namespace Tasky.Dtos.Response;

public class TaskListResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CategoryResponseDto Category { get; set; }
}
