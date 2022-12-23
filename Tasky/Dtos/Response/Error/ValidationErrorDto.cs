namespace Tasky.Dtos.Response.Error;

public class ValidationErrorDto
{
    public string Field { get; set; }

    public string Message { get; set; }
}