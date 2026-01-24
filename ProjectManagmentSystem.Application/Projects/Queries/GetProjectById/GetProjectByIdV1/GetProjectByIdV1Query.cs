using MediatR;
using ProjectManagementSystem.Application.Common.Interfaces;
using ProjectManagementSystem.Application.Projects.Dtos;
using ProjectManagementSystem.Domain.Entities;

namespace ProjectManagementSystem.Application.Projects.Queries.GetProjectById.GetProjectByIdV1;

public record GetProjectByIdV1Query(Guid Id):IRequest<ProjectDto>, ICacheableRequest
{
    public Guid Id { get;}=Id;
    public TimeSpan CacheDuration => TimeSpan.FromMinutes(10);
    public string CacheKey => $"project:{Id}";
    public string[] CacheTags => ["projects:v1"];
}
