using MediatR;

namespace ChronoTrack.Application.Clients.Commands.Remove
{
    public sealed record RemoveClientCommand(
        int Id,
        string Actor) : IRequest<int>;
}