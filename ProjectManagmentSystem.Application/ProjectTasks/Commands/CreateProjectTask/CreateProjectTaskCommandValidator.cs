using FluentValidation;
namespace ProjectManagementSystem.Application.ProjectTasks.Commands.CreateProjectTask;
public class CreateProjectTaskCommandValidator:AbstractValidator<CreateProjectTaskCommand>
{
    public CreateProjectTaskCommandValidator()
    {
        RuleFor(x => x.Title)
                    .NotEmpty()
                    .MaximumLength(100);
        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
        RuleFor(x => x.AssignedUserId)
            .NotEmpty();
      

    }
}
