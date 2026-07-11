using ChronoTrack.Api.Common.Responses;
using ChronoTrack.Api.Requests.Projects;
using ChronoTrack.Application.Projects.Commands.Create;
using ChronoTrack.Application.Projects.Commands.Remove;
using ChronoTrack.Application.Projects.Commands.Restore;
using ChronoTrack.Application.Projects.Commands.Update;
using ChronoTrack.Application.Projects.Queries.GetById;
using ChronoTrack.Application.Projects.Queries.ListAudit;
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
        [ProducesResponseType(
            typeof(ApiResponse<int>),
            StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAsync(
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

            return ApiResponseFactory.Created(
                nameof(GetById),
                ControllerContext.ActionDescriptor.ControllerName,
                new { id },
                new { id });
        }

        [HttpGet("{id:int}")]
        [EndpointSummary("Get project by id")]
        [EndpointDescription("Returns a single active project by id.")]
        [ProducesResponseType(
            typeof(ApiResponse<ProjectReadModel>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(
            int id,
            CancellationToken ct)
        {
            var response = await _mediator.Send(
                new GetProjectByIdQuery(id),
                ct);

            return ApiResponseFactory.Ok(response);
        }

        [HttpGet("workspace/{workspaceId:int}")]
        [EndpointSummary("List projects by workspace")]
        [EndpointDescription("Returns all active projects for a workspace.")]
        [ProducesResponseType(
            typeof(ApiResponse<IReadOnlyCollection<ProjectReadModel>>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> ListByWorkspaceAsync(
            int workspaceId,
            CancellationToken ct)
        {
            var response = await _mediator.Send(
                new ListProjectsByWorkspaceQuery(workspaceId),
                ct);

            return ApiResponseFactory.Ok(response);
        }

        [HttpPut("{id:int}")]
        [EndpointSummary("Update project")]
        [EndpointDescription("Updates an existing project.")]
        [ProducesResponseType(
            typeof(ApiResponse<int>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync(
            int id,
            UpdateProjectRequest request,
            CancellationToken ct)
        {
            await _mediator.Send(
                new UpdateProjectCommand(
                    id,
                    request.ClientId,
                    request.Name,
                    request.Description,
                    request.IsBillable,
                    Actor),
                ct);

            return ApiResponseFactory.NoContent();
        }

        [HttpPatch("{id:int}/remove")]
        [EndpointSummary("Remove project")]
        [EndpointDescription("Soft deletes an existing project.")]
        [ProducesResponseType(
            typeof(ApiResponse<int>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveAsync(
            int id,
            CancellationToken ct)
        {
            await _mediator.Send(
                new RemoveProjectCommand(id, Actor),
                ct);

            return ApiResponseFactory.NoContent();
        }

        [HttpPatch("{id:int}/restore")]
        [EndpointSummary("Restore project")]
        [EndpointDescription("Restores a previously removed project.")]
        [ProducesResponseType(
            typeof(ApiResponse<int>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> RestoreAsync(
            int id,
            CancellationToken ct)
        {
            await _mediator.Send(
                new RestoreProjectCommand(id, Actor),
                ct);

            return ApiResponseFactory.NoContent();
        }

        [HttpGet("{id:int}/audit")]
        [EndpointSummary("List project audit events")]
        [EndpointDescription("Returns the audit event history for a project.")]
        [ProducesResponseType(
            typeof(ApiResponse<ProjectAuditReadModel>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> ListAuditAsync(
            int id,
            CancellationToken ct)
        {
            var response = await _mediator.Send(
                new ListProjectAuditEventsQuery(Id: id),
                ct);

            return ApiResponseFactory.Ok(response);
        }
    }
}