using MediatR;

namespace ChronoTrack.Application.Clients.Commands.Restore
{
    public sealed record RestoreClientCommand(
        int Id,
        string Actor) : IRequest<int>;
}