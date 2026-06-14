using MediatR;

namespace ChronoTrack.Application.Tasks.Commands.Remove
{
    public sealed record RemoveProjectTaskCommand(
        int Id,
        string Actor) : IRequest<int>;
}