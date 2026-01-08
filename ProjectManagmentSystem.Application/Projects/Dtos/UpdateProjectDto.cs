

namespace ProjectManagementSystem.Application.Projects.Dtos;

public class UpdateProjectDto
{
    public string? Name { get; set; } = string.Empty;
    public string? Description { get; set; }= string.Empty;
    public DateTime ExpectedStartDate { get; set; }
}
