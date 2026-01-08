using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementSystem.Application.Common;
using ProjectManagementSystem.Application.Projects.Dtos;
using ProjectManagementSystem.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Projects.Queries.GetProjects.GetProjectsV1;

public class GetProjectsV1Query:IRequest<PagedResult<ProjectDto>>
{
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}
