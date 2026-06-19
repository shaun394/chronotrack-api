using ChronoTrack.Domain.Common;
using ChronoTrack.Domain.Common.Exceptions;

namespace ChronoTrack.Domain.Workspaces
{
    public sealed class Workspace : AuditableEntity
    {
        private const int MaxNameLength = 100;
        private const int MaxDescriptionLength = 500;

        private Workspace()
        {
        }

        private Workspace(
            string name,
            string? description,
            string actor,
            DateTimeOffset now)
        {
            Name = name;
            Description = description;
            CreatedBy = actor;
            CreatedAt = now;
            ModifiedBy = actor;
            ModifiedAt = now;
        }

        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }

        public static Workspace Create(
            string name,
            string? description,
            string actor,
            DateTimeOffset now)
        {
            ValidateName(name);
            ValidateDescription(description);

            return new Workspace(
                name.Trim(),
                description?.Trim(),
                actor,
                now);
        }

        public void Update(
            string name,
            string? description,
            string actor,
            DateTimeOffset now)
        {
            if (IsRemoved)
            {
                throw new DomainValidationException(
                    nameof(Workspace),
                    "Removed workspaces cannot be updated.");
            }

            ValidateName(name);
            ValidateDescription(description);

            Name = name.Trim();
            Description = description?.Trim();
            ModifiedBy = actor;
            ModifiedAt = now;
        }

        public void Remove(string actor, DateTimeOffset now)
        {
            if (IsRemoved)
            {
                throw new DomainValidationException(
                    nameof(Workspace),
                    "Workspace is already removed.");
            }

            RemovedBy = actor;
            RemovedAt = now;
        }

        public void Restore(string actor, DateTimeOffset now)
        {
            if (!IsRemoved)
            {
                throw new DomainValidationException(
                    nameof(Workspace),
                    "Workspace is not removed.");
            }

            RemovedBy = null;
            RemovedAt = null;
            RestoredBy = actor;
            RestoredAt = now;
            ModifiedBy = actor;
            ModifiedAt = now;
        }

        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainValidationException(
                    nameof(Workspace),
                    "Workspace name is required.");
            }

            if (name.Trim().Length > MaxNameLength)
            {
                throw new DomainValidationException(
                    nameof(Workspace),
                    $"Workspace name cannot exceed {MaxNameLength} characters.");
            }
        }

        private static void ValidateDescription(string? description)
        {
            if (description?.Trim().Length > MaxDescriptionLength)
            {
                throw new DomainValidationException(
                    nameof(Workspace),
                    $"Workspace description cannot exceed {MaxDescriptionLength} characters.");
            }
        }
    }
}