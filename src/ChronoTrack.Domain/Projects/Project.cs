using ChronoTrack.Domain.Common;
using ChronoTrack.Domain.Exceptions;

namespace ChronoTrack.Domain.Projects
{
    public sealed class Project : AuditableEntity
    {
        private const int MaxNameLength = 100;
        private const int MaxDescriptionLength = 500;

        public Project() { }

        public Project(
            int workspaceId,
            int? clientId,
            string name,
            string? description,
            bool idBillable,
            string actor,
            DateTimeOffset now)
        {
            WorkspaceId = workspaceId;
            ClientId = clientId;
            Name = name;
            Description = description;
            IsBillable = idBillable;
            CreatedBy = actor;
            CreatedAt = now;
        }

        public int WorkspaceId { get; private set; }
        public int? ClientId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public bool IsBillable { get; private set; }

        public static Project Create(
            int workspaceId,
            int? clientId,
            string name,
            string? description,
            bool isBillable,
            string actor,
            DateTimeOffset now)
        {
            ValidateWorkspaceId(workspaceId);
            ValidateClientId(clientId);
            ValidateName(name);
            ValidateDescription(description);

            return new Project(
                workspaceId,
                clientId,
                name.Trim(),
                description?.Trim(),
                isBillable,
                actor,
                now);
        }

        public void Update(
            int? clientId,
            string name,
            string? description,
            bool isBillable,
            string actor,
            DateTimeOffset now)
        {
            if (IsRemoved)
                throw new DomainValidationException("Removed projects cannot be updated.");

            ValidateClientId(clientId);
            ValidateName(name);
            ValidateDescription(description);

            ClientId = clientId;
            Name = name.Trim();
            Description = description?.Trim();
            IsBillable = isBillable;
            UpdatedBy = actor;
            UpdatedAt = now;
        }

        public void Remove(string actor, DateTimeOffset now)
        {
            if (IsRemoved)
                throw new DomainValidationException("Project is already removed.");

            RemovedBy = actor;
            RemovedAt = now;
        }

        public void Restore(string actor, DateTimeOffset now)
        {
            if (!IsRemoved)
                throw new DomainValidationException("Project is not removed.");

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

        private static void ValidateClientId(int? clientId)
        {
            if (clientId.HasValue && clientId.Value <= 0)
                throw new DomainValidationException("Client id must be greater than than 0.");
        }

        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainValidationException("Project name is required.");

            if (name.Trim().Length > MaxNameLength)
                throw new DomainValidationException($"Project name cannot exceed {MaxNameLength} characters.");
        }

        private static void ValidateDescription(string? description)
        {
            if (description?.Trim().Length > MaxDescriptionLength)
                throw new DomainValidationException($"Project description cannot exceed {MaxDescriptionLength} characters.");
        }
    }
}
