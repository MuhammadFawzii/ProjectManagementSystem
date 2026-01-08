
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.Application.Projects.Dtos;
using ProjectManagementSystem.Application.Projects.Queries.GetProjectById.GetProjectByIdV1;
using ProjectManagementSystem.Application.ProjectTasks.Dtos;
using ProjectManagementSystem.Domain.Entities;
using ProjectManagementSystem.Domain.Exceptions;
using ProjectManagementSystem.Domain.IRepositories;

namespace ProjectManagementSystem.Application.ProjectTasks.Queries.GetProjectTaskById;

public class GetProjectTaskByIdQueryHandler(ILogger<GetProjectByIdV1QueryHandler> logger,
    IGenericRepository<ProjectTask> taskRepository,
    IMapper mapper) : IRequestHandler<GetProjectTaskByIdQuery, ProjectTaskDto>
{
    public async Task<ProjectTaskDto> Handle(GetProjectTaskByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving project task with ID: {TaskId}", request.TaskId);
        var task = await taskRepository.GetByIdAsync(request.TaskId, x=>x.Project.Id==request.ProjectId);
        if (task is null)
        {
            logger.LogWarning("Task with ID: {TaskId} in project id {ProjectId} was not found ", request.TaskId,request.ProjectId);
            throw new NotFoundException(nameof(ProjectTask), request.TaskId.ToString());
        }
        var taskDto = mapper.Map<ProjectTaskDto>(task);
        logger.LogInformation("Successfully retrieved project task with ID: {TaskId}",
            task.Id);
        return taskDto;
    }
}
