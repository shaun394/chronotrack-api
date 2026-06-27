using ChronoTrack.Api.Requests.TimeEntries;
using ChronoTrack.Api.Responses;
using ChronoTrack.Application.ReadModels.TimeEntries;
using ChronoTrack.Application.TimeEntries.Commands.Create;
using ChronoTrack.Application.TimeEntries.Commands.Remove;
using ChronoTrack.Application.TimeEntries.Commands.Restore;
using ChronoTrack.Application.TimeEntries.Commands.Update;
using ChronoTrack.Application.TimeEntries.Queries.GetById;
using ChronoTrack.Application.TimeEntries.Queries.ListByProject;
using ChronoTrack.Application.TimeEntries.Queries.ListByWorkspace;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChronoTrack.Api.Controllers
{
    [ApiController]
    [Route("api/time-entries")]
    [Tags("Time Entries")]
    public sealed class TimeEntriesController : ControllerBase
    {
        private const string Actor = "system";

        private readonly IMediator _mediator;

        public TimeEntriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [EndpointSummary("Create time entry")]
        [EndpointDescription("Creates a new time entry for a workspace and project.")]
        [ProducesResponseType(
            typeof(ApiResponse<int>),
            StatusCodes.Status201Created)]
        public async Task<ActionResult<ApiResponse<int>>> Create(
            CreateTimeEntryRequest request,
            CancellationToken ct)
        {
            int id = await _mediator.Send(
                new CreateTimeEntryCommand(
                    request.WorkspaceId,
                    request.ProjectId,
                    request.ProjectTaskId,
                    request.ClientId,
                    request.Description,
                    request.WorkDate,
                    request.StartTime,
                    request.EndTime,
                    request.IsBillable,
                    Actor),
                ct);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                ApiResponse<int>.Ok(id));
        }

        [HttpGet("{id:int}")]
        [EndpointSummary("Get time entry by id")]
        [EndpointDescription("Returns a single active time entry by id.")]
        [ProducesResponseType(
            typeof(ApiResponse<TimeEntryReadModel>),
            StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<TimeEntryReadModel>>> GetById(
            int id,
            CancellationToken ct)
        {
            var timeEntry = await _mediator.Send(
                new GetTimeEntryByIdQuery(id),
                ct);

            return Ok(ApiResponse<TimeEntryReadModel>.Ok(timeEntry));
        }

        [HttpGet("workspace/{workspaceId:int}")]
        [EndpointSummary("List time entries by workspace")]
        [EndpointDescription("Returns all active time entries for a workspace.")]
        [ProducesResponseType(
            typeof(ApiResponse<IReadOnlyCollection<TimeEntryReadModel>>),
            StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<IReadOnlyCollection<TimeEntryReadModel>>>> ListByWorkspace(
            int workspaceId,
            CancellationToken ct)
        {
            var timeEntries = await _mediator.Send(
                new ListTimeEntriesByWorkspaceQuery(workspaceId),
                ct);

            return Ok(ApiResponse<IReadOnlyCollection<TimeEntryReadModel>>.Ok(timeEntries));
        }

        [HttpGet("project/{projectId:int}")]
        [EndpointSummary("List time entries by project")]
        [EndpointDescription("Returns all active time entries for a project.")]
        [ProducesResponseType(
            typeof(ApiResponse<IReadOnlyCollection<TimeEntryReadModel>>),
            StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<IReadOnlyCollection<TimeEntryReadModel>>>> ListByProject(
            int projectId,
            CancellationToken ct)
        {
            var timeEntries = await _mediator.Send(
                new ListTimeEntriesByProjectQuery(projectId),
                ct);

            return Ok(ApiResponse<IReadOnlyCollection<TimeEntryReadModel>>.Ok(timeEntries));
        }

        [HttpPut("{id:int}")]
        [EndpointSummary("Update time entry")]
        [EndpointDescription("Updates an existing time entry.")]
        [ProducesResponseType(
            typeof(ApiResponse<int>),
            StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<int>>> Update(
            int id,
            UpdateTimeEntryRequest request,
            CancellationToken ct)
        {
            int timeEntryId = await _mediator.Send(
                new UpdateTimeEntryCommand(
                    id,
                    request.ProjectId,
                    request.ProjectTaskId,
                    request.ClientId,
                    request.Description,
                    request.WorkDate,
                    request.StartTime,
                    request.EndTime,
                    request.IsBillable,
                    Actor),
                ct);

            return Ok(ApiResponse<int>.Ok(timeEntryId));
        }

        [HttpPatch("{id:int}/remove")]
        [EndpointSummary("Remove time entry")]
        [EndpointDescription("Soft deletes an existing time entry.")]
        [ProducesResponseType(
            typeof(ApiResponse<int>),
            StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<int>>> Remove(
            int id,
            CancellationToken ct)
        {
            int timeEntryId = await _mediator.Send(
                new RemoveTimeEntryCommand(id, Actor),
                ct);

            return Ok(ApiResponse<int>.Ok(timeEntryId));
        }

        [HttpPatch("{id:int}/restore")]
        [EndpointSummary("Restore time entry")]
        [EndpointDescription("Restores a previously removed time entry.")]
        [ProducesResponseType(
            typeof(ApiResponse<int>),
            StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<int>>> Restore(
            int id,
            CancellationToken ct)
        {
            int timeEntryId = await _mediator.Send(
                new RestoreTimeEntryCommand(id, Actor),
                ct);

            return Ok(ApiResponse<int>.Ok(timeEntryId));
        }
    }
}