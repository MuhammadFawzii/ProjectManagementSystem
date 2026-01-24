using MediatR;
using ProjectManagementSystem.Application.Common.Interfaces;
using System.Text.Json.Serialization;

namespace ProjectManagementSystem.Application.Projects.Commands.UpdateProject;

public record UpdateProjectCommand : IRequest, ICacheInvalidatorRequest
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public DateTime ExpectedStartDate { get; init; }
    [JsonIgnore]
    public string[] CacheKeys => [$"project:{Id}", $"project:v2:{Id}"];
    [JsonIgnore]
    public string[] CacheTags => ["projects:v1", "projects:v2"];
}

