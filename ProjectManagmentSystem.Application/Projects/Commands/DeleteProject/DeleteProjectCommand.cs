using MediatR;
using ProjectManagementSystem.Application.Common.Interfaces;
using ProjectManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Projects.Commands.DeleteProject
{
    public class DeleteProjectCommand(Guid Id):IRequest, ICacheInvalidatorRequest
    {
        public Guid Id { get; set; }=Id;

        [JsonIgnore]
        public string[] CacheKeys => [$"project:{Id}", $"project:v2:{Id}"];
        [JsonIgnore]
        public string[] CacheTags => ["projects:v1", "projects:v2"];
    }
}
