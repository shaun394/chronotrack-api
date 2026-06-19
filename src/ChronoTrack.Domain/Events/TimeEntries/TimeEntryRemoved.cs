using ChronoTrack.Domain.Common;

namespace ChronoTrack.Domain.Events.TimeEntries
{
    public sealed record TimeEntryRemoved : DomainEvent
    {
        public required int TimeEntryId { get; init; }
        public required int WorkspaceId { get; init; }
    }
}