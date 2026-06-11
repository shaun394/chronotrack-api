using MediatR;

namespace ChronoTrack.Application.Tags.Commands.Remove
{
    public sealed record RemoveTagCommand(
        int Id,
        string Actor) : IRequest<int>;
}