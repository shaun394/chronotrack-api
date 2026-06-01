using MediatR;

namespace ChronoTrack.Application.Workspaces.Commands.Remove
{
    public sealed record RemoveWorkspaceCommand(
        int Id,
        string Actor) : IRequest<int>;
}