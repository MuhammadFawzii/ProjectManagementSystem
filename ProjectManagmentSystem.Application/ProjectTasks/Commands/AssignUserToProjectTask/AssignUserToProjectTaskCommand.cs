using MediatR;
using ProjectManagementSystem.Application.Common.Interfaces;
using System.Text.Json.Serialization;

namespace ProjectManagementSystem.Application.ProjectTasks.Commands.AssignUserToProjectTask;

public class AssignUserToProjectTaskCommand : IRequest, ICacheInvalidatorRequest
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
    } ; 
}
