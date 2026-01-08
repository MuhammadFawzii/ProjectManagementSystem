using AutoMapper;
namespace ProjectManagementSystem.Application.ProjectTasks.Dtos;
using ProjectManagementSystem.Application.ProjectTasks.Commands.CreateProjectTask;
using ProjectManagementSystem.Application.ProjectTasks.Commands.UpdateProjectTask;
using ProjectManagementSystem.Domain.Entities;
public class ProjectTaskProfile: Profile
{
    public ProjectTaskProfile()
    {
        CreateMap<ProjectTask, ProjectTaskDto>().ReverseMap();
        CreateMap<CreateProjectTaskCommand, ProjectTask>().ReverseMap();
        CreateMap<UpdateProjectTaskCommand, ProjectTask>().ReverseMap();
    }
}
