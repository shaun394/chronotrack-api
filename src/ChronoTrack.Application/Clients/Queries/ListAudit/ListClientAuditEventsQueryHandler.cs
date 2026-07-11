using ChronoTrack.Application.ReadModels.Clients;
using Marten;
using MediatR;

namespace ChronoTrack.Application.Clients.Queries.ListAudit
{
    public sealed class ListClientAuditEventsQueryHandler
        : IRequestHandler<
            ListClientAuditEventsQuery,
            ClientAuditReadModel?>
    {
        private readonly IDocumentSession _session;

        public ListClientAuditEventsQueryHandler(
            IDocumentSession session)
        {
            _session = session;
        }

        public async Task<ClientAuditReadModel?> Handle(
            ListClientAuditEventsQuery query,
            CancellationToken ct)
        {
            return await _session
                .LoadAsync<ClientAuditReadModel>(query.Id, ct);
        }
    }
}