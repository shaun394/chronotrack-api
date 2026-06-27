using ChronoTrack.Api.Common.Responses;
using ChronoTrack.Api.Requests.TimeEntries;
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
        public async Task<IActionResult> CreateAsync(
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

            return ApiResponseFactory.Created(
                nameof(GetByIdAsync),
                ControllerContext.ActionDescriptor.ControllerName,
                new { id },
                new { id });
        }

        [HttpGet("{id:int}")]
        [EndpointSummary("Get time entry by id")]
        [EndpointDescription("Returns a single active time entry by id.")]
        [ProducesResponseType(
            typeof(ApiResponse<TimeEntryReadModel>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync(
            int id,
            CancellationToken ct)
        {
            var response = await _mediator.Send(
                new GetTimeEntryByIdQuery(id),
                ct);

            return ApiResponseFactory.Ok(response);
        }

        [HttpGet("workspace/{workspaceId:int}")]
        [EndpointSummary("List time entries by workspace")]
        [EndpointDescription("Returns all active time entries for a workspace.")]
        [ProducesResponseType(
            typeof(ApiResponse<IReadOnlyCollection<TimeEntryReadModel>>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> ListByWorkspaceAsync(
            int workspaceId,
            CancellationToken ct)
        {
            var response = await _mediator.Send(
                new ListTimeEntriesByWorkspaceQuery(workspaceId),
                ct);

            return ApiResponseFactory.Ok(response);
        }

        [HttpGet("project/{projectId:int}")]
        [EndpointSummary("List time entries by project")]
        [EndpointDescription("Returns all active time entries for a project.")]
        [ProducesResponseType(
            typeof(ApiResponse<IReadOnlyCollection<TimeEntryReadModel>>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> ListByProjectAsync(
            int projectId,
            CancellationToken ct)
        {
            var response = await _mediator.Send(
                new ListTimeEntriesByProjectQuery(projectId),
                ct);

            return ApiResponseFactory.Ok(response);
        }

        [HttpPut("{id:int}")]
        [EndpointSummary("Update time entry")]
        [EndpointDescription("Updates an existing time entry.")]
        [ProducesResponseType(
            typeof(ApiResponse<int>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync(
            int id,
            UpdateTimeEntryRequest request,
            CancellationToken ct)
        {
            await _mediator.Send(
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

            return ApiResponseFactory.NoContent();
        }

        [HttpPatch("{id:int}/remove")]
        [EndpointSummary("Remove time entry")]
        [EndpointDescription("Soft deletes an existing time entry.")]
        [ProducesResponseType(
            typeof(ApiResponse<int>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveAsync(
            int id,
            CancellationToken ct)
        {
            await _mediator.Send(
                new RemoveTimeEntryCommand(id, Actor),
                ct);

            return ApiResponseFactory.NoContent();
        }

        [HttpPatch("{id:int}/restore")]
        [EndpointSummary("Restore time entry")]
        [EndpointDescription("Restores a previously removed time entry.")]
        [ProducesResponseType(
            typeof(ApiResponse<int>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> RestoreAsync(
            int id,
            CancellationToken ct)
        {
            int timeEntryId = await _mediator.Send(
                new RestoreTimeEntryCommand(id, Actor),
                ct);

            return ApiResponseFactory.NoContent();
        }
    }
}