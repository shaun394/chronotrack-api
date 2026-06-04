using ChronoTrack.Api.Requests.Clients;
using ChronoTrack.Api.Responses;
using ChronoTrack.Application.Clients.Commands.Create;
using ChronoTrack.Application.Clients.Commands.Remove;
using ChronoTrack.Application.Clients.Commands.Restore;
using ChronoTrack.Application.Clients.Commands.Update;
using ChronoTrack.Application.Clients.Queries.GetById;
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
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status201Created)]
        public async Task<ActionResult<ApiResponse<int>>> Create(
            CreateClientRequest request,
            CancellationToken ct)
        {
            int id = await _mediator.Send(
                new CreateClientCommand(
                    request.WorkspaceId,
                    request.Name,
                    Actor),
                ct);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                ApiResponse<int>.Ok(id));
        }

        [HttpGet("{id:int}")]
        [EndpointSummary("Get client by id")]
        [EndpointDescription("Returns a single active client by id.")]
        [ProducesResponseType(typeof(ApiResponse<ClientReadModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<ClientReadModel>>> GetById(
            int id,
            CancellationToken ct)
        {
            var client = await _mediator.Send(
                new GetClientByIdQuery(id),
                ct);

            return Ok(ApiResponse<ClientReadModel>.Ok(client));
        }

        [HttpGet("workspace/{workspaceId:int}")]
        [EndpointSummary("List clients by workspace")]
        [EndpointDescription("Returns all active clients for a workspace.")]
        [ProducesResponseType(typeof(ApiResponse<IReadOnlyCollection<ClientReadModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<IReadOnlyCollection<ClientReadModel>>>> ListByWorkspace(
            int workspaceId,
            CancellationToken ct)
        {
            var clients = await _mediator.Send(
                new ListClientsByWorkspaceQuery(workspaceId),
                ct);

            return Ok(ApiResponse<IReadOnlyCollection<ClientReadModel>>.Ok(clients));
        }

        [HttpPut("{id:int}")]
        [EndpointSummary("Update client")]
        [EndpointDescription("Updates the name of an existing client.")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<int>>> Update(
            int id,
            UpdateClientRequest request,
            CancellationToken ct)
        {
            int clientId = await _mediator.Send(
                new UpdateClientCommand(
                    id,
                    request.Name,
                    Actor),
                ct);

            return Ok(ApiResponse<int>.Ok(clientId));
        }

        [HttpPatch("{id:int}/remove")]
        [EndpointSummary("Remove client")]
        [EndpointDescription("Soft deletes an existing client.")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<int>>> Remove(
            int id,
            CancellationToken ct)
        {
            int clientId = await _mediator.Send(
                new RemoveClientCommand(id, Actor),
                ct);

            return Ok(ApiResponse<int>.Ok(clientId));
        }

        [HttpPatch("{id:int}/restore")]
        [EndpointSummary("Restore client")]
        [EndpointDescription("Restores a previously removed client.")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<int>>> Restore(
            int id,
            CancellationToken ct)
        {
            int clientId = await _mediator.Send(
                new RestoreClientCommand(id, Actor),
                ct);

            return Ok(ApiResponse<int>.Ok(clientId));
        }
    }
}