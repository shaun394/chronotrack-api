using MediatR;

namespace ChronoTrack.Application.Tags.Commands.Update
{
    public sealed record UpdateTagCommand(
        int Id,
        string Name,
        string Actor) : IRequest<int>;
}