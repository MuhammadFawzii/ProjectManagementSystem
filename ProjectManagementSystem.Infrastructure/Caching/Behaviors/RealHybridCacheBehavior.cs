using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.Application.Common.Interfaces;

public class RealHybridCacheBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICacheableRequest
{
    private readonly HybridCache _cache;
    private readonly ILogger<RealHybridCacheBehavior<TRequest, TResponse>> _logger;

    public RealHybridCacheBehavior(
        HybridCache cache,
        ILogger<RealHybridCacheBehavior<TRequest, TResponse>> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var key = request.CacheKey;
        var duration = request.CacheDuration;

        if (string.IsNullOrEmpty(key) || duration <= TimeSpan.Zero)
        {
            return await next();
        }

        var tags = request.CacheTags ?? [];

        var options = new HybridCacheEntryOptions
        {
            Expiration = duration,
            LocalCacheExpiration = duration
        };

        var response = await _cache.GetOrCreateAsync(
            key,
            async ct =>
            {
                _logger.LogDebug("Cache MISS for {Key}", key);
                return await next();
            },
            options,
            tags,
            cancellationToken);

        _logger.LogInformation("Cache processed for {Key}", key);

        return response;
    }
}
