using ChronoTrack.Application.ReadModels.Tasks;
using ChronoTrack.Domain.Events.Tasks;
using Marten.Events.Projections;

namespace ChronoTrack.Infrastructure.Persistence.Projections
{
    public sealed class ProjectTaskAuditProjection
        : MultiStreamProjection<ProjectTaskAuditReadModel, int>
    {
        public ProjectTaskAuditProjection()
        {
            Identity<ProjectTaskCreated>(x => x.ProjectTaskId);
            Identity<ProjectTaskUpdated>(x => x.ProjectTaskId);
            Identity<ProjectTaskRemoved>(x => x.ProjectTaskId);
            Identity<ProjectTaskRestored>(x => x.ProjectTaskId);
        }

        public ProjectTaskAuditReadModel Create(ProjectTaskCreated @event)
        {
            return new ProjectTaskAuditReadModel
            {
                Id = @event.ProjectTaskId,
                ProjectId = @event.ProjectId,
                Name = @event.Name,
                Description = @event.Description,
                IsBillable = @event.IsBillable,
                CreatedBy = @event.Actor,
                CreatedAt = @event.OccurredAt,
                ModifiedBy = @event.Actor,
                ModifiedAt = @event.OccurredAt,
                Entries =
                [
                    new ProjectTaskAuditEntry
                    {
                        EventType = nameof(ProjectTaskCreated),
                        Actor = @event.Actor,
                        OccurredAt = @event.OccurredAt,
                        Changes = new Dictionary<string, string?>
                        {
                            ["ProjectId"] = @event.ProjectId.ToString(),
                            ["Name"] = @event.Name,
                            ["Description"] = @event.Description,
                            ["IsBillable"] = @event.IsBillable.ToString()
                        }
                    }
                ]
            };
        }

        public void Apply(
            ProjectTaskUpdated @event,
            ProjectTaskAuditReadModel model)
        {
            model.ProjectId = @event.ProjectId;
            model.Name = @event.Name;
            model.Description = @event.Description;
            model.IsBillable = @event.IsBillable;
            model.ModifiedBy = @event.Actor;
            model.ModifiedAt = @event.OccurredAt;

            model.Entries.Add(new ProjectTaskAuditEntry
            {
                EventType = nameof(ProjectTaskUpdated),
                Actor = @event.Actor,
                OccurredAt = @event.OccurredAt,
                Changes = new Dictionary<string, string?>
                {
                    ["ProjectId"] = @event.ProjectId.ToString(),
                    ["Name"] = @event.Name,
                    ["Description"] = @event.Description,
                    ["IsBillable"] = @event.IsBillable.ToString()
                }
            });
        }

        public void Apply(
            ProjectTaskRemoved @event,
            ProjectTaskAuditReadModel model)
        {
            model.RemovedBy = @event.Actor;
            model.RemovedAt = @event.OccurredAt;
            model.ModifiedBy = @event.Actor;
            model.ModifiedAt = @event.OccurredAt;

            model.Entries.Add(new ProjectTaskAuditEntry
            {
                EventType = nameof(ProjectTaskRemoved),
                Actor = @event.Actor,
                OccurredAt = @event.OccurredAt,
                Changes = new Dictionary<string, string?>
                {
                    ["RemovedAt"] = @event.OccurredAt.ToString("O")
                }
            });
        }

        public void Apply(
            ProjectTaskRestored @event,
            ProjectTaskAuditReadModel model)
        {
            model.RemovedBy = null;
            model.RemovedAt = null;
            model.RestoredBy = @event.Actor;
            model.RestoredAt = @event.OccurredAt;
            model.ModifiedBy = @event.Actor;
            model.ModifiedAt = @event.OccurredAt;

            model.Entries.Add(new ProjectTaskAuditEntry
            {
                EventType = nameof(ProjectTaskRestored),
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