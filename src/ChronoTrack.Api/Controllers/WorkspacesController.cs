using ChronoTrack.Api.Common.Responses;
using ChronoTrack.Api.Requests.Workspaces;
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
    [Tags("Workspaces")]
    [Route("api/workspaces")]
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
        [ProducesResponseType(
            typeof(ApiResponse<int>),
            StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAsync(
            [FromBody] CreateWorkspaceRequest request,
            CancellationToken ct)
        {
            int id = await _mediator.Send(
                new CreateWorkspaceCommand(
                    request.Name,
                    request.Description,
                    Actor),
                ct);

            return ApiResponseFactory.Created(
                nameof(GetByIdAsync),
                ControllerContext.ActionDescriptor.ControllerName,
                new { id },
                new { id });
        }

        [HttpGet]
        [EndpointSummary("List workspaces")]
        [EndpointDescription("Returns all active workspaces.")]
        [ProducesResponseType(
            typeof(ApiResponse<IReadOnlyCollection<WorkspaceReadModel>>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> List(
            CancellationToken ct)
        {
            var response = await _mediator.Send(
                new ListWorkspacesQuery(),
                ct);

            return ApiResponseFactory.Ok(response);
        }

        [HttpGet("{id:int}")]
        [EndpointSummary("Get workspace by id")]
        [EndpointDescription("Returns a single active workspace by id.")]
        [ProducesResponseType(
            typeof(ApiResponse<WorkspaceReadModel>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync(
            int id,
            CancellationToken ct)
        {
            var response = await _mediator.Send(
                new GetWorkspaceByIdQuery(id),
                ct);

            return ApiResponseFactory.Ok(response);
        }

        [HttpPut("{id:int}")]
        [EndpointSummary("Update workspace")]
        [EndpointDescription("Updates an existing workspace.")]
        [ProducesResponseType(
            typeof(ApiResponse<int>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync(
            int id,
            UpdateWorkspaceRequest request,
            CancellationToken ct)
        {
            await _mediator.Send(
                new UpdateWorkspaceCommand(
                    id,
                    request.Name,
                    request.Description,
                    Actor),
                ct);

            return ApiResponseFactory.NoContent();
        }

        [HttpPatch("{id:int}/remove")]
        [EndpointSummary("Remove workspace")]
        [EndpointDescription("Soft deletes an existing workspace.")]
        [ProducesResponseType(
            typeof(ApiResponse<int>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveAsync(
            int id,
            CancellationToken ct)
        {
            await _mediator.Send(
                new RemoveWorkspaceCommand(id, Actor),
                ct);

            return ApiResponseFactory.NoContent();
        }

        [HttpPatch("{id:int}/restore")]
        [EndpointSummary("Restore workspace")]
        [EndpointDescription("Restores a previously removed workspace.")]
        [ProducesResponseType(
            typeof(ApiResponse<int>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> RestoreAsync(
            int id,
            CancellationToken ct)
        {
            await _mediator.Send(
                new RestoreWorkspaceCommand(id, Actor),
                ct);

            return ApiResponseFactory.NoContent();
        }
    }
}