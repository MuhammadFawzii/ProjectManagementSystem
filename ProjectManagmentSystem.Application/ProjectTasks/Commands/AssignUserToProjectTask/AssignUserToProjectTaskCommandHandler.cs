using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.Domain.Entities;
using ProjectManagementSystem.Domain.Exceptions;
using ProjectManagementSystem.Domain.IRepositories;

namespace ProjectManagementSystem.Application.ProjectTasks.Commands.AssignUserToProjectTask;

public class AssignUserToProjectTaskCommandHandler(IMapper mapper, IUnitOfWork unitOfWork,
    ILogger<AssignUserToProjectTaskCommandHandler> logger) : IRequestHandler<AssignUserToProjectTaskCommand>
{
    public async Task Handle(AssignUserToProjectTaskCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Assigning user {UserId} to task {TaskId}", request.UserId, request.TaskId);
        
        var task = await unitOfWork.Repository<ProjectTask>().GetByIdAsync(request.TaskId, t => t.Project)
            ?? throw new NotFoundException(nameof(ProjectTask), request.TaskId.ToString());
        
        // Verify task belongs to the specified project
        if (task.ProjectId != request.ProjectId)
            throw new BusinessRuleException($"Task {request.TaskId} does not belong to project {request.ProjectId}", StatusCodes.Status404NotFound);
        
        // TODO: Add current user authorization check
        // var currentUserId = /* get from claims */;
        // if (task.Project.OwnerId != currentUserId)
        //     throw new BusinessRuleException("Only the project owner can assign users to tasks.", StatusCodes.Status403Forbidden);
        
        if (task.Project.ActualEndDate.HasValue)
            throw new BusinessRuleException("Cannot modify tasks on an ended project.", StatusCodes.Status409Conflict);
        
        task.AssignedUserId = request.UserId;
        
        int count = await unitOfWork.SaveChangesAsync(cancellationToken);
        
        if (count == 0)
        {
            logger.LogError("Failed to assign user to project task. ProjectTask: {@ProjectTask}", task);
            throw new OperationFailedException("assign user to project task", "No changes were saved to the database.");
        }
        
        logger.LogInformation("User {UserId} assigned successfully to task {TaskId}", request.UserId, task.Id);
    }
}
