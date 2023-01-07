namespace Tasky.Dtos.Request.User;

public class UserModificationRequestDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}
