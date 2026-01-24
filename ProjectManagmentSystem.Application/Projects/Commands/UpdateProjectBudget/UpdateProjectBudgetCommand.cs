using MediatR;
using ProjectManagementSystem.Application.Common.Interfaces;
using ProjectManagementSystem.Application.Projects.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Projects.Commands.UpdateProjectBudget
{
    public class UpdateProjectBudgetCommand: IRequest, ICacheInvalidatorRequest
    {
        public Guid Id { get; set; }
        public decimal Budget { get; set; }
        [JsonIgnore]
        public string[] CacheKeys => [$"project:{Id}", $"project:v2:{Id}"];
        [JsonIgnore]
        public string[] CacheTags => ["projects:v1", "projects:v2"];
    }
}

