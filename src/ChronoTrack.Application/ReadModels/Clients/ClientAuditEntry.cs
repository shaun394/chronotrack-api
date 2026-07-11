namespace ChronoTrack.Application.ReadModels.Clients
{
    public sealed class ClientAuditEntry
    {
        public string EventType { get; set; } = string.Empty;
        public string Actor { get; set; } = string.Empty;
        public DateTimeOffset OccurredAt { get; set; }
        public Dictionary<string, string?> Changes { get; set; } = [];
    }
}