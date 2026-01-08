using MediatR;
using ProjectManagementSystem.Application.Projects.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommand:IRequest
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public DateTime ExpectedStartDate { get; set; } = default!;
    }
}
