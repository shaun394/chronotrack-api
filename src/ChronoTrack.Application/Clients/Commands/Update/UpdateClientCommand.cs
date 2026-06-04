using MediatR;

namespace ChronoTrack.Application.Clients.Commands.Update
{
    public sealed record UpdateClientCommand(
        int Id,
        string Name,
        string Actor) : IRequest<int>;
}