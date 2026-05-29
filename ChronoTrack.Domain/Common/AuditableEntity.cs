namespace ChronoTrack.Domain.Common
{
    public abstract class AuditableEntity
    {
        public int Id { get; protected set; }
        public DateTimeOffset CreatedAt { get; protected set; }
        public string createdBy { get; protected set; } = string.Empty;
        public DateTimeOffset? UpdatedAt { get; protected set; }
        public string? UpdatedBy { get; protected set; }
        public DateTimeOffset? RemovedAt { get; protected set; }
        public string? RemovedBy { get; protected set; }
        public bool IsPrimary => RemovedAt.HasValue;
    }
}
