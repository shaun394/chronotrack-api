using ChronoTrack.Api.Common.Responses;
using ChronoTrack.Api.Requests.Clients;
using ChronoTrack.Application.Clients.Commands.Create;
using ChronoTrack.Application.Clients.Commands.Remove;
using ChronoTrack.Application.Clients.Commands.Restore;
using ChronoTrack.Application.Clients.Commands.Update;
using ChronoTrack.Application.Clients.Queries.GetById;
using ChronoTrack.Application.Clients.Queries.ListAudit;
using ChronoTrack.Application.Clients.Queries.ListByWorkspace;
using ChronoTrack.Application.ReadModels.Clients;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChronoTrack.Api.Controllers
{
    [ApiController]
    [Route("api/clients")]
    [Tags("Clients")]
    public sealed class ClientsController : ControllerBase
    {
        private const string Actor = "system";

        private readonly IMediator _mediator;

        public ClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [EndpointSummary("Create client")]
        [EndpointDescription("Creates a new client inside a workspace.")]
        [ProducesResponseType(
            typeof(ApiResponse<int>),
            StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAsync(
            CreateClientRequest request,
            CancellationToken ct)
        {
            int id = await _mediator.Send(
                new CreateClientCommand(
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
        [EndpointSummary("Get client by id")]
        [EndpointDescription("Returns a single active client by id.")]
        [ProducesResponseType(
            typeof(ApiResponse<ClientReadModel>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(
            int id,
            CancellationToken ct)
        {
            var response = await _mediator.Send(
                new GetClientByIdQuery(id),
                ct);

            return ApiResponseFactory.Ok(response);
        }

        [HttpGet("workspace/{workspaceId:int}")]
        [EndpointSummary("List clients by workspace")]
        [EndpointDescription("Returns all active clients for a workspace.")]
        [ProducesResponseType(
            typeof(ApiResponse<IReadOnlyCollection<ClientReadModel>>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> ListByWorkspaceAsync(
            int workspaceId,
            CancellationToken ct)
        {
            var response = await _mediator.Send(
                new ListClientsByWorkspaceQuery(workspaceId),
                ct);

            return ApiResponseFactory.Ok(response);
        }

        [HttpPut("{id:int}")]
        [EndpointSummary("Update client")]
        [EndpointDescription("Updates the name of an existing client.")]
        [ProducesResponseType(
            typeof(ApiResponse<int>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync(
            int id,
            UpdateClientRequest request,
            CancellationToken ct)
        {
            await _mediator.Send(
                new UpdateClientCommand(
                    id,
                    request.Name,
                    Actor),
                ct);

            return ApiResponseFactory.NoContent();
        }

        [HttpPatch("{id:int}/remove")]
        [EndpointSummary("Remove client")]
        [EndpointDescription("Soft deletes an existing client.")]
        [ProducesResponseType(
            typeof(ApiResponse<int>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveAsync(
            int id,
            CancellationToken ct)
        {
            await _mediator.Send(
                new RemoveClientCommand(id, Actor),
                ct);

            return ApiResponseFactory.NoContent();
        }

        [HttpPatch("{id:int}/restore")]
        [EndpointSummary("Restore client")]
        [EndpointDescription("Restores a previously removed client.")]
        [ProducesResponseType(
            typeof(ApiResponse<int>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> RestoreAsync(
            int id,
            CancellationToken ct)
        {
            await _mediator.Send(
                new RestoreClientCommand(id, Actor),
                ct);

            return ApiResponseFactory.NoContent();
        }

        [HttpGet("{id:int}/audit")]
        [EndpointSummary("List client audit events")]
        [EndpointDescription("Returns the audit event history for a client.")]
        [ProducesResponseType(
            typeof(ApiResponse<ClientAuditReadModel>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> ListAuditAsync(
            int id,
            CancellationToken ct)
        {
            var response = await _mediator.Send(
                new ListClientAuditEventsQuery(Id: id),
                ct);

            return ApiResponseFactory.Ok(response);
        }
    }
}