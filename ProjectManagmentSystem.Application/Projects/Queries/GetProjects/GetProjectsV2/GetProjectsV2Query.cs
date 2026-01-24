using MediatR;
using ProjectManagementSystem.Application.Common;
using ProjectManagementSystem.Application.Common.Interfaces;
using ProjectManagementSystem.Application.Projects.Dtos;
using ProjectManagementSystem.Domain.Constants;
using System.Text;
namespace ProjectManagementSystem.Application.Projects.Queries.GetProjects.GetProjectsV2;
public record GetProjectsV2Query: IRequest<PagedResult<ProjectCurrencyDto>>, ICacheableRequest
{
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
    public TimeSpan CacheDuration => TimeSpan.FromMinutes(5);
    public string CacheKey
    {
        get
        {
            var sb = new StringBuilder("projects:v2");
            if (!string.IsNullOrWhiteSpace(SearchPhrase))
                sb.Append($":search={SearchPhrase}");
            sb.Append($":page={PageNumber}");
            sb.Append($":size={PageSize}");
            if (!string.IsNullOrWhiteSpace(SortBy))
                sb.Append($":sort={SortBy}:{SortDirection}");
            return sb.ToString();
        }
    }
    public string[]? CacheTags => ["projects:v2"];

}
