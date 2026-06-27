namespace ChronoTrack.Application.ReadModels.Audit.Workspaces
{
    public sealed class WorkspaceAuditReadModel
    {
        public int Version { get; init; }
        public string EventType { get; init; } = string.Empty;
        public int WorkspaceId { get; init; }
        public string? Name { get; init; }
        public string Actor { get; init; } = string.Empty;
        public DateTimeOffset OccurredAt { get; init; }
    }
}