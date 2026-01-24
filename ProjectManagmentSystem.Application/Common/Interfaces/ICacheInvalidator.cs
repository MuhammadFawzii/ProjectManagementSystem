
namespace ProjectManagementSystem.Application.Common.Interfaces;

public interface ICacheInvalidator
{
    Task InvalidateCacheAsync(string cacheKey, CancellationToken cancellationToken = default);
}

