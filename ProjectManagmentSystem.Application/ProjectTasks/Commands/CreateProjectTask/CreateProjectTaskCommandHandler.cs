using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.Application.ProjectTasks.Dtos;
using ProjectManagementSystem.Domain.Constants;
using ProjectManagementSystem.Domain.Entities;
using ProjectManagementSystem.Domain.Exceptions;
using ProjectManagementSystem.Domain.IRepositories;

namespace ProjectManagementSystem.Application.ProjectTasks.Commands.CreateProjectTask;

public class CreateProjectTaskCommandHandler (IMapper mapper, IUnitOfWork unitOfWork,
    ILogger<CreateProjectTaskCommandHandler> logger) : IRequestHandler<CreateProjectTaskCommand, ProjectTaskDto>
{
    public async Task<ProjectTaskDto> Handle(CreateProjectTaskCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a new project task take with title: {TaskTitle}", request.Title);
        var project = await unitOfWork.Repository<Project>().GetByIdAsync(request.ProjectId)
         ?? throw new NotFoundException(nameof(Project),request.ProjectId.ToString());

        if (project.OwnerId != request.CurrentUserId)
            throw new BusinessRuleException("Only the project owner can create tasks.", StatusCodes.Status403Forbidden);

        if (project.ActualEndDate.HasValue)
            throw new BusinessRuleException("Cannot modify tasks on an ended project.", StatusCodes.Status409Conflict);

        var task = mapper.Map<ProjectTask>(request);
        task.CreatedAt = DateTime.UtcNow;
        task.Status = ProjectTaskStatus.NotStarted;
        unitOfWork.Repository<ProjectTask>().AddAsync(task);

        int count = await unitOfWork.SaveChangesAsync(cancellationToken);

        if(count == 0)
        {
            logger.LogError("Failed to create project task. ProjectTask: {@ProjectTask}", task);
            throw new OperationFailedException("create project task", "No changes were saved to the database.");
        }
        
        logger.LogInformation("Project task created successfully with ID: {ProjectId}", task.Id);
        var taskDto = mapper.Map<ProjectTaskDto>(task);
        return taskDto;
    }
}
