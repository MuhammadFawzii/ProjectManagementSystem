using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.Domain.Entities;
using ProjectManagementSystem.Domain.Exceptions;
using ProjectManagementSystem.Domain.IRepositories;

namespace ProjectManagementSystem.Application.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommandHandler(IMapper mapper, IUnitOfWork unitOfWork,
        ILogger<UpdateProjectCommandHandler> logger) : IRequestHandler<UpdateProjectCommand>
    {
        public async Task Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating Project with id: {ProjectId} with {@UpdateProject}", request.Id, request);
            
            var project = await unitOfWork.Repository<Project>().GetByIdAsync(request.Id);
            
            if (project == null)
            {
                throw new NotFoundException(nameof(Project), request.Id.ToString());
            }

            mapper.Map(request, project);            
            int saveChanges = await unitOfWork.SaveChangesAsync(cancellationToken);
            if (saveChanges == 0)
            {
                logger.LogError("Failed to update project with ID: {ProjectId}. Project: {@Project}", request.Id, project);
                throw new OperationFailedException("update project", "No changes were saved to the database.");
            }
            
            logger.LogInformation("Project updated successfully with ID: {ProjectId}", request.Id);
        }
    }
}
