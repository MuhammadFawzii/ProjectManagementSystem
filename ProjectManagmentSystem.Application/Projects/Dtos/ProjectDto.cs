using ProjectManagementSystem.Application.ProjectTasks.Dtos;

namespace ProjectManagementSystem.Application.Projects.Dtos;
public class ProjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public Guid OwnerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpectedStartDate { get; set; }
    public DateTime? ActualEndDate { get; set; }
    public decimal Budget { get; set; }
    public ICollection<ProjectTaskDto> Tasks { get; set; } = new List<ProjectTaskDto>();
}
