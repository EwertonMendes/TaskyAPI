using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text.Json;
using Tasky.Dtos.Response.Error;
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
        List<ValidationErrorDto> validationErrorList = new();

        var exceptionType = ex.GetType();

        if (exceptionType == typeof(BadRequestException))
        {
            status = HttpStatusCode.BadRequest;
        }

        if (exceptionType == typeof(NotFoundException))
        {
            status = HttpStatusCode.NotFound;
        }

        if(exceptionType == typeof(UnauthorizedException))
        {
            status = HttpStatusCode.Unauthorized;
        }

        if (exceptionType == typeof(ValidationException))
        {
            var validationException = ex as ValidationException;

            message = "There are validation errors on your request";

            foreach (var error in validationException.Errors)
            {
                validationErrorList.Add(new ValidationErrorDto
                {
                    Field = error.PropertyName,
                    Message = error.ErrorMessage,
                });
            }

            status = HttpStatusCode.BadRequest;
        }

        var exceptionResult = GetSerializedMessage(message, validationErrorList);

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)status;

        return httpContext.Response.WriteAsync(exceptionResult);
    }

    private static string GetSerializedMessage(string message, List<ValidationErrorDto> validationErrorList)
    {
        if(!validationErrorList.IsNullOrEmpty())
        {
            return JsonSerializer.Serialize(new MultipleErrorsResponseDto
            {
                Message = message,
                Errors = validationErrorList
            });
        }

        return JsonSerializer.Serialize(new SingleErrorResponseDto
        {
            Message = message
        });
    }
}
