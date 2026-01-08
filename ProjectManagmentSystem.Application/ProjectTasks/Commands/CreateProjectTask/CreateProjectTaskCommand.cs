using MediatR;
using ProjectManagementSystem.Application.ProjectTasks.Dtos;
using System.Text.Json.Serialization;
namespace ProjectManagementSystem.Application.ProjectTasks.Commands.CreateProjectTask;
public class CreateProjectTaskCommand : IRequest<ProjectTaskDto>
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public Guid? AssignedUserId { get; set; }
    [JsonIgnore]
    public Guid ProjectId { get; set; }
    [JsonIgnore]
    public Guid CurrentUserId { get; set; }
}
