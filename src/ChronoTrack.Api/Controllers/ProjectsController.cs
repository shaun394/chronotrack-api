using ChronoTrack.Api.Requests.Projects;
using ChronoTrack.Api.Responses;
using ChronoTrack.Application.Projects.Commands.Create;
using ChronoTrack.Application.Projects.Commands.Remove;
using ChronoTrack.Application.Projects.Commands.Restore;
using ChronoTrack.Application.Projects.Commands.Update;
using ChronoTrack.Application.Projects.Queries.GetById;
using ChronoTrack.Application.Projects.Queries.ListByWorkspace;
using ChronoTrack.Application.ReadModels.Projects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChronoTrack.Api.Controllers
{
    [ApiController]
    [Route("api/projects")]
    [Tags("Projects")]
    public sealed class ProjectsController : ControllerBase
    {
        private const string Actor = "system";

        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [EndpointSummary("Create project")]
        [EndpointDescription("Creates a new project inside a workspace. A project can optionally be linked to a client.")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status201Created)]
        public async Task<ActionResult<ApiResponse<int>>> Create(
            CreateProjectRequest request,
            CancellationToken ct)
        {
            int id = await _mediator.Send(
                new CreateProjectCommand(
                    request.WorkspaceId,
                    request.ClientId,
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
        [EndpointSummary("Get project by id")]
        [EndpointDescription("Returns a single active project by id.")]
        [ProducesResponseType(typeof(ApiResponse<ProjectReadModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<ProjectReadModel>>> GetById(
            int id,
            CancellationToken ct)
        {
            var project = await _mediator.Send(
                new GetProjectByIdQuery(id),
                ct);

            return Ok(ApiResponse<ProjectReadModel>.Ok(project));
        }

        [HttpGet("workspace/{workspaceId:int}")]
        [EndpointSummary("List projects by workspace")]
        [EndpointDescription("Returns all active projects for a workspace.")]
        [ProducesResponseType(typeof(ApiResponse<IReadOnlyCollection<ProjectReadModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<IReadOnlyCollection<ProjectReadModel>>>> ListByWorkspace(
            int workspaceId,
            CancellationToken ct)
        {
            var projects = await _mediator.Send(
                new ListProjectsByWorkspaceQuery(workspaceId),
                ct);

            return Ok(ApiResponse<IReadOnlyCollection<ProjectReadModel>>.Ok(projects));
        }

        [HttpPut("{id:int}")]
        [EndpointSummary("Update project")]
        [EndpointDescription("Updates an existing project.")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<int>>> Update(
            int id,
            UpdateProjectRequest request,
            CancellationToken ct)
        {
            int projectId = await _mediator.Send(
                new UpdateProjectCommand(
                    id,
                    request.ClientId,
                    request.Name,
                    request.Description,
                    request.IsBillable,
                    Actor),
                ct);

            return Ok(ApiResponse<int>.Ok(projectId));
        }

        [HttpPatch("{id:int}/remove")]
        [EndpointSummary("Remove project")]
        [EndpointDescription("Soft deletes an existing project.")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<int>>> Remove(
            int id,
            CancellationToken ct)
        {
            int projectId = await _mediator.Send(
                new RemoveProjectCommand(id, Actor),
                ct);

            return Ok(ApiResponse<int>.Ok(projectId));
        }

        [HttpPatch("{id:int}/restore")]
        [EndpointSummary("Restore project")]
        [EndpointDescription("Restores a previously removed project.")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<int>>> Restore(
            int id,
            CancellationToken ct)
        {
            int projectId = await _mediator.Send(
                new RestoreProjectCommand(id, Actor),
                ct);

            return Ok(ApiResponse<int>.Ok(projectId));
        }
    }
}