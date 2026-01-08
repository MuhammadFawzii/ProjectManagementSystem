using MediatR;
using System.Text.Json.Serialization;

namespace ProjectManagementSystem.Application.ProjectTasks.Commands.AssignUserToProjectTask;

public class AssignUserToProjectTaskCommand : IRequest
{
    public Guid UserId { get; set; }
    
    [JsonIgnore]
    public Guid ProjectId { get; private set; }
    
    [JsonIgnore]
    public Guid TaskId { get; private set; }
    
    public AssignUserToProjectTaskCommand() { }
    
    public void SetIds(Guid projectId, Guid taskId)
    {
        ProjectId = projectId;
        TaskId = taskId;
    }
}
