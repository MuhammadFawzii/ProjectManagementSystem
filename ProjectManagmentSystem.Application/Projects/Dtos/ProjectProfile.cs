
using AutoMapper;
using ProjectManagementSystem.Application.Projects.Commands.CreateProject;
using ProjectManagementSystem.Application.Projects.Commands.UpdateProject;
using ProjectManagementSystem.Application.ProjectTasks.Dtos;
using ProjectManagementSystem.Domain.Entities;
namespace ProjectManagementSystem.Application.Projects.Dtos;

public class ProjectTaskProfile: Profile
{
    public ProjectTaskProfile()
    {
        CreateMap<Project, ProjectDto>().ReverseMap();
        CreateMap<Project, ProjectCurrencyDto>().ReverseMap();
        CreateMap<ProjectTask, ProjectTaskDto>().ReverseMap();
        CreateMap<CreateProjectCommand, Project>().ReverseMap();
        CreateMap<UpdateProjectCommand, Project>().ReverseMap();
    }
}
