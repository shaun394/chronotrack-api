using ChronoTrack.Application.ReadModels.Clients;
using ChronoTrack.Domain.Events.Clients;
using Marten.Events.Projections;

namespace ChronoTrack.Infrastructure.Persistence.Projections
{
    public sealed class ClientAuditProjection
        : MultiStreamProjection<ClientAuditReadModel, int>
    {
        public ClientAuditProjection()
        {
            Identity<ClientCreated>(x => x.ClientId);
            Identity<ClientUpdated>(x => x.ClientId);
            Identity<ClientRemoved>(x => x.ClientId);
            Identity<ClientRestored>(x => x.ClientId);
        }

        public ClientAuditReadModel Create(ClientCreated @event)
        {
            return new ClientAuditReadModel
            {
                Id = @event.ClientId,
                WorkspaceId = @event.WorkspaceId,
                Name = @event.Name,
                CreatedBy = @event.Actor,
                CreatedAt = @event.OccurredAt,
                ModifiedBy = @event.Actor,
                ModifiedAt = @event.OccurredAt,
                Entries =
                [
                    new ClientAuditEntry
                    {
                        EventType = nameof(ClientCreated),
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
            ClientUpdated @event,
            ClientAuditReadModel model)
        {
            model.WorkspaceId = @event.WorkspaceId;
            model.Name = @event.Name;
            model.ModifiedBy = @event.Actor;
            model.ModifiedAt = @event.OccurredAt;

            model.Entries.Add(new ClientAuditEntry
            {
                EventType = nameof(ClientUpdated),
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
            ClientRemoved @event,
            ClientAuditReadModel model)
        {
            model.RemovedBy = @event.Actor;
            model.RemovedAt = @event.OccurredAt;
            model.ModifiedBy = @event.Actor;
            model.ModifiedAt = @event.OccurredAt;

            model.Entries.Add(new ClientAuditEntry
            {
                EventType = nameof(ClientRemoved),
                Actor = @event.Actor,
                OccurredAt = @event.OccurredAt,
                Changes = new Dictionary<string, string?>
                {
                    ["RemovedAt"] = @event.OccurredAt.ToString("O")
                }
            });
        }

        public void Apply(
            ClientRestored @event,
            ClientAuditReadModel model)
        {
            model.RemovedBy = null;
            model.RemovedAt = null;
            model.RestoredBy = @event.Actor;
            model.RestoredAt = @event.OccurredAt;
            model.ModifiedBy = @event.Actor;
            model.ModifiedAt = @event.OccurredAt;

            model.Entries.Add(new ClientAuditEntry
            {
                EventType = nameof(ClientRestored),
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