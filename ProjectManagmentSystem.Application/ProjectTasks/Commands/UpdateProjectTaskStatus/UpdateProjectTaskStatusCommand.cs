using MediatR;
using ProjectManagementSystem.Application.Common.Interfaces;
using ProjectManagementSystem.Domain.Constants;
using System.Text.Json.Serialization;

namespace ProjectManagementSystem.Application.ProjectTasks.Commands.UpdateProjectTaskStatus;

public class UpdateProjectTaskStatusCommand : IRequest, ICacheInvalidatorRequest
{
    public ProjectTaskStatus Status { get; set; }
    
    [JsonIgnore]
    public Guid ProjectId { get; private set; }
    
    [JsonIgnore]
    public Guid TaskId { get; private set; }

    public string[]? CacheTags => new[]
    {
        $"projects:v1",
        $"projects:v2",
    };
    public string[]? CacheKeys => new[]
    {
        $"projecttask:{ProjectId}:{TaskId}",
        $"project:{ProjectId}",
        $"project:v2:{ProjectId}",
    };

    public UpdateProjectTaskStatusCommand() { }
    
    public void SetIds(Guid projectId, Guid taskId)
    {
        ProjectId = projectId;
        TaskId = taskId;
    }

    
}

