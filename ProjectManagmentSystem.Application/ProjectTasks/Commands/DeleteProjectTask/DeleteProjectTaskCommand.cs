using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.ProjectTasks.Commands.DeleteProjectTask;

public class DeleteProjectTaskCommand(Guid ProjectId,Guid TaskId,Guid UserId):IRequest
{
    public Guid ProjectId { get; set; }= ProjectId;
    public Guid TaskId { get; set; } = TaskId;
    public Guid UserId { get;} = UserId;

}
