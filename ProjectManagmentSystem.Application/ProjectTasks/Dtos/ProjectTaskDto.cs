
namespace ProjectManagementSystem.Application.ProjectTasks.Dtos;
public class ProjectTaskDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public Guid AssignedUserId { get; set; }
    public string Status { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
