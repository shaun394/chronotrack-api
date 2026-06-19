using ChronoTrack.Api.Responses;
using ChronoTrack.Application.ReadModels.TimeEntryTags;
using ChronoTrack.Application.TimeEntryTags.Commands.Add;
using ChronoTrack.Application.TimeEntryTags.Commands.Remove;
using ChronoTrack.Application.TimeEntryTags.Queries.ListByTimeEntry;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChronoTrack.Api.Controllers
{
    [ApiController]
    [Route("api/time-entries/{timeEntryId:int}/tags")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public sealed class TimeEntryTagsController : ControllerBase
    {
        private const string Actor = "system";

        private readonly IMediator _mediator;

        public TimeEntryTagsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [EndpointSummary("List time entry tags")]
        [EndpointDescription("Returns all active tags assigned to a time entry.")]
        [ProducesResponseType(typeof(ApiResponse<IReadOnlyCollection<TimeEntryTagReadModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<IReadOnlyCollection<TimeEntryTagReadModel>>>> ListByTimeEntryAsync(
            int timeEntryId,
            CancellationToken ct)
        {
            var query = new ListTimeEntryTagsByTimeEntryQuery(timeEntryId);

            var result = await _mediator.Send(query, ct);

            return Ok(ApiResponse<IReadOnlyCollection<TimeEntryTagReadModel>>.Ok(result));
        }

        [HttpPost("{tagId:int}")]
        [EndpointSummary("Add tag to time entry")]
        [EndpointDescription("Assigns a tag to a time entry. If the tag was previously removed from the time entry, the existing link is restored.")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ApiResponse<int>>> AddAsync(
            int timeEntryId,
            int tagId,
            CancellationToken ct)
        {
            var command = new AddTimeEntryTagCommand(
                timeEntryId,
                tagId,
                Actor);

            var result = await _mediator.Send(command, ct);

            return Ok(ApiResponse<int>.Ok(result));
        }

        [HttpDelete("{tagId:int}")]
        [EndpointSummary("Remove tag from time entry")]
        [EndpointDescription("Soft-removes a tag assignment from a time entry.")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<int>>> RemoveAsync(
            int timeEntryId,
            int tagId,
            CancellationToken ct)
        {
            var command = new RemoveTimeEntryTagCommand(
                timeEntryId,
                tagId,
                Actor);

            var result = await _mediator.Send(command, ct);

            return Ok(ApiResponse<int>.Ok(result));
        }
    }
}