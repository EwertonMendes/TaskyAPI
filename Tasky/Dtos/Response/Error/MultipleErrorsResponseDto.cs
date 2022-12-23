namespace Tasky.Dtos.Response.Error;

public class MultipleErrorsResponseDto
{
    public string Message { get; set; }
    public List<ValidationErrorDto>? Errors { get; set; }
}
