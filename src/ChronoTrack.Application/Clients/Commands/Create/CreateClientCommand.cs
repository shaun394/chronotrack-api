using MediatR;

namespace ChronoTrack.Application.Clients.Commands.Create
{
    public sealed record CreateClientCommand(
        int WorkspaceId,
        string Name,
        string Actor) : IRequest<int>;
}
