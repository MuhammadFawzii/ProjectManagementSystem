using MediatR;
using ProjectManagementSystem.Application.Projects.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Projects.Commands.UpdateProjectBudget
{
    public class UpdateProjectBudgetCommand: IRequest
    {
        public Guid Id { get; set; }
        public decimal Budget { get; set; }
    }
}
