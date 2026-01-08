using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.Application.Projects.Commands.UpdateProject;
using ProjectManagementSystem.Domain.Entities;
using ProjectManagementSystem.Domain.Exceptions;
using ProjectManagementSystem.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Projects.Commands.DeleteProject;

public class DeleteProjectCommandHandler(IUnitOfWork unitOfWork,
        ILogger<DeleteProjectCommandHandler> logger) : IRequestHandler<DeleteProjectCommand>
{
    public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting Project with id: {ProjectId} with {@DeleteProject}", request.Id, request);

        var project = await unitOfWork.Repository<Project>().GetByIdAsync(request.Id);

        if (project == null)
        {
            throw new NotFoundException(nameof(Project), request.Id.ToString());
        }
        unitOfWork.Repository<Project>().DeleteAsync(request.Id);
        int saveChanges = await unitOfWork.SaveChangesAsync(cancellationToken);
        if (saveChanges == 0)
        {
            logger.LogError("Failed to delete project with ID: {ProjectId}. Project: {@Project}", request.Id, project);
            throw new OperationFailedException("delete project", "No changes were saved to the database.");
        }

        logger.LogInformation("Project deleted successfully with ID: {ProjectId}", request.Id);

    }
}
