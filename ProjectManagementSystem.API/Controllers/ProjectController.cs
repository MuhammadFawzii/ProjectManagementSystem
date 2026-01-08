using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Application.Projects.Commands.CreateProject;
using ProjectManagementSystem.Application.Projects.Commands.DeleteProject;
using ProjectManagementSystem.Application.Projects.Commands.EndProject;
using ProjectManagementSystem.Application.Projects.Commands.UpdateProject;
using ProjectManagementSystem.Application.Projects.Commands.UpdateProjectBudget;
using ProjectManagementSystem.Application.Projects.Dtos;
using ProjectManagementSystem.Application.Projects.Queries.GetProjectById.GetProjectByIdV1;
using ProjectManagementSystem.Application.Projects.Queries.GetProjectById.GetProjectByIdV2;
using ProjectManagementSystem.Application.Projects.Queries.GetProjects.GetProjectsV1;
using ProjectManagementSystem.Application.Projects.Queries.GetProjects.GetProjectsV2;
using ProjectManagementSystem.Application.ProjectTasks.Commands.AssignUserToProjectTask;
using ProjectManagementSystem.Application.ProjectTasks.Commands.CreateProjectTask;
using ProjectManagementSystem.Application.ProjectTasks.Commands.DeleteProjectTask;
using ProjectManagementSystem.Application.ProjectTasks.Commands.UpdateProjectTask;
using ProjectManagementSystem.Application.ProjectTasks.Commands.UpdateProjectTaskStatus;
using ProjectManagementSystem.Application.ProjectTasks.Dtos;
using ProjectManagementSystem.Application.ProjectTasks.Queries.GetProjectTaskById;
using ProjectManagementSystem.Infrastructure.Permissions;
using System.Security.Claims;
namespace ProjectManagementSystem.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/projects")]
[Authorize]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Tags("Projects")]
public class ProjectController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [MapToApiVersion("1.0")]
    [Authorize(Permission.Project.Create)]
    [Consumes("application/json")]
    [ProducesResponseType<ProjectDto>(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    [EndpointName("CreateProjectV1")]
    [EndpointSummary("Creates a new project")]
    [EndpointDescription("Creates a new project for the current user and returns the created result.")]
    public async Task<ActionResult<ProjectDto>> CreateProject([FromBody] CreateProjectCommand command)
    {
        Guid id = await mediator.Send(command);
        var project = await mediator.Send(new GetProjectByIdV1Query(id));
        return CreatedAtAction(nameof(GetProject), new { projectId = id, version = "1.0" }, project);

    }
    

    [HttpGet]
    [MapToApiVersion("1.0")]
    [Authorize(Permission.Project.Read)]
    [ProducesResponseType<List<ProjectDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    [EndpointName("GetProjectsV1")]
    [EndpointSummary("Retrieves all projects")]
    [EndpointDescription("Retrieves all projects owned or accessible by the user.")]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects([FromQuery] GetProjectsV1Query query)
    {
        var projects = await mediator.Send(query);
        return Ok(projects);
    }
    [HttpGet]
    [MapToApiVersion("2.0")]
    [Authorize(Permission.Project.Read)]
    [ProducesResponseType<List<ProjectDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    [EndpointName("GetProjectsV2")]
    [EndpointSummary("Retrieves all projects with currency info")]
    [EndpointDescription("Retrieves all projects and includes a currency field for each.")]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjectsWithCurrency([FromQuery] GetProjectsV2Query query)
    {
        var projects = await mediator.Send(query);
        return Ok(projects);
    }

    [HttpPut("{projectId:guid}")]
    [MapToApiVersion("1.0")]
    [Authorize(Permission.Project.Update)]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status403Forbidden)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    [EndpointName("UpdateProjectV1")]
    [EndpointSummary("Updates a project")]
    [EndpointDescription("Updates the data of an existing project.")]
    public async Task<IActionResult> UpdateProject([FromRoute] Guid projectId, [FromBody] UpdateProjectCommand command)
    {
        command.Id = projectId;
        await mediator.Send(command);
        return NoContent();
    }

    [HttpGet("{projectId:guid}")]
    [MapToApiVersion("1.0")]
    [Authorize(Permission.Project.Read)]
    [ProducesResponseType<ProjectDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    [EndpointName("GetProjectV1")]
    [EndpointSummary("Retrieves a specific project")]
    [EndpointDescription("Retrieves a specific project by its ID.")]
    public async Task<ActionResult<ProjectDto>> GetProject([FromRoute] Guid projectId)
    {
        var project = await mediator.Send(new GetProjectByIdV1Query(projectId));
        return Ok(project);
    }

    [HttpGet("{projectId:guid}")]
    [MapToApiVersion("2.0")]
    [Authorize(Permission.Project.Read)]
    [ProducesResponseType<ProjectDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    [EndpointName("GetProjectV2")]
    [EndpointSummary("Retrieves a specific project with currency info")]
    [EndpointDescription("Retrieves a project by ID and includes currency information.")]
    public async Task<ActionResult<ProjectCurrencyDto>> GetProjectV2([FromRoute] Guid projectId)
    {
        var project = await mediator.Send(new GetProjectByIdV2Query(projectId));
        return Ok(project);
    }

    [HttpDelete("{projectId:guid}")]
    [MapToApiVersion("1.0")]
    [Authorize(Permission.Project.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status403Forbidden)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    [EndpointName("DeleteProjectV1")]
    [EndpointSummary("Deletes a project")]
    [EndpointDescription("Deletes a specific project by ID.")]
    public async Task<IActionResult> DeleteProject([FromRoute] Guid projectId)
    {
        await mediator.Send(new DeleteProjectCommand(projectId));
        return NoContent();
    }

    [HttpPut("{projectId:guid}/budget")]
    [MapToApiVersion("1.0")]
    [Authorize(Permission.Project.ManageBudget)]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status403Forbidden)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    [EndpointName("UpdateBudgetV1")]
    [EndpointSummary("Updates project budget")]
    [EndpointDescription("Updates the budget for a specific project.")]
    public async Task<IActionResult> UpdateProjectBudget([FromRoute] Guid projectId, [FromBody] UpdateProjectBudgetCommand command)
    {
        command.Id = projectId;
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{projectId:guid}/completion")]
    [MapToApiVersion("1.0")]
    [Authorize(Permission.Project.Update)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status403Forbidden)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    [EndpointName("EndProjectV1")]
    [EndpointSummary("Marks project as completed")]
    [EndpointDescription("Marks a project as completed.")]
    public async Task<IActionResult> EndProject([FromRoute] Guid projectId)
    {
        await mediator.Send(new EndProjectCommand(projectId));
        return NoContent();
    }

    // === TASK ENDPOINTS ===

    [HttpPost("{projectId:guid}/tasks")]
    [MapToApiVersion("1.0")]
    [Authorize(Permission.Task.Create)]
    [Consumes("application/json")]
    [ProducesResponseType<ProjectTaskDto>(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status403Forbidden)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    [Tags("Tasks")]
    [EndpointName("CreateTaskV1")]
    [EndpointSummary("Creates a task in a project")]
    [EndpointDescription("Creates a task under the given project.")]
    public async Task<ActionResult<ProjectTaskDto>> CreateTask([FromRoute] Guid projectId, [FromBody] CreateProjectTaskCommand command)
    {
        command.ProjectId = projectId;
        command.CurrentUserId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!

        );
        var task=await mediator.Send(command);
        
        return CreatedAtAction(nameof(GetTask), new { projectId, taskId=task.Id }, task);
    }

    [HttpGet("{projectId:guid}/tasks/{taskId:guid}")]
    [MapToApiVersion("1.0")]
    [Authorize(Permission.Task.Read)]
    [ProducesResponseType<ProjectTaskDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    [Tags("Tasks")]
    [EndpointName("GetTaskV1")]
    [EndpointSummary("Gets a task by ID")]
    [EndpointDescription("Retrieves a specific task from a project.")]
    public async Task<ActionResult<ProjectTaskDto>> GetTask([FromRoute] Guid projectId, [FromRoute] Guid taskId)
    {
        var task = await mediator.Send(new GetProjectTaskByIdQuery(taskId,projectId));
        return Ok(task);
    }

    [HttpPut("{projectId:guid}/tasks/{taskId:guid}/status")]
    [MapToApiVersion("1.0")]
    [Authorize(Permission.Task.UpdateStatus)]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status403Forbidden)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    [Tags("Tasks")]
    [EndpointName("UpdateTaskStatusV1")]
    [EndpointSummary("Updates task status")]
    [EndpointDescription("Updates the status of a task in a project.")]
    public async Task<IActionResult> UpdateTaskStatus([FromRoute] Guid projectId, [FromRoute] Guid taskId, [FromBody] UpdateProjectTaskStatusCommand request)
    {
        request.SetIds(projectId, taskId);
        await mediator.Send(request);
        return NoContent();
    }

    [HttpPut("{projectId:guid}/tasks/{taskId:guid}")]
    [MapToApiVersion("1.0")]
    [Authorize(Permission.Task.Update)]
    [Consumes("application/json")]
    [ProducesResponseType<ProjectTaskDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status403Forbidden)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    [Tags("Tasks")]
    [EndpointName("UpdateTaskV1")]
    [EndpointSummary("Updates task details")]
    [EndpointDescription("Updates the fields of an existing task.")]
    public async Task<IActionResult> UpdateTask([FromRoute] Guid projectId, [FromRoute] Guid taskId, [FromBody] UpdateProjectTaskCommand request)
    {
        var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        request.SetIds(projectId, taskId, currentUserId);
        ProjectTaskDto task = await mediator.Send(request);
        return Ok(task);
    }

    [HttpPut("{projectId:guid}/tasks/{taskId:guid}/assignment")]
    [MapToApiVersion("1.0")]
    [Authorize(Permission.Task.AssignUser)]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status403Forbidden)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    [Tags("Tasks")]
    [EndpointName("AssignUserV1")]
    [EndpointSummary("Assigns a user to a task")]
    [EndpointDescription("Assigns a user to a task within a project.")]
    public async Task<IActionResult> AssignUser([FromRoute] Guid projectId, [FromRoute] Guid taskId, [FromBody] AssignUserToProjectTaskCommand request)
    {
        request.SetIds(projectId, taskId);
        await mediator.Send(request);
        return NoContent();
    }

    [HttpDelete("{projectId:guid}/tasks/{taskId:guid}")]
    [MapToApiVersion("1.0")]
    [Authorize(Permission.Task.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status403Forbidden)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    [Tags("Tasks")]
    [EndpointName("DeleteTaskV1")]
    [EndpointSummary("Deletes a task")]
    [EndpointDescription("Deletes a task from a specific project.")]
    public async Task<IActionResult> DeleteTask([FromRoute] Guid projectId, [FromRoute] Guid taskId)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await mediator.Send(new DeleteProjectTaskCommand(projectId, taskId, userId));
        return NoContent();
    }
}
