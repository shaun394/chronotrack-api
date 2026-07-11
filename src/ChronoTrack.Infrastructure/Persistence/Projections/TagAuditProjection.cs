using ChronoTrack.Application.ReadModels.Tags;
using ChronoTrack.Domain.Events.Tags;
using Marten.Events.Projections;

namespace ChronoTrack.Infrastructure.Persistence.Projections
{
    public sealed class TagAuditProjection
        : MultiStreamProjection<TagAuditReadModel, int>
    {
        public TagAuditProjection()
        {
            Identity<TagCreated>(x => x.TagId);
            Identity<TagUpdated>(x => x.TagId);
            Identity<TagRemoved>(x => x.TagId);
            Identity<TagRestored>(x => x.TagId);
        }

        public TagAuditReadModel Create(TagCreated @event)
        {
            return new TagAuditReadModel
            {
                Id = @event.TagId,
                WorkspaceId = @event.WorkspaceId,
                Name = @event.Name,
                CreatedBy = @event.Actor,
                CreatedAt = @event.OccurredAt,
                ModifiedBy = @event.Actor,
                ModifiedAt = @event.OccurredAt,
                Entries =
                [
                    new TagAuditEntry
                    {
                        EventType = nameof(TagCreated),
                        Actor = @event.Actor,
                        OccurredAt = @event.OccurredAt,
                        Changes = new Dictionary<string, string?>
                        {
                            ["WorkspaceId"] = @event.WorkspaceId.ToString(),
                            ["Name"] = @event.Name
                        }
                    }
                ]
            };
        }

        public void Apply(
            TagUpdated @event,
            TagAuditReadModel model)
        {
            model.WorkspaceId = @event.WorkspaceId;
            model.Name = @event.Name;
            model.ModifiedBy = @event.Actor;
            model.ModifiedAt = @event.OccurredAt;

            model.Entries.Add(new TagAuditEntry
            {
                EventType = nameof(TagUpdated),
                Actor = @event.Actor,
                OccurredAt = @event.OccurredAt,
                Changes = new Dictionary<string, string?>
                {
                    ["WorkspaceId"] = @event.WorkspaceId.ToString(),
                    ["Name"] = @event.Name
                }
            });
        }

        public void Apply(
            TagRemoved @event,
            TagAuditReadModel model)
        {
            model.RemovedBy = @event.Actor;
            model.RemovedAt = @event.OccurredAt;
            model.ModifiedBy = @event.Actor;
            model.ModifiedAt = @event.OccurredAt;

            model.Entries.Add(new TagAuditEntry
            {
                EventType = nameof(TagRemoved),
                Actor = @event.Actor,
                OccurredAt = @event.OccurredAt,
                Changes = new Dictionary<string, string?>
                {
                    ["RemovedAt"] = @event.OccurredAt.ToString("O")
                }
            });
        }

        public void Apply(
            TagRestored @event,
            TagAuditReadModel model)
        {
            model.RemovedBy = null;
            model.RemovedAt = null;
            model.RestoredBy = @event.Actor;
            model.RestoredAt = @event.OccurredAt;
            model.ModifiedBy = @event.Actor;
            model.ModifiedAt = @event.OccurredAt;

            model.Entries.Add(new TagAuditEntry
            {
                EventType = nameof(TagRestored),
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