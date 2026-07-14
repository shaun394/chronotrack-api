using ChronoTrack.Application.ReadModels.Tasks;
using Marten;
using MediatR;

namespace ChronoTrack.Application.Tasks.Queries.ListAudit
{
    public sealed class ListProjectTaskAuditEventsQueryHandler
        : IRequestHandler<
            ListProjectTaskAuditEventsQuery,
            ProjectTaskAuditReadModel?>
    {
        private readonly IDocumentSession _session;

        public ListProjectTaskAuditEventsQueryHandler(
            IDocumentSession session)
        {
            _session = session;
        }

        public async Task<ProjectTaskAuditReadModel?> Handle(
            ListProjectTaskAuditEventsQuery query,
            CancellationToken ct)
        {
            return await _session
                .LoadAsync<ProjectTaskAuditReadModel>(query.Id, ct);
        }
    }
}