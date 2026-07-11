using ChronoTrack.Application.ReadModels.Projects;
using ChronoTrack.Domain.Events.Project;
using ChronoTrack.Domain.Events.Projects;
using Marten.Events.Projections;

namespace ChronoTrack.Infrastructure.Persistence.Projections
{
    public sealed class ProjectAuditProjection
        : MultiStreamProjection<ProjectAuditReadModel, int>
    {
        public ProjectAuditProjection()
        {
            Identity<ProjectCreated>(x => x.ProjectId);
            Identity<ProjectUpdated>(x => x.ProjectId);
            Identity<ProjectRemoved>(x => x.ProjectId);
            Identity<ProjectRestored>(x => x.ProjectId);
        }

        public ProjectAuditReadModel Create(ProjectCreated @event)
        {
            return new ProjectAuditReadModel
            {
                Id = @event.ProjectId,
                WorkspaceId = @event.WorkspaceId,
                ClientId = @event.ClientId,
                Name = @event.Name,
                Description = @event.Description,
                IsBillable = @event.IsBillable,
                CreatedBy = @event.Actor,
                CreatedAt = @event.OccurredAt,
                ModifiedBy = @event.Actor,
                ModifiedAt = @event.OccurredAt,
                Entries =
                [
                    new ProjectAuditEntry
                    {
                        EventType = nameof(ProjectCreated),
                        Actor = @event.Actor,
                        OccurredAt = @event.OccurredAt,
                        Changes = new Dictionary<string, string?>
                        {
                            ["WorkspaceId"] = @event.WorkspaceId.ToString(),
                            ["ClientId"] = @event.ClientId?.ToString(),
                            ["Name"] = @event.Name,
                            ["Description"] = @event.Description,
                            ["IsBillable"] = @event.IsBillable.ToString()
                        }
                    }
                ]
            };
        }

        public void Apply(
            ProjectUpdated @event,
            ProjectAuditReadModel model)
        {
            model.WorkspaceId = @event.WorkspaceId;
            model.ClientId = @event.ClientId;
            model.Name = @event.Name;
            model.Description = @event.Description;
            model.IsBillable = @event.IsBillable;
            model.ModifiedBy = @event.Actor;
            model.ModifiedAt = @event.OccurredAt;

            model.Entries.Add(new ProjectAuditEntry
            {
                EventType = nameof(ProjectUpdated),
                Actor = @event.Actor,
                OccurredAt = @event.OccurredAt,
                Changes = new Dictionary<string, string?>
                {
                    ["WorkspaceId"] = @event.WorkspaceId.ToString(),
                    ["ClientId"] = @event.ClientId?.ToString(),
                    ["Name"] = @event.Name,
                    ["Description"] = @event.Description,
                    ["IsBillable"] = @event.IsBillable.ToString()
                }
            });
        }

        public void Apply(
            ProjectRemoved @event,
            ProjectAuditReadModel model)
        {
            model.RemovedBy = @event.Actor;
            model.RemovedAt = @event.OccurredAt;
            model.ModifiedBy = @event.Actor;
            model.ModifiedAt = @event.OccurredAt;

            model.Entries.Add(new ProjectAuditEntry
            {
                EventType = nameof(ProjectRemoved),
                Actor = @event.Actor,
                OccurredAt = @event.OccurredAt,
                Changes = new Dictionary<string, string?>
                {
                    ["RemovedAt"] = @event.OccurredAt.ToString("O")
                }
            });
        }

        public void Apply(
            ProjectRestored @event,
            ProjectAuditReadModel model)
        {
            model.RemovedBy = null;
            model.RemovedAt = null;
            model.RestoredBy = @event.Actor;
            model.RestoredAt = @event.OccurredAt;
            model.ModifiedBy = @event.Actor;
            model.ModifiedAt = @event.OccurredAt;

            model.Entries.Add(new ProjectAuditEntry
            {
                EventType = nameof(ProjectRestored),
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