using FluentValidation;
using System.Net;
using System.Text.Json;
using Tasky.Exceptions;

namespace Tasky.Middlewares;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        HttpStatusCode status = HttpStatusCode.InternalServerError;
        string message = ex.Message;

        var exceptionType = ex.GetType();

        if (exceptionType == typeof(BadRequestException))
        {
            status = HttpStatusCode.BadRequest;
        }

        if (exceptionType == typeof(NotFoundException))
        {
            status = HttpStatusCode.NotFound;
        }

        if (exceptionType == typeof(ValidationException))
        {
            var validationException = ex as ValidationException;

            message = string.Join("; ", validationException.Errors);

            status = HttpStatusCode.BadRequest;
        }

        var exceptionResult = JsonSerializer.Serialize(new
        {
            error = message
        });

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)status;

        return httpContext.Response.WriteAsync(exceptionResult);
    }
}
