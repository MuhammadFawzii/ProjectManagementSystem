using MediatR;
using ProjectManagementSystem.Application.Common;
using ProjectManagementSystem.Application.Projects.Dtos;
using ProjectManagementSystem.Domain.Constants;

namespace ProjectManagementSystem.Application.Projects.Queries.GetProjects.GetProjectsV2;

public class GetProjectsV2Query: IRequest<PagedResult<ProjectCurrencyDto>>
{
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}
