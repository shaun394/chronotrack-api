namespace ChronoTrack.Domain.Common
{
    public abstract class AuditableEntity
    {
        public int Id { get; protected set; }

        public DateTimeOffset CreatedAt { get; protected set; }

        public string CreatedBy { get; protected set; } = string.Empty;

        public DateTimeOffset ModifiedAt { get; protected set; }

        public string ModifiedBy { get; protected set; } = string.Empty;

        public DateTimeOffset? RemovedAt { get; protected set; }

        public string? RemovedBy { get; protected set; }

        public DateTimeOffset? RestoredAt { get; protected set; }

        public string? RestoredBy { get; protected set; }

        public bool IsRemoved => RemovedAt.HasValue;
    }
}