namespace ChronoTrack.Application.ReadModels.TimeEntries
{
    public sealed class TimeEntryReadModel
    {
        public int Id { get; init; }
        public int WorkspaceId { get; init; }
        public int ProjectId { get; init; }
        public int? ProjectTaskId { get; init; }
        public int? ClientId { get; init; }
        public string? Description { get; init; }
        public DateOnly WorkDate { get; init; }
        public TimeOnly StartTime { get; init; }
        public TimeOnly EndTime { get; init; }
        public int DurationMinutes { get; init; }
        public bool IsBillable { get; init; }
    }
}