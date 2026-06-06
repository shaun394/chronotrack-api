using MediatR;

namespace ChronoTrack.Application.Projects.Commands.Create
{
    public sealed record CreateProjectCommand(
        int WorkspaceId,
        int? ClientId,
        string Name,
        string? Description,
        bool IsBillable,
        string Actor) : IRequest<int>;
}
