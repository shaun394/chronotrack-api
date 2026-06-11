using MediatR;

namespace ChronoTrack.Application.Tags.Commands.Create
{
    public sealed record CreateTagCommand(
        int WorkspaceId,
        string Name,
        string Actor) : IRequest<int>;
}