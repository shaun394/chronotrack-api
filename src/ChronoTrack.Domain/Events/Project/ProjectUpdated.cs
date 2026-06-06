using ChronoTrack.Domain.Common;

namespace ChronoTrack.Domain.Events.Project
{
    public sealed record ProjectUpdated : DomainEvent
    {
        public required int ProjectId { get; init; }
        public required int WorkspaceId { get; init; }
        public int? ClientId { get; init; }
        public required string Name { get; init; }
        public string? Description { get; init; }
        public required bool IsBillable { get; init; }
    }
}
