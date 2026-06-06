using MediatR;

namespace ChronoTrack.Application.Projects.Commands.Update
{
    public sealed record UpdateProjectCommand(
        int Id,
        int? ClientId,
        string Name,
        string? Description,
        bool IsBillable,
        string Actor) : IRequest<int>;
}
