using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.Application.ProjectTasks.Dtos;
using ProjectManagementSystem.Domain.Entities;
using ProjectManagementSystem.Domain.Exceptions;
using ProjectManagementSystem.Domain.IRepositories;

namespace ProjectManagementSystem.Application.ProjectTasks.Commands.UpdateProjectTask;

public class UpdateProjectTaskCommandHandler(IMapper mapper, IUnitOfWork unitOfWork,
    ILogger<UpdateProjectTaskCommandHandler> logger) : IRequestHandler<UpdateProjectTaskCommand, ProjectTaskDto>
{
    public async Task<ProjectTaskDto> Handle(UpdateProjectTaskCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating project task with title: {TaskTitle}, TaskId: {TaskId}", request.Title, request.TaskId);
        
        var task = await unitOfWork.Repository<ProjectTask>().GetByIdAsync(request.TaskId, t => t.Project)
            ?? throw new NotFoundException(nameof(ProjectTask), request.TaskId.ToString());

        if (task.ProjectId != request.ProjectId)
            throw new BusinessRuleException($"Task {request.TaskId} does not belong to project {request.ProjectId}", StatusCodes.Status404NotFound);

        if (task.AssignedUserId != request.CurrentUserId && task.Project.OwnerId != request.CurrentUserId)
            throw new BusinessRuleException("Only assigned user or project owner can update the task.", StatusCodes.Status403Forbidden);

        if (task.Project.ActualEndDate.HasValue)
            throw new BusinessRuleException("Cannot update tasks in an ended project.", StatusCodes.Status409Conflict);

        task.Title = request.Title;
        task.Description = request.Description;
        task.Status = request.Status;
        
        unitOfWork.Repository<ProjectTask>().UpdateAsync(task);

        int count = await unitOfWork.SaveChangesAsync(cancellationToken);

        if (count == 0)
        {
            logger.LogError("Failed to update project task. ProjectTask: {@ProjectTask}", task);
            throw new OperationFailedException("update project task", "No changes were saved to the database.");
        }
        
        logger.LogInformation("Project task updated successfully with ID: {TaskId}", task.Id);
        return mapper.Map<ProjectTaskDto>(task);
    }
}
