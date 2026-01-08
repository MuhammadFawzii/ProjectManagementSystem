using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.Application.ProjectTasks.Commands.CreateProjectTask;
using ProjectManagementSystem.Domain.Entities;
using ProjectManagementSystem.Domain.Exceptions;
using ProjectManagementSystem.Domain.IRepositories;

namespace ProjectManagementSystem.Application.ProjectTasks.Commands.UpdateProjectTaskStatus;

public class UpdateProjectTaskStatusCommandHandler(IMapper mapper, IUnitOfWork unitOfWork,
    ILogger<UpdateProjectTaskStatusCommandHandler> logger) : IRequestHandler<UpdateProjectTaskStatusCommand>
{
    public async Task Handle(UpdateProjectTaskStatusCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating project task status for TaskId: {TaskId}", request.TaskId);
        
        var task = await unitOfWork.Repository<ProjectTask>().GetByIdAsync(request.TaskId, t => t.Project)
                ?? throw new NotFoundException(nameof(ProjectTask), request.TaskId.ToString());
        
        if(task.ProjectId != request.ProjectId)
            throw new BusinessRuleException($"Task {request.TaskId} does not belong to project {request.ProjectId}", StatusCodes.Status404NotFound);
        
        // TODO: Add current user authorization check
        // var currentUserId = /* get from claims or request */;
        // if(task.AssignedUserId != currentUserId && task.Project.OwnerId != currentUserId)
        //     throw new BusinessRuleException("Only assigned user or project owner can update task status.", StatusCodes.Status403Forbidden);
        
        if (task.Project.ActualEndDate.HasValue)
            throw new BusinessRuleException("Cannot modify tasks on an ended project.", StatusCodes.Status409Conflict);
        
        task.Status = request.Status;
        
        int count = await unitOfWork.SaveChangesAsync(cancellationToken);
        
        if (count == 0)
        {
            logger.LogError("Failed to update project task status. ProjectTask: {@ProjectTask}", task);
            throw new OperationFailedException("update project task status", "No changes were saved to the database.");
        }
        
        logger.LogInformation("Project task status updated successfully for TaskId: {TaskId}, New Status: {Status}", task.Id, request.Status);
    }
}
