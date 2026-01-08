using ProjectManagementSystem.Domain.Constants;
namespace ProjectManagementSystem.Domain.Entities;
public class ProjectTask
{
    public Guid Id { get; set; }
    public string Title { get; set; }=null!;
    public string? Description { get; set; }=null!;
    public Guid ProjectId { get; set; }
    public Guid AssignedUserId { get; set; }
    public ProjectTaskStatus Status { get; set; }
    public DateTime CreatedAt { get; set; } 
    public Project Project { get; set; }=null!;

}
