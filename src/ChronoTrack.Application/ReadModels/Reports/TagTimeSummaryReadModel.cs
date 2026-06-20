namespace ChronoTrack.Application.ReadModels.Reports
{
    public sealed class TagTimeSummaryReadModel
    {
        public int TagId { get; init; }
        public string TagName { get; init; } = string.Empty;
        public int TotalMinutes { get; init; }
        public int EntryCount { get; init; }
    }
}