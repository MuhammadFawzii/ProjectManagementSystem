using MediatR;
using ProjectManagementSystem.Domain.Constants;
using System.Text.Json.Serialization;

namespace ProjectManagementSystem.Application.ProjectTasks.Commands.UpdateProjectTaskStatus;

public class UpdateProjectTaskStatusCommand : IRequest
{
    public ProjectTaskStatus Status { get; set; }
    
    [JsonIgnore]
    public Guid ProjectId { get; private set; }
    
    [JsonIgnore]
    public Guid TaskId { get; private set; }
    
    public UpdateProjectTaskStatusCommand() { }
    
    public void SetIds(Guid projectId, Guid taskId)
    {
        ProjectId = projectId;
        TaskId = taskId;
    }
}
