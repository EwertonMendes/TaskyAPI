using Tasky.Middlewares;

namespace Tasky.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder builder) 
        => builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
}
