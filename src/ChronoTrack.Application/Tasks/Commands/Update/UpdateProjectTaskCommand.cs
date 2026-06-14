using MediatR;

namespace ChronoTrack.Application.Tasks.Commands.Update
{
    public sealed record UpdateProjectTaskCommand(
        int Id,
        string Name,
        string? Description,
        bool IsBillable,
        string Actor) : IRequest<int>;
}