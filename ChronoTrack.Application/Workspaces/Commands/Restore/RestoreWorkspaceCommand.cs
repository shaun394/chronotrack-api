using MediatR;

namespace ChronoTrack.Application.Workspaces.Commands.Restore
{
    public sealed record RestoreWorkspaceCommand(
        int Id,
        string Actor) : IRequest<int>;
}