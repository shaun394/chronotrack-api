using MediatR;

namespace ChronoTrack.Application.TimeEntries.Commands.Remove
{
    public sealed record RemoveTimeEntryCommand(
        int Id,
        string Actor) : IRequest<int>;
}