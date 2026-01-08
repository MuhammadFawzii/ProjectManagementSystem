
using Microsoft.AspNetCore.Http.HttpResults;
using ProjectManagementSystem.Domain.Exceptions;

namespace ProjectManagementSystem.API.Middlewares;

/// <summary>
/// Provides middleware for handling exceptions that occur during HTTP request processing, mapping specific exceptions
/// to appropriate HTTP status codes and response messages.
/// </summary>
/// <remarks>This middleware intercepts exceptions thrown by downstream components in the request pipeline. It
/// returns a 404 status code for <see cref="NotFoundException"/>, a 403 status code for <see cref="ForbidException"/>,
/// and a 500 status code for all other unhandled exceptions. Exception messages are logged for diagnostic purposes.
/// Place this middleware early in the pipeline to ensure consistent error responses.</remarks>
/// <param name="logger">The logger used to record exception details and warnings during request handling.</param>
public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException notFoundException)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(notFoundException.Message);
            logger.LogWarning(notFoundException.Message);
        }
        catch (ForbidException forbidException)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Access forbidden");
            logger.LogWarning(forbidException.Message);
        }
        catch (Exception ex) 
        {
            logger.LogError(ex, ex.Message);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Something went wrong");
        }
    }
}
