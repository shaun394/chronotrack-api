using MediatR;

namespace ChronoTrack.Application.Workspaces.Commands.Create
{
    public sealed record CreateWorkspaceCommand(
        string Name,
        string? Description,
        string Actor) : IRequest<int>;
}