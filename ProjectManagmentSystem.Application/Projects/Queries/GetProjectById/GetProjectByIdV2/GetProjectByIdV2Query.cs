
using MediatR;
using ProjectManagementSystem.Application.Projects.Dtos;

namespace ProjectManagementSystem.Application.Projects.Queries.GetProjectById.GetProjectByIdV2;

public class GetProjectByIdV2Query(Guid id):IRequest<ProjectCurrencyDto>
{
    public Guid Id { get;}=id;
}
