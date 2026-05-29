using ChronoTrack.Domain.Common;

namespace ChronoTrack.Domain.Events.Workspaces
{
    public sealed record WorkspaceUpdated : DomainEvent
    {
        public required int WorkspaceId { get; init; }

        public required string Name { get; init; }
    }
}