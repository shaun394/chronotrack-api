using MediatR;

namespace ChronoTrack.Application.Tags.Commands.Restore
{
    public sealed record RestoreTagCommand(
        int Id,
        string Actor) : IRequest<int>;
}