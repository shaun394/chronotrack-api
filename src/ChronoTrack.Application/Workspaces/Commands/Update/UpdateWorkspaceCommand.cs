using MediatR;

namespace ChronoTrack.Application.Workspaces.Commands.Update
{
    public sealed record UpdateWorkspaceCommand(
        int Id,
        string Name,
        string Actor) : IRequest<int>;
}