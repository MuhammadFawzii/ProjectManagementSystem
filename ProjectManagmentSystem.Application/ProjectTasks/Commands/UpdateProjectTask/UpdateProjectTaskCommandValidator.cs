using FluentValidation;
using ProjectManagementSystem.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.ProjectTasks.Commands.UpdateProjectTask;

public class UpdateProjectTaskCommandValidator:AbstractValidator<UpdateProjectTaskCommand>
{
    public UpdateProjectTaskCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
        RuleFor(x => x.Status)
            .Must(Status => Status == ProjectTaskStatus.Blocked
            || Status == ProjectTaskStatus.Completed
            || Status == ProjectTaskStatus.Cancelled
            || Status == ProjectTaskStatus.InProgress)
            .NotEmpty();


    }
}
