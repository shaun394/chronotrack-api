using MediatR;

namespace ChronoTrack.Application.Tasks.Commands.Create
{
    public sealed record CreateProjectTaskCommand(
        int ProjectId,
        string Name,
        string? Description,
        bool IsBillable,
        string Actor) : IRequest<int>;
}