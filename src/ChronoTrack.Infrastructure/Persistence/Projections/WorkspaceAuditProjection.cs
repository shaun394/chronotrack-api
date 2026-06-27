using ChronoTrack.Application.ReadModels.Audit.Workspaces;
using ChronoTrack.Domain.Common;
using ChronoTrack.Domain.Events.Workspaces;

namespace ChronoTrack.Infrastructure.Persistence.Projections
{
    public sealed class WorkspaceAuditProjection
    {
        public IReadOnlyCollection<WorkspaceAuditReadModel> Project(
            IReadOnlyCollection<DomainEvent> domainEvents)
        {
            var result = new List<WorkspaceAuditReadModel>();
            int version = 1;

            foreach (var domainEvent in domainEvents)
            {
                WorkspaceAuditReadModel? readModel = domainEvent switch
                {
                    WorkspaceCreated workspaceCreated => new WorkspaceAuditReadModel
                    {
                        Version = version,
                        EventType = nameof(WorkspaceCreated),
                        WorkspaceId = workspaceCreated.WorkspaceId,
                        Name = workspaceCreated.Name,
                        Actor = workspaceCreated.Actor,
                        OccurredAt = workspaceCreated.OccurredAt
                    },

                    WorkspaceUpdated workspaceUpdated => new WorkspaceAuditReadModel
                    {
                        Version = version,
                        EventType = nameof(WorkspaceUpdated),
                        WorkspaceId = workspaceUpdated.WorkspaceId,
                        Name = workspaceUpdated.Name,
                        Actor = workspaceUpdated.Actor,
                        OccurredAt = workspaceUpdated.OccurredAt
                    },

                    WorkspaceRemoved workspaceRemoved => new WorkspaceAuditReadModel
                    {
                        Version = version,
                        EventType = nameof(WorkspaceRemoved),
                        WorkspaceId = workspaceRemoved.WorkspaceId,
                        Name = null,
                        Actor = workspaceRemoved.Actor,
                        OccurredAt = workspaceRemoved.OccurredAt
                    },

                    WorkspaceRestored workspaceRestored => new WorkspaceAuditReadModel
                    {
                        Version = version,
                        EventType = nameof(WorkspaceRestored),
                        WorkspaceId = workspaceRestored.WorkspaceId,
                        Name = null,
                        Actor = workspaceRestored.Actor,
                        OccurredAt = workspaceRestored.OccurredAt
                    },

                    _ => null
                };

                if (readModel is null)
                {
                    continue;
                }

                result.Add(readModel);
                version++;
            }

            return result;
        }
    }
}