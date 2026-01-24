using MediatR;
using ProjectManagementSystem.Application.Common;
using ProjectManagementSystem.Application.Common.Interfaces;
using ProjectManagementSystem.Application.Projects.Dtos;
using ProjectManagementSystem.Domain.Constants;
using System.Text;

namespace ProjectManagementSystem.Application.Projects.Queries.GetProjects.GetProjectsV1;

public record GetProjectsV1Query : IRequest<PagedResult<ProjectDto>>, ICacheableRequest
{
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; } = SortDirection.Ascending;
    public TimeSpan CacheDuration => TimeSpan.FromMinutes(5);
    public string CacheKey
    {
        get
        {
            var sb = new StringBuilder("projects:v1");
            if (!string.IsNullOrWhiteSpace(SearchPhrase))
                sb.Append($":search={SearchPhrase}");
            sb.Append($":page={PageNumber}");
            sb.Append($":size={PageSize}");
            if (!string.IsNullOrWhiteSpace(SortBy))
                sb.Append($":sort={SortBy}:{SortDirection}");
            return sb.ToString();
        }
    }
    public string[]? CacheTags => ["projects:v1"];
}
