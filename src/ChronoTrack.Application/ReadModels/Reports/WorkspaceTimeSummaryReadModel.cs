namespace ChronoTrack.Application.ReadModels.Reports
{
    public sealed class WorkspaceTimeSummaryReadModel
    {
        public int WorkspaceId { get; init; }
        public DateOnly From { get; init; }
        public DateOnly To { get; init; }
        public int TotalMinutes { get; init; }
        public int BillableMinutes { get; init; }
        public int NonBillableMinutes { get; init; }
        public int EntryCount { get; init; }
    }
}