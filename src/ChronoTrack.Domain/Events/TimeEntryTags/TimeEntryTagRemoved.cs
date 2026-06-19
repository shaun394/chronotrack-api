using ChronoTrack.Domain.Common;

namespace ChronoTrack.Domain.Events.TimeEntryTags
{
    public sealed record TimeEntryTagRemoved : DomainEvent
    {
        public required int TimeEntryTagId { get; init; }
        public required int WorkspaceId { get; init; }
        public required int TimeEntryId { get; init; }
        public required int TagId { get; init; }
    }
}