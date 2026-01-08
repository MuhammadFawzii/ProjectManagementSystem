
using MediatR;
using ProjectManagementSystem.Application.Projects.Dtos;

namespace ProjectManagementSystem.Application.Projects.Queries.GetProjectById.GetProjectByIdV1;

public class GetProjectByIdV1Query(Guid Id):IRequest<ProjectDto>
{
    public Guid Id { get;}=Id;
}
