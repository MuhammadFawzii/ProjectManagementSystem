using System;

namespace ProjectManagementSystem.Application.Common.Interfaces;
/// <summary>
/// Implement this interface for MediatR queries that should be cached.
/// </summary>
public interface ICacheableRequest
{
    /// <summary>
    /// The key used to store/retrieve the result from cache.
    /// </summary>
    string CacheKey { get; }

    /// <summary>
    /// How long the result should stay in cache.
    /// </summary>
    TimeSpan CacheDuration { get; }

    /// <summary>
    /// Optional tags for cache invalidation.
    /// </summary>
    string[]? CacheTags { get; }
}
