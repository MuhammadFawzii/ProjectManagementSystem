using Microsoft.Extensions.Caching.Hybrid;
using ProjectManagementSystem.Application.Common.Interfaces;

namespace ProjectManagementSystem.Infrastructure.Caching.Services;

public class RedisCacheInvalidator : ICacheInvalidator
{
    private readonly HybridCache _cache;

    public RedisCacheInvalidator(HybridCache cache)
    {
        _cache = cache;
    }

    public async Task InvalidateCacheAsync(string cacheKey, CancellationToken cancellationToken = default)
    {
        await _cache.RemoveAsync(cacheKey, cancellationToken);
    }
}
