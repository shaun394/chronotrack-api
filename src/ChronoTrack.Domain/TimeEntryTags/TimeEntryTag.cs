using ChronoTrack.Domain.Common;
using ChronoTrack.Domain.Common.Exceptions;

namespace ChronoTrack.Domain.TimeEntryTags
{
    public sealed class TimeEntryTag : AuditableEntity
    {
        private TimeEntryTag()
        {
        }

        private TimeEntryTag(
            int workspaceId,
            int timeEntryId,
            int tagId,
            string actor,
            DateTimeOffset now)
        {
            WorkspaceId = workspaceId;
            TimeEntryId = timeEntryId;
            TagId = tagId;
            CreatedBy = actor;
            CreatedAt = now;
            ModifiedBy = actor;
            ModifiedAt = now;
        }

        public int WorkspaceId { get; private set; }
        public int TimeEntryId { get; private set; }
        public int TagId { get; private set; }

        public static TimeEntryTag Create(
            int workspaceId,
            int timeEntryId,
            int tagId,
            string actor,
            DateTimeOffset now)
        {
            ValidateWorkspaceId(workspaceId);
            ValidateTimeEntryId(timeEntryId);
            ValidateTagId(tagId);

            return new TimeEntryTag(
                workspaceId,
                timeEntryId,
                tagId,
                actor,
                now);
        }

        public void Remove(string actor, DateTimeOffset now)
        {
            if (IsRemoved)
            {
                throw new DomainValidationException(
                    nameof(TimeEntryTag),
                    "Time entry tag is already removed.");
            }

            RemovedBy = actor;
            RemovedAt = now;
        }

        public void Restore(string actor, DateTimeOffset now)
        {
            if (!IsRemoved)
            {
                throw new DomainValidationException(
                    nameof(TimeEntryTag),
                    "Time entry tag is not removed.");
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
                    nameof(TimeEntryTag),
                    "Workspace id is required.");
            }
        }

        private static void ValidateTimeEntryId(int timeEntryId)
        {
            if (timeEntryId <= 0)
            {
                throw new DomainValidationException(
                    nameof(TimeEntryTag),
                    "Time entry id is required.");
            }
        }

        private static void ValidateTagId(int tagId)
        {
            if (tagId <= 0)
            {
                throw new DomainValidationException(
                    nameof(TimeEntryTag),
                    "Tag id is required.");
            }
        }
    }
}