using MediatR;

namespace ChronoTrack.Application.Workspaces.Commands.Update
{
    public sealed record UpdateWorkspaceCommand(
        int Id,
        string Name,
        string? Description,
        string Actor) : IRequest<int>;
}