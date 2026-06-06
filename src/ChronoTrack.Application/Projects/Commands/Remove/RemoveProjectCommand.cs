using MediatR;

namespace ChronoTrack.Application.Projects.Commands.Remove
{
    public sealed record RemoveProjectCommand(
        int Id,
        string Actor) : IRequest<int>;
}