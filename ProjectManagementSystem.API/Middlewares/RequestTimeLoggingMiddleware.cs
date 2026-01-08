
using System.Diagnostics;
namespace ProjectManagementSystem.API.Middlewares;
/// <summary>
/// Middleware for logging the duration of HTTP requests. 
/// if a request takes longer than 4 seconds, it logs the request method, path, and elapsed time.
/// </summary>
/// <param name="logger">  </param>
public class RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var stopwatch = Stopwatch.StartNew();
        await next.Invoke(context);
        stopwatch.Stop();
        if (stopwatch.ElapsedMilliseconds / 1000 > 4)
        {
            logger.LogInformation("Request [{Verb}] at {Path} took {Time} ms ",
                context.Request.Method,
                context.Request.Path,
                stopwatch.ElapsedMilliseconds

                );
        }

    }
}
