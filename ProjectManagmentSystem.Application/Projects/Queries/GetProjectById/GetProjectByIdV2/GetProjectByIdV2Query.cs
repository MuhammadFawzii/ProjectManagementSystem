using MediatR;
using ProjectManagementSystem.Application.Common.Interfaces;
using ProjectManagementSystem.Application.Projects.Dtos;
namespace ProjectManagementSystem.Application.Projects.Queries.GetProjectById.GetProjectByIdV2;

public record GetProjectByIdV2Query(Guid id):IRequest<ProjectCurrencyDto>, ICacheableRequest
{
    public Guid Id { get;}=id;
    public TimeSpan CacheDuration => TimeSpan.FromMinutes(10);
    public string CacheKey => $"project:v2:{Id}";
    public string[] CacheTags => ["projects:v2"];
}
