using ChronoTrack.Application.ReadModels.Workspaces;
using ChronoTrack.Domain.Events.Workspaces;
using Marten.Events.Projections;

namespace ChronoTrack.Infrastructure.Persistence.Projections
{
    public sealed class WorkspaceAuditProjection
        : MultiStreamProjection<WorkspaceAuditReadModel, int>
    {
        public WorkspaceAuditProjection()
        {
            Identity<WorkspaceCreated>(x => x.Id);
            Identity<WorkspaceUpdated>(x => x.Id);
            Identity<WorkspaceRemoved>(x => x.Id);
            Identity<WorkspaceRestored>(x => x.Id);
        }

        public WorkspaceAuditReadModel Create(WorkspaceCreated @event)
        {
            return new WorkspaceAuditReadModel
            {
                Id = @event.Id,
                Name = @event.Name,
                Description = @event.Description,
                CreatedBy = @event.Actor,
                CreatedAt = @event.OccurredAt,
                Entries =
                [
                    new WorkspaceAuditEntry
                    {
                        EventType = nameof(WorkspaceCreated),
                        Actor = @event.Actor,
                        OccurredAt = @event.OccurredAt,
                        Changes = new Dictionary<string, string?>
                        {
                            ["Name"] = @event.Name,
                            ["Description"] = @event.Description
                        }
                    }
                ]
            };
        }

        public void Apply(
            WorkspaceUpdated @event,
            WorkspaceAuditReadModel model)
        {
            model.Name = @event.Name;
            model.Description = @event.Description;
            model.ModifiedBy = @event.Actor;
            model.ModifiedAt = @event.OccurredAt;

            model.Entries.Add(new WorkspaceAuditEntry
            {
                EventType = nameof(WorkspaceUpdated),
                Actor = @event.Actor,
                OccurredAt = @event.OccurredAt,
                Changes = new Dictionary<string, string?>
                {
                    ["Name"] = @event.Name,
                    ["Description"] = @event.Description
                }
            });
        }

        public void Apply(
            WorkspaceRemoved @event,
            WorkspaceAuditReadModel model)
        {
            model.RemovedBy = @event.Actor;
            model.RemovedAt = @event.OccurredAt;
            model.ModifiedBy = @event.Actor;
            model.ModifiedAt = @event.OccurredAt;

            model.Entries.Add(new WorkspaceAuditEntry
            {
                EventType = nameof(WorkspaceRemoved),
                Actor = @event.Actor,
                OccurredAt = @event.OccurredAt,
                Changes = new Dictionary<string, string?>
                {
                    ["RemovedAt"] = @event.OccurredAt.ToString("O")
                }
            });
        }

        public void Apply(
            WorkspaceRestored @event,
            WorkspaceAuditReadModel model)
        {
            model.RemovedBy = null;
            model.RemovedAt = null;
            model.RestoredBy = @event.Actor;
            model.RestoredAt = @event.OccurredAt;
            model.ModifiedBy = @event.Actor;
            model.ModifiedAt = @event.OccurredAt;

            model.Entries.Add(new WorkspaceAuditEntry
            {
                EventType = nameof(WorkspaceRestored),
                Actor = @event.Actor,
                OccurredAt = @event.OccurredAt,
                Changes = new Dictionary<string, string?>
                {
                    ["RestoredAt"] = @event.OccurredAt.ToString("O")
                }
            });
        }
    }
}