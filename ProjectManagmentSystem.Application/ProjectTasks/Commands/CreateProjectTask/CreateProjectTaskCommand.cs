using MediatR;
using ProjectManagementSystem.Application.Common.Interfaces;
using ProjectManagementSystem.Application.ProjectTasks.Dtos;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
namespace ProjectManagementSystem.Application.ProjectTasks.Commands.CreateProjectTask;
public class CreateProjectTaskCommand : IRequest<ProjectTaskDto>, ICacheInvalidatorRequest
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public Guid? AssignedUserId { get; set; }
    [JsonIgnore]
    public Guid ProjectId { get; set; }
    [JsonIgnore]
    public Guid CurrentUserId { get; set; }
    public string[]? CacheTags => new[]
    {
        $"projects:v1",
        $"projects:v2",
    };
    public string[]? CacheKeys => new[]
    {
        $"project:{ProjectId}",
        $"project:v2:{ProjectId}",
    };
}
