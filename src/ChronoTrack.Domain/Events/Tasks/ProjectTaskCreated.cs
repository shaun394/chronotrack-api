using ChronoTrack.Domain.Common;

namespace ChronoTrack.Domain.Events.Tasks
{
    public sealed record ProjectTaskCreated : DomainEvent
    {
        public required int ProjectTaskId { get; init; }
        public required int ProjectId { get; init; }
        public required string Name { get; init; }
        public string? Description { get; init; }
        public required bool IsBillable { get; init; }
    }
}