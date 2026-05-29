using ChronoTrack.Domain.Common;
using ChronoTrack.Domain.Exceptions;

namespace ChronoTrack.Domain.Workspaces
{
    public sealed class Workspace : AuditableEntity
    {
        private const int MaxNameLength = 100;

        private Workspace()
        {
        }

        private Workspace(string name, string actor, DateTimeOffset now)
        {
            Name = name;
            CreatedBy = actor;
            CreatedAt = now;
        }

        public string Name { get; private set; } = string.Empty;

        public static Workspace Create(string name, string actor, DateTimeOffset now)
        {
            ValidateName(name);

            return new Workspace(name.Trim(), actor, now);
        }

        public void Update(string name, string actor, DateTimeOffset now)
        {
            if (IsRemoved)
            {
                throw new DomainValidationException("Removed workspaces cannot be updated.");
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
                throw new DomainValidationException("Workspace is already removed.");
            }

            RemovedBy = actor;
            RemovedAt = now;
        }

        public void Restore(string actor, DateTimeOffset now)
        {
            if (!IsRemoved)
            {
                throw new DomainValidationException("Workspace is not removed.");
            }

            RemovedBy = null;
            RemovedAt = null;
            UpdatedBy = actor;
            UpdatedAt = now;
        }

        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainValidationException("Workspace name is required.");
            }

            if (name.Trim().Length > MaxNameLength)
            {
                throw new DomainValidationException($"Workspace name cannot exceed {MaxNameLength} characters.");
            }
        }
    }
}