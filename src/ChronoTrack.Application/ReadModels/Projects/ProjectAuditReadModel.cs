namespace ChronoTrack.Application.ReadModels.Projects
{
    public sealed class ProjectAuditReadModel
    {
        public int Id { get; set; }
        public int WorkspaceId { get; set; }
        public int? ClientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsBillable { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; }
        public string ModifiedBy { get; set; } = string.Empty;
        public DateTimeOffset ModifiedAt { get; set; }
        public string? RemovedBy { get; set; }
        public DateTimeOffset? RemovedAt { get; set; }
        public string? RestoredBy { get; set; }
        public DateTimeOffset? RestoredAt { get; set; }
        public List<ProjectAuditEntry> Entries { get; set; } = [];
    }
}