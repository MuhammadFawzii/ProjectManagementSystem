using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Projects.Commands.DeleteProject
{
    public class DeleteProjectCommand(Guid Id):IRequest
    {
        public Guid Id { get; set; }=Id;
    }
}
