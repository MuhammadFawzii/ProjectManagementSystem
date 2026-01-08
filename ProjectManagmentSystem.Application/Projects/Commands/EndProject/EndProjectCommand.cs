using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Projects.Commands.EndProject
{
    public class EndProjectCommand(Guid projectId):IRequest
    {
        public Guid ProjectId { get; } = projectId;
    }
}
