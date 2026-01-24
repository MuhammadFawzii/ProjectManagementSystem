using MediatR;
using ProjectManagementSystem.Application.Common.Interfaces;
using System.Text.Json.Serialization;

namespace ProjectManagementSystem.Application.Projects.Commands.CreateProject;

public class CreateProjectCommand:IRequest<Guid>, ICacheInvalidatorRequest
{
    public string Name { get; set; }=null!;
    public string? Description { get; set; }=null!;
    public DateTime ExpectedStartDate { get; set; }
    public decimal Budget { get; set; }

    [JsonIgnore]
    public string[] CacheKeys => [];
    [JsonIgnore]
    public string[] CacheTags => ["projects:v1", "projects:v2"];
}
