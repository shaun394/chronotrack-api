using ChronoTrack.Domain.Common;
using ChronoTrack.Domain.Exceptions;

namespace ChronoTrack.Domain.Tags
{
    public sealed class Tag : AuditableEntity
    {
        private const int MaxNameLength = 50;

        private Tag()
        {
        }

        private Tag(
            int workspaceId,
            string name,
            string actor,
            DateTimeOffset now)
        {
            WorkspaceId = workspaceId;
            Name = name;
            CreatedBy = actor;
            CreatedAt = now;
        }

        public int WorkspaceId { get; private set; }
        public string Name { get; private set; } = string.Empty;

        public static Tag Create(
            int workspaceId,
            string name,
            string actor,
            DateTimeOffset now)
        {
            ValidateWorkspaceId(workspaceId);
            ValidateName(name);

            return new Tag(
                workspaceId,
                name.Trim(),
                actor,
                now);
        }

        public void Update(
            string name,
            string actor,
            DateTimeOffset now)
        {
            if (IsRemoved)
            {
                throw new DomainValidationException("Removed tags cannot be updated.");
            }

            ValidateName(name);

            Name = name.Trim();
            UpdatedBy = actor;
            UpdatedAt = now;
        }

        public void Remove(string actor, DateTimeOffset now)
        {
            if (IsRemoved)
            {
                throw new DomainValidationException("Tag is already removed.");
            }

            RemovedBy = actor;
            RemovedAt = now;
        }

        public void Restore(string actor, DateTimeOffset now)
        {
            if (!IsRemoved)
            {
                throw new DomainValidationException("Tag is not removed.");
            }

            RemovedBy = null;
            RemovedAt = null;
            UpdatedBy = actor;
            UpdatedAt = now;
        }

        private static void ValidateWorkspaceId(int workspaceId)
        {
            if (workspaceId <= 0)
            {
                throw new DomainValidationException("Workspace id is required.");
            }
        }

        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainValidationException("Tag name is required.");
            }

            if (name.Trim().Length > MaxNameLength)
            {
                throw new DomainValidationException($"Tag name cannot exceed {MaxNameLength} characters.");
            }
        }
    }
}