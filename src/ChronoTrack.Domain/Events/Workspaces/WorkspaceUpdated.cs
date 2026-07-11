using ChronoTrack.Domain.Common;

namespace ChronoTrack.Domain.Events.Workspaces
{
    public sealed record WorkspaceUpdated : DomainEvent
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}