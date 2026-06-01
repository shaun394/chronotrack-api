using ChronoTrack.Api.Requests.Workspaces;
using ChronoTrack.Api.Responses;
using ChronoTrack.Application.ReadModels.Workspaces;
using ChronoTrack.Application.Workspaces.Commands.Create;
using ChronoTrack.Application.Workspaces.Commands.Remove;
using ChronoTrack.Application.Workspaces.Commands.Restore;
using ChronoTrack.Application.Workspaces.Commands.Update;
using ChronoTrack.Application.Workspaces.Queries.GetById;
using ChronoTrack.Application.Workspaces.Queries.List;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChronoTrack.Api.Controllers
{
    [ApiController]
    [Route("api/workspaces")]
    [Tags("Workspaces")]
    public sealed class WorkspacesController : ControllerBase
    {
        private const string Actor = "system";

        private readonly IMediator _mediator;

        public WorkspacesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [EndpointSummary("Create workspace")]
        [EndpointDescription("Creates a new workspace.")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<int>>> Create(
            CreateWorkspaceRequest request,
            CancellationToken ct)
        {
            int id = await _mediator.Send(
                new CreateWorkspaceCommand(request.Name, Actor),
                ct);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                ApiResponse<int>.Ok(id));
        }

        [HttpGet]
        [EndpointSummary("List workspaces")]
        [EndpointDescription("Returns all active workspaces.")]
        [ProducesResponseType(typeof(ApiResponse<IReadOnlyCollection<WorkspaceReadModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<IReadOnlyCollection<WorkspaceReadModel>>>> List(
            CancellationToken ct)
        {
            var workspaces = await _mediator.Send(
                new ListWorkspacesQuery(),
                ct);

            return Ok(ApiResponse<IReadOnlyCollection<WorkspaceReadModel>>.Ok(workspaces));
        }

        [HttpPut("{id:int}")]
        [EndpointSummary("Update workspace")]
        [EndpointDescription("Updates the name of an existing workspace.")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<int>>> Update(
            int id,
            UpdateWorkspaceRequest request,
            CancellationToken ct)
        {
            int workspaceId = await _mediator.Send(
                new UpdateWorkspaceCommand(id, request.Name, Actor),
                ct);

            return Ok(ApiResponse<int>.Ok(workspaceId));
        }

        [HttpGet("{id:int}")]
        [EndpointSummary("Get workspace by id")]
        [EndpointDescription("Returns a single active workspace by id.")]
        [ProducesResponseType(typeof(ApiResponse<WorkspaceReadModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<WorkspaceReadModel>>> GetById(
            int id,
            CancellationToken ct)
        {
            var workspace = await _mediator.Send(
                new GetWorkspaceByIdQuery(id),
                ct);

            return Ok(ApiResponse<WorkspaceReadModel>.Ok(workspace));
        }

        [HttpPatch("{id:int}/remove")]
        [EndpointSummary("Remove workspace")]
        [EndpointDescription("Soft deletes an existing workspace.")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<int>>> Remove(
            int id,
            CancellationToken ct)
        {
            int workspaceId = await _mediator.Send(
                new RemoveWorkspaceCommand(id, Actor),
                ct);

            return Ok(ApiResponse<int>.Ok(workspaceId));
        }

        [HttpPatch("{id:int}/restore")]
        [EndpointSummary("Restore workspace")]
        [EndpointDescription("Restores a previously removed workspace.")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<int>>> Restore(
            int id,
            CancellationToken ct)
        {
            int workspaceId = await _mediator.Send(
                new RestoreWorkspaceCommand(id, Actor),
                ct);

            return Ok(ApiResponse<int>.Ok(workspaceId));
        }
    }
}