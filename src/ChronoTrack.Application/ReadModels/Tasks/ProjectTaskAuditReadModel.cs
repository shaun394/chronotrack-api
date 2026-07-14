namespace ChronoTrack.Application.ReadModels.Tasks
{
    public sealed class ProjectTaskAuditReadModel
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
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
        public List<ProjectTaskAuditEntry> Entries { get; set; } = [];
    }
}