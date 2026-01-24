using MediatR;
using ProjectManagementSystem.Application.Common.Interfaces;
using ProjectManagementSystem.Application.ProjectTasks.Dtos;

namespace ProjectManagementSystem.Application.ProjectTasks.Queries.GetProjectTaskById;
public class GetProjectTaskByIdQuery(Guid taskId,Guid projectId):IRequest<ProjectTaskDto>, ICacheableRequest
{
    public Guid TaskId { get; set; } = taskId;
    public Guid ProjectId { get; set; }= projectId;
    public string CacheKey => $"projecttask:{ProjectId}:{TaskId}";
    public TimeSpan CacheDuration => TimeSpan.FromMinutes(10);
    public string[]? CacheTags => ["projecttask"];
}

