namespace ChronoTrack.Application.ReadModels.Projects
{
    public sealed class ProjectAuditEntry
    {
        public string EventType { get; set; } = string.Empty;
        public string Actor { get; set; } = string.Empty;
        public DateTimeOffset OccurredAt { get; set; }
        public Dictionary<string, string?> Changes { get; set; } = [];
    }
}