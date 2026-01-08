using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.Application.Projects.Dtos;
using ProjectManagementSystem.Domain.Entities;
using ProjectManagementSystem.Domain.Exceptions;
using ProjectManagementSystem.Domain.IRepositories;
namespace ProjectManagementSystem.Application.Projects.Queries.GetProjectById.GetProjectByIdV2;

public class GetProjectByIdV2QueryHandler
    (ILogger<GetProjectByIdV2QueryHandler> logger,
    IGenericRepository<Project> projectRepository,
    IMapper mapper) : IRequestHandler<GetProjectByIdV2Query, ProjectCurrencyDto>
{
    public async Task<ProjectCurrencyDto> Handle(GetProjectByIdV2Query request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving project with ID: {ProjectId}", request.Id);
        var project = await projectRepository.GetByIdAsync(request.Id, p => p.Tasks);
        if (project is null)
        {
            logger.LogWarning("Project with ID: {ProjectId} was not found", request.Id);
            throw new NotFoundException(nameof(Project), request.Id.ToString());
        }
        var projectDto = mapper.Map<ProjectCurrencyDto>(project);
        logger.LogInformation("Successfully retrieved project with ID: {ProjectId} with {TaskCount} tasks", 
            project.Id, project.Tasks.Count);
        return projectDto;
    }
}
