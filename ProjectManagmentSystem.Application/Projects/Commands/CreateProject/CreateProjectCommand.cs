
using MediatR;

namespace ProjectManagementSystem.Application.Projects.Commands.CreateProject;

public class CreateProjectCommand:IRequest<Guid>
{
    public string Name { get; set; }=null!;
    public string? Description { get; set; }=null!;
    public DateTime ExpectedStartDate { get; set; }
    public decimal Budget { get; set; }
}
