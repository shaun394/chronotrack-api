using ChronoTrack.Domain.Common;
using ChronoTrack.Domain.Exceptions;

namespace ChronoTrack.Domain.Clients
{
    public sealed class Client : AuditableEntity
    {
        private const int MaxNameLength = 100;

        public Client() { }

        private Client(int workspaceId, string name, string actor, DateTimeOffset now)
        {
            WorkspaceId = workspaceId;
            Name = name;
            CreatedBy = actor;
            CreatedAt = now;
        }

        public int WorkspaceId { get; private set; }
        public string Name { get; private set; } = string.Empty;

        public static Client Create(
            int workspaceId,
            string name,
            string actor,
            DateTimeOffset now)
        {
            ValidateWorkspaceId(workspaceId);
            ValidateName(name);

            return new Client(
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
                throw new DomainValidationException("Removed clients cannot be updated.");

            ValidateName(name);

            Name = name.Trim();
            UpdatedBy = actor;
            UpdatedAt = now;
        }

        public void Remove(string actor, DateTimeOffset now)
        {
            if (IsRemoved)
                throw new DomainValidationException("Client is already removed.");

            RemovedBy = actor;
            RemovedAt = now;
        }

        public void Restore(string actor, DateTimeOffset now)
        {
            if (!IsRemoved)
                throw new DomainValidationException("Client is already removed.");

            RemovedBy = null;
            RemovedAt = null;
            UpdatedBy = actor;
            UpdatedAt = now;
        }

        private static void ValidateWorkspaceId(int workspaceId)
        {
            if (workspaceId <= 0)
                throw new DomainValidationException("Workspace id is required.");
        }

        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainValidationException("Client name is required.");

            if (name.Trim().Length > MaxNameLength)
                throw new DomainValidationException($"Client name cannot exceed {MaxNameLength} characters.");
        }
    }
}
