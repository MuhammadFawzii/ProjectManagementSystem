using MediatR;
using ProjectManagementSystem.Application.ProjectTasks.Dtos;
using ProjectManagementSystem.Domain.Constants;
using System.Text.Json.Serialization;

namespace ProjectManagementSystem.Application.ProjectTasks.Commands.UpdateProjectTask;

public class UpdateProjectTaskCommand : IRequest<ProjectTaskDto>
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public ProjectTaskStatus Status { get; set; }
    
    [JsonIgnore]
    public Guid ProjectId { get; private set; }
    
    [JsonIgnore]
    public Guid TaskId { get; private set; }
    
    [JsonIgnore]
    public Guid CurrentUserId { get; private set; }
    
    public UpdateProjectTaskCommand() { }
    
    public void SetIds(Guid projectId, Guid taskId, Guid currentUserId)
    {
        ProjectId = projectId;
        TaskId = taskId;
        CurrentUserId = currentUserId;
    }
}
