using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.Application.Projects.Dtos;
using ProjectManagementSystem.Domain.Entities;
using ProjectManagementSystem.Domain.Exceptions;
using ProjectManagementSystem.Domain.IRepositories;
namespace ProjectManagementSystem.Application.Projects.Queries.GetProjectById.GetProjectByIdV1;

public class GetProjectByIdV1QueryHandler
    (ILogger<GetProjectByIdV1QueryHandler> logger,
    IGenericRepository<Project> projectRepository,
    IMapper mapper) : IRequestHandler<GetProjectByIdV1Query, ProjectDto>
{
    public async Task<ProjectDto> Handle(GetProjectByIdV1Query request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving project with ID: {ProjectId}", request.Id);
        var project = await projectRepository.GetByIdAsync(request.Id, p => p.Tasks);
        if (project is null)
        {
            logger.LogWarning("Project with ID: {ProjectId} was not found", request.Id);
            throw new NotFoundException(nameof(Project), request.Id.ToString());
        }
        var projectDto = mapper.Map<ProjectDto>(project);
        logger.LogInformation("Successfully retrieved project with ID: {ProjectId} with {TaskCount} tasks", 
            project.Id, project.Tasks.Count);
        return projectDto;
    }
}
