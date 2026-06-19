using MediatR;

namespace ChronoTrack.Application.TimeEntryTags.Commands.Add
{
    public sealed record AddTimeEntryTagCommand(
        int TimeEntryId,
        int TagId,
        string Actor) : IRequest<int>;
}