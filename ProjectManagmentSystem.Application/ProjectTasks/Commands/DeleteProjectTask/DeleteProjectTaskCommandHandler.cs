using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.Domain.Entities;
using ProjectManagementSystem.Domain.Exceptions;
using ProjectManagementSystem.Domain.IRepositories;

namespace ProjectManagementSystem.Application.ProjectTasks.Commands.DeleteProjectTask;

public class DeleteProjectTaskCommandHandler(IUnitOfWork unitOfWork,
        ILogger<DeleteProjectTaskCommandHandler> logger) : IRequestHandler<DeleteProjectTaskCommand>
{
    public async Task Handle(DeleteProjectTaskCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting project task with id: {ProjectTaskId}", request.TaskId);
        
        var task = await unitOfWork.Repository<ProjectTask>().GetByIdAsync(request.TaskId, t => t.Project)
            ?? throw new NotFoundException(nameof(ProjectTask), request.TaskId.ToString());

        if (task.ProjectId != request.ProjectId)
            throw new BusinessRuleException($"Task {request.TaskId} does not belong to project {request.ProjectId}", StatusCodes.Status404NotFound);

        if (task.Project.OwnerId != request.UserId)
            throw new BusinessRuleException("Only the project owner can delete the task.", StatusCodes.Status403Forbidden);

        if (task.Project.ActualEndDate.HasValue)
            throw new BusinessRuleException("Cannot delete tasks from an ended project.", StatusCodes.Status409Conflict);

        unitOfWork.Repository<ProjectTask>().DeleteAsync(request.TaskId);
        
        int saveChanges = await unitOfWork.SaveChangesAsync(cancellationToken);
        
        if (saveChanges == 0)
        {
            logger.LogError("Failed to delete project task with ID: {TaskId}. Task: {@ProjectTask}", request.TaskId, task);
            throw new OperationFailedException("delete project task", "No changes were saved to the database.");
        }
        
        logger.LogInformation("Project task deleted successfully with ID: {TaskId}", request.TaskId);
    }
}
