using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.Application.Common.Interfaces;

namespace ProjectManagementSystem.Infrastructure.Caching.Behaviors;

public class CacheInvalidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICacheInvalidatorRequest
{
    private readonly HybridCache _cache;
    private readonly ILogger<CacheInvalidationBehavior<TRequest, TResponse>> _logger;

    public CacheInvalidationBehavior(
        HybridCache cache,
        ILogger<CacheInvalidationBehavior<TRequest, TResponse>> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Execute the command first
        var response = await next();

        // Invalidate specific cache keys after successful execution
        if (request.CacheKeys?.Length > 0) 
        {
            foreach (var key in request.CacheKeys)
            {
                await _cache.RemoveAsync(key,cancellationToken);
                _logger.LogInformation("Cache invalidated by key: {Key}", key);
            }
        }

        // Invalidate cache by tags after successful execution
        if (request.CacheTags?.Length > 0)
        {
            foreach (var tag in request.CacheTags)
            {
                await _cache.RemoveAsync(tag, cancellationToken);
                _logger.LogInformation("Cache invalidated by tag: {tag}", tag);
            }
        }

        return response;
    }
}
