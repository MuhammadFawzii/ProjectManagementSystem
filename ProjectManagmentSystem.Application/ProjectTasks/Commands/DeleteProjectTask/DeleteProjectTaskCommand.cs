using MediatR;
using ProjectManagementSystem.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.ProjectTasks.Commands.DeleteProjectTask;

public class DeleteProjectTaskCommand(Guid ProjectId, Guid TaskId, Guid UserId) : IRequest, ICacheInvalidatorRequest
{
    public Guid ProjectId { get; set; } = ProjectId;
    public Guid TaskId { get; set; } = TaskId;
    public Guid UserId { get; } = UserId;

    public string[]? CacheTags => new[]
        {
        $"projects:v1",
        $"projects:v2",
    };
    public string[]? CacheKeys => new[]
    {
        $"projecttask:{ProjectId}:{TaskId}",
        $"project:{ProjectId}",
        $"project:v2:{ProjectId}",
    };

}