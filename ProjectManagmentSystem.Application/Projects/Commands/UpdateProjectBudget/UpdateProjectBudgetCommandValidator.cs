using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Projects.Commands.UpdateProjectBudget
{
    public class UpdateProjectCommandValidator:AbstractValidator<UpdateProjectBudgetCommand>
    {
        public UpdateProjectCommandValidator() {

            RuleFor(x => x.Budget)
                .NotEmpty()
                .GreaterThan(0);
        }

    }
}
