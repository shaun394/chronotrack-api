namespace ChronoTrack.Application.ReadModels.Reports
{
    public sealed class ProjectTimeSummaryReadModel
    {
        public int ProjectId { get; init; }
        public string ProjectName { get; init; } = string.Empty;
        public int TotalMinutes { get; init; }
        public int BillableMinutes { get; init; }
        public int NonBillableMinutes { get; init; }
        public int EntryCount { get; init; }
    }
}