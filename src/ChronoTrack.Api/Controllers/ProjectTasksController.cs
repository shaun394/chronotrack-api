using ChronoTrack.Api.Requests.Tasks;
using ChronoTrack.Api.Responses;
using ChronoTrack.Application.ReadModels.Tasks;
using ChronoTrack.Application.Tasks.Commands.Create;
using ChronoTrack.Application.Tasks.Commands.Remove;
using ChronoTrack.Application.Tasks.Commands.Restore;
using ChronoTrack.Application.Tasks.Commands.Update;
using ChronoTrack.Application.Tasks.Queries.GetById;
using ChronoTrack.Application.Tasks.Queries.ListByProject;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChronoTrack.Api.Controllers
{
    [ApiController]
    [Route("api/project-tasks")]
    [Tags("Project Tasks")]
    public sealed class ProjectTasksController : ControllerBase
    {
        private const string Actor = "system";

        private readonly IMediator _mediator;

        public ProjectTasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [EndpointSummary("Create project task")]
        [EndpointDescription("Creates a new task inside a project.")]
        [ProducesResponseType(
            typeof(ApiResponse<int>),
            StatusCodes.Status201Created)]
        public async Task<ActionResult<ApiResponse<int>>> Create(
            CreateProjectTaskRequest request,
            CancellationToken ct)
        {
            int id = await _mediator.Send(
                new CreateProjectTaskCommand(
                    request.ProjectId,
                    request.Name,
                    request.Description,
                    request.IsBillable,
                    Actor),
                ct);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                ApiResponse<int>.Ok(id));
        }

        [HttpGet("{id:int}")]
        [EndpointSummary("Get project task by id")]
        [EndpointDescription("Returns a single active project task by id.")]
        [ProducesResponseType(
            typeof(ApiResponse<ProjectTaskReadModel>),
            StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<ProjectTaskReadModel>>> GetById(
            int id,
            CancellationToken ct)
        {
            var projectTask = await _mediator.Send(
                new GetProjectTaskByIdQuery(id),
                ct);

            return Ok(ApiResponse<ProjectTaskReadModel>.Ok(projectTask));
        }

        [HttpGet("project/{projectId:int}")]
        [EndpointSummary("List project tasks by project")]
        [EndpointDescription("Returns all active tasks for a project.")]
        [ProducesResponseType(
            typeof(ApiResponse<IReadOnlyCollection<ProjectTaskReadModel>>),
            StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<IReadOnlyCollection<ProjectTaskReadModel>>>> ListByProject(
            int projectId,
            CancellationToken ct)
        {
            var projectTasks = await _mediator.Send(
                new ListProjectTasksByProjectQuery(projectId),
                ct);

            return Ok(ApiResponse<IReadOnlyCollection<ProjectTaskReadModel>>.Ok(projectTasks));
        }

        [HttpPut("{id:int}")]
        [EndpointSummary("Update project task")]
        [EndpointDescription("Updates an existing project task.")]
        [ProducesResponseType(
            typeof(ApiResponse<int>),
            StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<int>>> Update(
            int id,
            UpdateProjectTaskRequest request,
            CancellationToken ct)
        {
            int projectTaskId = await _mediator.Send(
                new UpdateProjectTaskCommand(
                    id,
                    request.Name,
                    request.Description,
                    request.IsBillable,
                    Actor),
                ct);

            return Ok(ApiResponse<int>.Ok(projectTaskId));
        }

        [HttpPatch("{id:int}/remove")]
        [EndpointSummary("Remove project task")]
        [EndpointDescription("Soft deletes an existing project task.")]
        [ProducesResponseType(
            typeof(ApiResponse<int>),
            StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<int>>> Remove(
            int id,
            CancellationToken ct)
        {
            int projectTaskId = await _mediator.Send(
                new RemoveProjectTaskCommand(id, Actor),
                ct);

            return Ok(ApiResponse<int>.Ok(projectTaskId));
        }

        [HttpPatch("{id:int}/restore")]
        [EndpointSummary("Restore project task")]
        [EndpointDescription("Restores a previously removed project task.")]
        [ProducesResponseType(
            typeof(ApiResponse<int>),
            StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<int>>> Restore(
            int id,
            CancellationToken ct)
        {
            int projectTaskId = await _mediator.Send(
                new RestoreProjectTaskCommand(id, Actor),
                ct);

            return Ok(ApiResponse<int>.Ok(projectTaskId));
        }
    }
}