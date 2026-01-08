using MediatR;
using ProjectManagementSystem.Application.ProjectTasks.Dtos;

namespace ProjectManagementSystem.Application.ProjectTasks.Queries.GetProjectTaskById;
public class GetProjectTaskByIdQuery(Guid taskId,Guid projectId):IRequest<ProjectTaskDto>
{
    public Guid TaskId { get; set; } = taskId;
    public Guid ProjectId { get; set; }= projectId;
}
