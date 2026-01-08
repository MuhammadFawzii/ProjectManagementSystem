using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.Domain.Entities;
using ProjectManagementSystem.Domain.Exceptions;
using ProjectManagementSystem.Domain.IRepositories;

namespace ProjectManagementSystem.Application.Projects.Commands.UpdateProjectBudget
{
    public class UpdateProjectBudgetCommandHandler(IMapper mapper, IUnitOfWork unitOfWork,
        ILogger<UpdateProjectBudgetCommandHandler> logger) : IRequestHandler<UpdateProjectBudgetCommand>
    {
        public async Task Handle(UpdateProjectBudgetCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating Project Budget with id: {ProjectId} with {@UpdateProjectBudget}", request.Id, request);
            
            var project = await unitOfWork.Repository<Project>().GetByIdAsync(request.Id);
            
            if (project == null)
            {
                throw new NotFoundException(nameof(Project), request.Id.ToString());
            }

            mapper.Map(request, project);            
            int saveChanges = await unitOfWork.SaveChangesAsync(cancellationToken);
            if (saveChanges == 0)
            {
                logger.LogError("Failed to update project Budget with ID: {ProjectId}. Project: {@Project}", request.Id, project);
                throw new OperationFailedException("update project Budget", "No changes were saved to the database.");
            }
            
            logger.LogInformation("Project Budget updated successfully with ID: {ProjectId}", request.Id);
        }
    }
}
