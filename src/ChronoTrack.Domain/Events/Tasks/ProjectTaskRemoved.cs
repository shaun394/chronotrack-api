using ChronoTrack.Domain.Common;

namespace ChronoTrack.Domain.Events.Tasks
{
    public sealed record ProjectTaskRemoved : DomainEvent
    {
        public required int ProjectTaskId { get; init; }
        public required int ProjectId { get; init; }
    }
}