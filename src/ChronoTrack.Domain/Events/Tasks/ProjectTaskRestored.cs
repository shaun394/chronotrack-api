using ChronoTrack.Domain.Common;

namespace ChronoTrack.Domain.Events.Tasks
{
    public sealed record ProjectTaskRestored : DomainEvent
    {
        public required int ProjectTaskId { get; init; }
        public required int ProjectId { get; init; }
    }
}