using ChronoTrack.Domain.Common;

namespace ChronoTrack.Domain.Events.Tags
{
    public sealed record TagRemoved : DomainEvent
    {
        public required int TagId { get; init; }
        public required int WorkspaceId { get; init; }
    }
}