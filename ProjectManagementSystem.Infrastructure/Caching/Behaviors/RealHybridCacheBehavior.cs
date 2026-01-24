using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.Application.Common.Interfaces;
using System.Text.Json;

public class RealHybridCacheBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICacheableRequest
{
    private readonly IMemoryCache _memoryCache;
    private readonly IDistributedCache _distributedCache;
    private readonly ILogger<RealHybridCacheBehavior<TRequest, TResponse>> _logger;

    public RealHybridCacheBehavior(
        IMemoryCache memoryCache,
        IDistributedCache distributedCache,
        ILogger<RealHybridCacheBehavior<TRequest, TResponse>> logger)
    {
        _memoryCache = memoryCache;
        _distributedCache = distributedCache;
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

        // 1️⃣ Try L1 (Memory)
        if (_memoryCache.TryGetValue(key, out TResponse memoryValue))
        {
            _logger.LogInformation("Cache HIT (L1 - Memory) for {Key}", key);
            return memoryValue;
        }

        _logger.LogDebug("Cache MISS (L1 - Memory) for {Key}", key);

        // 2️⃣ Try L2 (Redis)
        var redisValue = await _distributedCache.GetStringAsync(key, cancellationToken);
        if (redisValue != null)
        {
            _logger.LogInformation("Cache HIT (L2 - Redis) for {Key}", key);

            var deserialized = JsonSerializer.Deserialize<TResponse>(redisValue)!;

            // Put back into L1
            _memoryCache.Set(key, deserialized, duration);

            return deserialized;
        }

        _logger.LogWarning("Cache MISS (L2 - Redis) for {Key}", key);

        // 3️⃣ Fetch from handler
        var response = await next();

        // 4️⃣ Store in both
        _memoryCache.Set(key, response, duration);

        await _distributedCache.SetStringAsync(
            key,
            JsonSerializer.Serialize(response),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = duration
            },
            cancellationToken);

        _logger.LogInformation("Cache SET (L1 + L2) for {Key}", key);

        return response;
    }
}
