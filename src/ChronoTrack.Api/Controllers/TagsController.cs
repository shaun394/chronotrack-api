using ChronoTrack.Api.Common.Responses;
using ChronoTrack.Api.Requests.Tags;
using ChronoTrack.Application.ReadModels.Tags;
using ChronoTrack.Application.Tags.Commands.Create;
using ChronoTrack.Application.Tags.Commands.Remove;
using ChronoTrack.Application.Tags.Commands.Restore;
using ChronoTrack.Application.Tags.Commands.Update;
using ChronoTrack.Application.Tags.Queries.GetById;
using ChronoTrack.Application.Tags.Queries.ListAudit;
using ChronoTrack.Application.Tags.Queries.ListByWorkspace;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChronoTrack.Api.Controllers
{
    [ApiController]
    [Route("api/tags")]
    [Tags("Tags")]
    public sealed class TagsController : ControllerBase
    {
        private const string Actor = "system";

        private readonly IMediator _mediator;

        public TagsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [EndpointSummary("Create tag")]
        [EndpointDescription("Creates a new tag inside a workspace.")]
        [ProducesResponseType(
            typeof(ApiResponse<int>),
            StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAsync(
            CreateTagRequest request,
            CancellationToken ct)
        {
            int id = await _mediator.Send(
                new CreateTagCommand(
                    request.WorkspaceId,
                    request.Name,
                    Actor),
                ct);

            return ApiResponseFactory.Created(
                nameof(GetById),
                ControllerContext.ActionDescriptor.ControllerName,
                new { id },
                new { id });
        }

        [HttpGet("{id:int}")]
        [EndpointSummary("Get tag by id")]
        [EndpointDescription("Returns a single active tag by id.")]
        [ProducesResponseType(
            typeof(ApiResponse<TagReadModel>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(
            int id,
            CancellationToken ct)
        {
            var response = await _mediator.Send(
                new GetTagByIdQuery(id),
                ct);

            return ApiResponseFactory.Ok(response);
        }

        [HttpGet("workspace/{workspaceId:int}")]
        [EndpointSummary("List tags by workspace")]
        [EndpointDescription("Returns all active tags for a workspace.")]
        [ProducesResponseType(
            typeof(ApiResponse<IReadOnlyCollection<TagReadModel>>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> ListByWorkspaceAsync(
            int workspaceId,
            CancellationToken ct)
        {
            var response = await _mediator.Send(
                new ListTagsByWorkspaceQuery(workspaceId),
                ct);

            return ApiResponseFactory.Ok(response);
        }

        [HttpPut("{id:int}")]
        [EndpointSummary("Update tag")]
        [EndpointDescription("Updates an existing tag.")]
        [ProducesResponseType(
            typeof(ApiResponse<int>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync(
            int id,
            UpdateTagRequest request,
            CancellationToken ct)
        {
            await _mediator.Send(
                new UpdateTagCommand(
                    id,
                    request.Name,
                    Actor),
                ct);

            return ApiResponseFactory.NoContent();
        }

        [HttpPatch("{id:int}/remove")]
        [EndpointSummary("Remove tag")]
        [EndpointDescription("Soft deletes an existing tag.")]
        [ProducesResponseType(
            typeof(ApiResponse<int>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveAsync(
            int id,
            CancellationToken ct)
        {
            await _mediator.Send(
                new RemoveTagCommand(id, Actor),
                ct);

            return ApiResponseFactory.NoContent();
        }

        [HttpPatch("{id:int}/restore")]
        [EndpointSummary("Restore tag")]
        [EndpointDescription("Restores a previously removed tag.")]
        [ProducesResponseType(
            typeof(ApiResponse<int>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> RestoreAsync(
            int id,
            CancellationToken ct)
        {
            await _mediator.Send(
                new RestoreTagCommand(id, Actor),
                ct);

            return ApiResponseFactory.NoContent();
        }

        [HttpGet("{id:int}/audit")]
        [EndpointSummary("List tag audit events")]
        [EndpointDescription("Returns the audit event history for a tag.")]
        [ProducesResponseType(
            typeof(ApiResponse<TagAuditReadModel>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> ListAuditAsync(
            int id,
            CancellationToken ct)
        {
            var response = await _mediator.Send(
                new ListTagAuditEventsQuery(Id: id),
                ct);

            return ApiResponseFactory.Ok(response);
        }
    }
}