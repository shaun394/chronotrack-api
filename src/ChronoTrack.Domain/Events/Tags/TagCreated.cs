using ChronoTrack.Domain.Common;

namespace ChronoTrack.Domain.Events.Tags
{
    public sealed record TagCreated : DomainEvent
    {
        public required int TagId { get; init; }
        public required int WorkspaceId { get; init; }
        public required string Name { get; init; }
    }
}