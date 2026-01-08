using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.Domain.Entities;
using ProjectManagementSystem.Domain.Exceptions;
using ProjectManagementSystem.Domain.IRepositories;

namespace ProjectManagementSystem.Application.Projects.Commands.CreateProject;

public class CreateProjectCommandHandler (IMapper mapper, IUnitOfWork unitOfWork,
    ILogger<CreateProjectCommandHandler> logger) : IRequestHandler<CreateProjectCommand, Guid>
{
    public async Task<Guid> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a new project with name: {ProjectName}", request.Name);

        var project = mapper.Map<Project>(request);
        project.Id = Guid.NewGuid();
        project.OwnerId = Guid.NewGuid(); // TODO: Replace with actual authenticated user ID
        project.CreatedAt = DateTime.UtcNow;
        project.ActualEndDate = null;

        unitOfWork.Repository<Project>().AddAsync(project);
        
        int count = await unitOfWork.SaveChangesAsync(cancellationToken);
        
        if(count == 0)
        {
            logger.LogError("Failed to create project. Project: {@Project}", project);
            throw new OperationFailedException("create project", "No changes were saved to the database.");
        }
        
        logger.LogInformation("Project created successfully with ID: {ProjectId}", project.Id);
        return project.Id;
    }
}
