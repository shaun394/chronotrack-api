namespace ChronoTrack.Application.ReadModels.Reports
{
    public sealed class DailyTimeSummaryReadModel
    {
        public DateOnly WorkDate { get; init; }
        public int TotalMinutes { get; init; }
        public int BillableMinutes { get; init; }
        public int NonBillableMinutes { get; init; }
        public int EntryCount { get; init; }
    }
}