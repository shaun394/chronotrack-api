using ChronoTrack.Api.Common.Responses;
using ChronoTrack.Application.ReadModels.Reports;
using ChronoTrack.Application.Reports.Queries.GetWorkspaceSummary;
using ChronoTrack.Application.Reports.Queries.ListDailySummary;
using ChronoTrack.Application.Reports.Queries.ListProjectSummary;
using ChronoTrack.Application.Reports.Queries.ListTagSummary;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChronoTrack.Api.Controllers
{
    [ApiController]
    [Route("api/reports/workspace/{workspaceId:int}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public sealed class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("summary")]
        [EndpointSummary("Get workspace time summary")]
        [EndpointDescription("Returns total, billable, and non-billable time for a workspace within a date range.")]
        [ProducesResponseType(
            typeof(ApiResponse<WorkspaceTimeSummaryReadModel>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWorkspaceSummaryAsync(
            int workspaceId,
            [FromQuery] DateOnly from,
            [FromQuery] DateOnly to,
            CancellationToken ct)
        {
            var query = new GetWorkspaceSummaryQuery(
                workspaceId,
                from,
                to);

            var response = await _mediator.Send(query, ct);

            return ApiResponseFactory.Ok(response);
        }

        [HttpGet("daily")]
        [EndpointSummary("List daily time summary")]
        [EndpointDescription("Returns daily time totals for a workspace within a date range.")]
        [ProducesResponseType(
            typeof(ApiResponse<IReadOnlyCollection<DailyTimeSummaryReadModel>>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> ListDailySummaryAsync(
            int workspaceId,
            [FromQuery] DateOnly from,
            [FromQuery] DateOnly to,
            CancellationToken ct)
        {
            var query = new ListDailySummaryQuery(
                workspaceId,
                from,
                to);

            var response = await _mediator.Send(query, ct);

            return ApiResponseFactory.Ok(response);
        }

        [HttpGet("project-summary")]
        [EndpointSummary("List project time summary")]
        [EndpointDescription("Returns grouped time totals by project for a workspace within a date range.")]
        [ProducesResponseType(
            typeof(ApiResponse<IReadOnlyCollection<ProjectTimeSummaryReadModel>>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> ListProjectSummaryAsync(
            int workspaceId,
            [FromQuery] DateOnly from,
            [FromQuery] DateOnly to,
            CancellationToken ct)
        {
            var query = new ListProjectSummaryQuery(
                workspaceId,
                from,
                to);

            var response = await _mediator.Send(query, ct);

            return ApiResponseFactory.Ok(response);
        }

        [HttpGet("tag-summary")]
        [EndpointSummary("List tag time summary")]
        [EndpointDescription("Returns grouped time totals by tag for a workspace within a date range.")]
        [ProducesResponseType(
            typeof(ApiResponse<IReadOnlyCollection<TagTimeSummaryReadModel>>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> ListTagSummaryAsync(
            int workspaceId,
            [FromQuery] DateOnly from,
            [FromQuery] DateOnly to,
            CancellationToken ct)
        {
            var query = new ListTagSummaryQuery(
                workspaceId,
                from,
                to);

            var response = await _mediator.Send(query, ct);

            return ApiResponseFactory.Ok(response);
        }
    }
}