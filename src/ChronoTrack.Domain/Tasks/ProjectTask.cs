using ChronoTrack.Domain.Common;
using ChronoTrack.Domain.Common.Exceptions;

namespace ChronoTrack.Domain.Tasks
{
    public sealed class ProjectTask : AuditableEntity
    {
        private const int MaxNameLength = 100;
        private const int MaxDescriptionLength = 500;

        private ProjectTask()
        {
        }

        private ProjectTask(
            int projectId,
            string name,
            string? description,
            bool isBillable,
            string actor,
            DateTimeOffset now)
        {
            ProjectId = projectId;
            Name = name;
            Description = description;
            IsBillable = isBillable;
            CreatedBy = actor;
            CreatedAt = now;
            ModifiedBy = actor;
            ModifiedAt = now;
        }

        public int ProjectId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public bool IsBillable { get; private set; }

        public static ProjectTask Create(
            int projectId,
            string name,
            string? description,
            bool isBillable,
            string actor,
            DateTimeOffset now)
        {
            ValidateProjectId(projectId);
            ValidateName(name);
            ValidateDescription(description);

            return new ProjectTask(
                projectId,
                name.Trim(),
                description?.Trim(),
                isBillable,
                actor,
                now);
        }

        public void Update(
            string name,
            string? description,
            bool isBillable,
            string actor,
            DateTimeOffset now)
        {
            if (IsRemoved)
            {
                throw new DomainValidationException(
                    nameof(ProjectTask),
                    "Removed project tasks cannot be updated.");
            }

            ValidateName(name);
            ValidateDescription(description);

            Name = name.Trim();
            Description = description?.Trim();
            IsBillable = isBillable;
            ModifiedBy = actor;
            ModifiedAt = now;
        }

        public void Remove(string actor, DateTimeOffset now)
        {
            if (IsRemoved)
            {
                throw new DomainValidationException(
                    nameof(ProjectTask),
                    "Project task is already removed.");
            }

            RemovedBy = actor;
            RemovedAt = now;
        }

        public void Restore(string actor, DateTimeOffset now)
        {
            if (!IsRemoved)
            {
                throw new DomainValidationException(
                    nameof(ProjectTask),
                    "Project task is not removed.");
            }

            RemovedBy = null;
            RemovedAt = null;
            RestoredBy = actor;
            RestoredAt = now;
            ModifiedBy = actor;
            ModifiedAt = now;
        }

        private static void ValidateProjectId(int projectId)
        {
            if (projectId <= 0)
            {
                throw new DomainValidationException(
                    nameof(ProjectTask),
                    "Project id is required.");
            }
        }

        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainValidationException(
                    nameof(ProjectTask),
                    "Project task name is required.");
            }

            if (name.Trim().Length > MaxNameLength)
            {
                throw new DomainValidationException(
                    nameof(ProjectTask),
                    $"Project task name cannot exceed {MaxNameLength} characters.");
            }
        }

        private static void ValidateDescription(string? description)
        {
            if (description?.Trim().Length > MaxDescriptionLength)
            {
                throw new DomainValidationException(
                    nameof(ProjectTask),
                    $"Project task description cannot exceed {MaxDescriptionLength} characters.");
            }
        }
    }
}