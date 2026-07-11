using ChronoTrack.Application.ReadModels.Tags;
using Marten;
using MediatR;

namespace ChronoTrack.Application.Tags.Queries.ListAudit
{
    public sealed class ListTagAuditEventsQueryHandler
        : IRequestHandler<
            ListTagAuditEventsQuery,
            TagAuditReadModel?>
    {
        private readonly IDocumentSession _session;

        public ListTagAuditEventsQueryHandler(
            IDocumentSession session)
        {
            _session = session;
        }

        public async Task<TagAuditReadModel?> Handle(
            ListTagAuditEventsQuery query,
            CancellationToken ct)
        {
            return await _session
                .LoadAsync<TagAuditReadModel>(query.Id, ct);
        }
    }
}