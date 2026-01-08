using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommandValidator:AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidator() {

            RuleFor(x => x.Name)
                    .NotEmpty()
                    .MaximumLength(200);
            RuleFor(x => x.Description)
                .MaximumLength(1000)
                .When(x => !string.IsNullOrWhiteSpace(x.Description));
            RuleFor(x => x.ExpectedStartDate)
                .NotEmpty();
            


        }

    }
}
