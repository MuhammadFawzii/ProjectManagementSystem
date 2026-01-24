using MediatR;
using ProjectManagementSystem.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Projects.Commands.EndProject
{
    public class EndProjectCommand(Guid projectId):IRequest, ICacheInvalidatorRequest
    {
        public Guid ProjectId { get; } = projectId;
        [JsonIgnore]
        public string[] CacheKeys => [$"project:{ProjectId}", $"project:v2:{ProjectId}"];
        [JsonIgnore]
        public string[] CacheTags => ["projects:v1", "projects:v2"];
    }
}

