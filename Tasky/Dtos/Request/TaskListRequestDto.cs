namespace Tasky.Dtos.Request;

public class TaskListRequestDto
{
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public int UserId { get; set; }
}
