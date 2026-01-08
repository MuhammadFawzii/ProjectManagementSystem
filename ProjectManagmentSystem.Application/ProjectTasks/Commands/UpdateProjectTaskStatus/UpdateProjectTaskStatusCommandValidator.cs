using FluentValidation;
using ProjectManagementSystem.Application.ProjectTasks.Commands.UpdateProjectTask;
using ProjectManagementSystem.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.ProjectTasks.Commands.UpdateProjectTaskStatus;

public class UpdateProjectTaskStatusCommandValidator : AbstractValidator<UpdateProjectTaskStatusCommand>
{
    public UpdateProjectTaskStatusCommandValidator()
    {
        RuleFor(x => x.Status)
            .Must(Status => Status == ProjectTaskStatus.Blocked
            || Status == ProjectTaskStatus.Completed
            || Status == ProjectTaskStatus.Cancelled
            || Status == ProjectTaskStatus.InProgress)
            .NotEmpty();
    }
}
