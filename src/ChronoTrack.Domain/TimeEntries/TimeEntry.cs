using ChronoTrack.Domain.Common;
using ChronoTrack.Domain.Common.Exceptions;

namespace ChronoTrack.Domain.TimeEntries
{
    public sealed class TimeEntry : AuditableEntity
    {
        private const int MaxDescriptionLength = 500;

        private TimeEntry()
        {
        }

        private TimeEntry(
            int workspaceId,
            int projectId,
            int? projectTaskId,
            int? clientId,
            string? description,
            DateOnly workDate,
            TimeOnly startTime,
            TimeOnly endTime,
            bool isBillable,
            string actor,
            DateTimeOffset now)
        {
            WorkspaceId = workspaceId;
            ProjectId = projectId;
            ProjectTaskId = projectTaskId;
            ClientId = clientId;
            Description = description;
            WorkDate = workDate;
            StartTime = startTime;
            EndTime = endTime;
            DurationMinutes = CalculateDurationMinutes(startTime, endTime);
            IsBillable = isBillable;
            CreatedBy = actor;
            CreatedAt = now;
            ModifiedBy = actor;
            ModifiedAt = now;
        }

        public int WorkspaceId { get; private set; }
        public int ProjectId { get; private set; }
        public int? ProjectTaskId { get; private set; }
        public int? ClientId { get; private set; }
        public string? Description { get; private set; }
        public DateOnly WorkDate { get; private set; }
        public TimeOnly StartTime { get; private set; }
        public TimeOnly EndTime { get; private set; }
        public int DurationMinutes { get; private set; }
        public bool IsBillable { get; private set; }

        public static TimeEntry Create(
            int workspaceId,
            int projectId,
            int? projectTaskId,
            int? clientId,
            string? description,
            DateOnly workDate,
            TimeOnly startTime,
            TimeOnly endTime,
            bool isBillable,
            string actor,
            DateTimeOffset now)
        {
            ValidateWorkspaceId(workspaceId);
            ValidateProjectId(projectId);
            ValidateProjectTaskId(projectTaskId);
            ValidateClientId(clientId);
            ValidateDescription(description);
            ValidateTimeRange(startTime, endTime);

            return new TimeEntry(
                workspaceId,
                projectId,
                projectTaskId,
                clientId,
                description?.Trim(),
                workDate,
                startTime,
                endTime,
                isBillable,
                actor,
                now);
        }

        public void Update(
            int projectId,
            int? projectTaskId,
            int? clientId,
            string? description,
            DateOnly workDate,
            TimeOnly startTime,
            TimeOnly endTime,
            bool isBillable,
            string actor,
            DateTimeOffset now)
        {
            if (IsRemoved)
            {
                throw new DomainValidationException(
                    nameof(TimeEntry),
                    "Removed time entries cannot be updated.");
            }

            ValidateProjectId(projectId);
            ValidateProjectTaskId(projectTaskId);
            ValidateClientId(clientId);
            ValidateDescription(description);
            ValidateTimeRange(startTime, endTime);

            ProjectId = projectId;
            ProjectTaskId = projectTaskId;
            ClientId = clientId;
            Description = description?.Trim();
            WorkDate = workDate;
            StartTime = startTime;
            EndTime = endTime;
            DurationMinutes = CalculateDurationMinutes(startTime, endTime);
            IsBillable = isBillable;
            ModifiedBy = actor;
            ModifiedAt = now;
        }

        public void Remove(string actor, DateTimeOffset now)
        {
            if (IsRemoved)
            {
                throw new DomainValidationException(
                    nameof(TimeEntry),
                    "Time entry is already removed.");
            }

            RemovedBy = actor;
            RemovedAt = now;
        }

        public void Restore(string actor, DateTimeOffset now)
        {
            if (!IsRemoved)
            {
                throw new DomainValidationException(
                    nameof(TimeEntry),
                    "Time entry is not removed.");
            }

            RemovedBy = null;
            RemovedAt = null;
            RestoredBy = actor;
            RestoredAt = now;
            ModifiedBy = actor;
            ModifiedAt = now;
        }

        private static void ValidateWorkspaceId(int workspaceId)
        {
            if (workspaceId <= 0)
            {
                throw new DomainValidationException(
                    nameof(TimeEntry),
                    "Workspace id is required.");
            }
        }

        private static void ValidateProjectId(int projectId)
        {
            if (projectId <= 0)
            {
                throw new DomainValidationException(
                    nameof(TimeEntry),
                    "Project id is required.");
            }
        }

        private static void ValidateProjectTaskId(int? projectTaskId)
        {
            if (projectTaskId.HasValue && projectTaskId.Value <= 0)
            {
                throw new DomainValidationException(
                    nameof(TimeEntry),
                    "Project task id must be greater than 0.");
            }
        }

        private static void ValidateClientId(int? clientId)
        {
            if (clientId.HasValue && clientId.Value <= 0)
            {
                throw new DomainValidationException(
                    nameof(TimeEntry),
                    "Client id must be greater than 0.");
            }
        }

        private static void ValidateDescription(string? description)
        {
            if (description?.Trim().Length > MaxDescriptionLength)
            {
                throw new DomainValidationException(
                    nameof(TimeEntry),
                    $"Time entry description cannot exceed {MaxDescriptionLength} characters.");
            }
        }

        private static void ValidateTimeRange(
            TimeOnly startTime,
            TimeOnly endTime)
        {
            if (startTime >= endTime)
            {
                throw new DomainValidationException(
                    nameof(TimeEntry),
                    "Start time must be before end time.");
            }
        }

        private static int CalculateDurationMinutes(
            TimeOnly startTime,
            TimeOnly endTime)
        {
            return (int)(endTime.ToTimeSpan() - startTime.ToTimeSpan()).TotalMinutes;
        }
    }
}