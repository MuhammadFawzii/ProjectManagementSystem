namespace ProjectManagementSystem.Application.Common.Interfaces;

public interface ICacheInvalidatorRequest
{
    string[]? CacheKeys { get; }
    string[]? CacheTags { get; }
}
