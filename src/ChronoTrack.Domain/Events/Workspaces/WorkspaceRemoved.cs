using ChronoTrack.Domain.Common;

namespace ChronoTrack.Domain.Events.Workspaces
{
    public sealed record WorkspaceRemoved : DomainEvent
    {
        public required int WorkspaceId { get; init; }
    }
}