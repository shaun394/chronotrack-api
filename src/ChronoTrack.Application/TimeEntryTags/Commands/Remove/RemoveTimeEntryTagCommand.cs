using MediatR;

namespace ChronoTrack.Application.TimeEntryTags.Commands.Remove
{
    public sealed record RemoveTimeEntryTagCommand(
        int TimeEntryId,
        int TagId,
        string Actor) : IRequest<int>;
}