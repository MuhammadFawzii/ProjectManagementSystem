using FluentValidation;
namespace ProjectManagementSystem.Application.Projects.Commands.CreateProject;
public class CreateProjectCommandValidator:AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.Name)
                    .NotEmpty()
                    .MaximumLength(200);
        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
        RuleFor(x => x.ExpectedStartDate)
            .NotEmpty();
        RuleFor(x => x.Budget)
            .GreaterThan(0);

    }
}
