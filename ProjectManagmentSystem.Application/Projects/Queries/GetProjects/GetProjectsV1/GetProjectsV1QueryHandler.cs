using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.Application.Common;
using ProjectManagementSystem.Application.Projects.Dtos;
using ProjectManagementSystem.Domain.Constants;
using ProjectManagementSystem.Domain.Entities;
using ProjectManagementSystem.Domain.IRepositories;
using System.Linq.Expressions;

namespace ProjectManagementSystem.Application.Projects.Queries.GetProjects.GetProjectsV1;

/// <summary>
/// Handler for retrieving a paginated list of projects with optional filtering and sorting.
/// </summary>
public class GetProjectsV1QueryHandler(
    ILogger<GetProjectsV1QueryHandler> logger,
    IGenericRepository<Project> projectRepository,
    IMapper mapper) : IRequestHandler<GetProjectsV1Query, PagedResult<ProjectDto>>
{
    /// <summary>
    /// Handles the GetProjectsQuery request and returns a paged result of projects.
    /// </summary>
    /// <param name="request">The query containing pagination, filtering, and sorting parameters.</param>
    /// <param name="cancellationToken">Cancellation token for async operations.</param>
    /// <returns>A paged result containing project DTOs.</returns>
    public async Task<PagedResult<ProjectDto>> Handle(GetProjectsV1Query request, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Retrieving projects - Page: {PageNumber}, PageSize: {PageSize}, SearchPhrase: {SearchPhrase}, SortBy: {SortBy}, SortDirection: {SortDirection}",
            request.PageNumber==0 ?1: request.PageNumber,
            request.PageSize==0 ? 10 :request.PageSize,
            request.SearchPhrase ?? "None",
            request.SortBy ?? "Name",
            request.SortDirection);

        var sortColumns = new Dictionary<string, Expression<Func<Project, object>>>
        {
            { nameof(Project.Name), p => p.Name },
            { nameof(Project.CreatedAt), p => p.CreatedAt },
            { nameof(Project.ExpectedStartDate), p => p.ExpectedStartDate },
            { nameof(Project.Budget), p => p.Budget },
            { nameof(Project.OwnerId), p => p.OwnerId }
        };

        Expression<Func<Project, string>>[] searchColumns = 
        [
            p => p.Name,
            p => p.Description
        ];

        var (projects, totalCount) = await projectRepository.GetAllMatchingAsync(
            request.SearchPhrase,
            request.PageSize == 0 ? 10 : request.PageSize,
            request.PageNumber == 0 ? 1 : request.PageNumber,
            request.SortBy??"Name",
            request.SortDirection,
            sortColumns,
            searchColumns,
            p => p.Tasks
        );

        var projectDtos = mapper.Map<IEnumerable<ProjectDto>>(projects);
        
        var result = new PagedResult<ProjectDto>(
            projectDtos,
            totalCount,
            request.PageSize,
            request.PageNumber);

        logger.LogInformation(
            "Successfully retrieved {Count} projects out of {TotalCount} total projects",
            projectDtos.Count(),
            totalCount);

        return result;
    }
}
