using MediatR;

namespace ChronoTrack.Application.Projects.Commands.Restore
{
    public sealed record RestoreProjectCommand(
        int Id,
        string Actor) : IRequest<int>;
}