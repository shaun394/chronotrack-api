using ChronoTrack.Domain.Common;

namespace ChronoTrack.Domain.Events.TimeEntries
{
    public sealed record TimeEntryCreated : DomainEvent
    {
        public required int TimeEntryId { get; init; }
        public required int WorkspaceId { get; init; }
        public required int ProjectId { get; init; }
        public int? ProjectTaskId { get; init; }
        public int? ClientId { get; init; }
        public string? Description { get; init; }
        public required DateOnly WorkDate { get; init; }
        public required TimeOnly StartTime { get; init; }
        public required TimeOnly EndTime { get; init; }
        public required int DurationMinutes { get; init; }
        public required bool IsBillable { get; init; }
    }
}