namespace ChronoTrack.Application.ReadModels.TimeEntryTags
{
    public sealed class TimeEntryTagReadModel
    {
        public int Id { get; init; }
        public int WorkspaceId { get; init; }
        public int TimeEntryId { get; init; }
        public int TagId { get; init; }
        public string TagName { get; init; } = string.Empty;
    }
}