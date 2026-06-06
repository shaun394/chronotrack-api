using ChronoTrack.Domain.Common;

namespace ChronoTrack.Domain.Events.Projects
{
    public sealed record ProjectRestored : DomainEvent
    {
        public required int ProjectId { get; init; }
        public required int WorkspaceId { get; init; }
    }
}