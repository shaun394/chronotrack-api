using MediatR;

namespace ChronoTrack.Application.Tasks.Commands.Restore
{
    public sealed record RestoreProjectTaskCommand(
        int Id,
        string Actor) : IRequest<int>;
}