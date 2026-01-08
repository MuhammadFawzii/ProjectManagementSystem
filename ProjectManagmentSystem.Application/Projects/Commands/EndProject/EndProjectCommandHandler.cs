

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.Application.Projects.Commands.CreateProject;
using ProjectManagementSystem.Domain.Constants;
using ProjectManagementSystem.Domain.Entities;
using ProjectManagementSystem.Domain.Exceptions;
using ProjectManagementSystem.Domain.IRepositories;

namespace ProjectManagementSystem.Application.Projects.Commands.EndProject;

public class EndProjectCommandHandler(IMapper mapper, IUnitOfWork unitOfWork,
    ILogger<CreateProjectCommandHandler> logger) : IRequestHandler<EndProjectCommand>
{
    async Task IRequestHandler<EndProjectCommand>.Handle(EndProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await unitOfWork.Repository<Project>().GetByIdAsync(request.ProjectId, includes: p => p.Tasks);
        if (project == null)
        {
            logger.LogError("Failed to Get project. Project: {@Project}", project);
            throw new OperationFailedException("Get project");
        }
        var currentUserId = project.OwnerId;
        if (project.OwnerId != currentUserId)
        {
            logger.LogError("the current user cant end this project");
            throw new BusinessRuleException("Only the project owner can end the project.", StatusCodes.Status403Forbidden);
        }
        if (project.ActualEndDate.HasValue)
            throw new BusinessRuleException("Project is already ended.", StatusCodes.Status409Conflict);
        var allTasksClosed = project.Tasks.All(t =>
                    t.Status == ProjectTaskStatus.Completed || t.Status == ProjectTaskStatus.Cancelled);
        if (!allTasksClosed)
            throw new BusinessRuleException("Cannot end project with active tasks.", StatusCodes.Status409Conflict);
        project.ActualEndDate = DateTime.UtcNow;
        int count = await unitOfWork.SaveChangesAsync(cancellationToken);
        if (count == 0)
        {
            logger.LogError("Failed to End project. Project: {@Project}", project);
            throw new OperationFailedException("End project", "No changes were saved to the database.");
        }
    }
}
