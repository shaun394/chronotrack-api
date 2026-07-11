using ChronoTrack.Application.ReadModels.Projects;
using Marten;
using MediatR;

namespace ChronoTrack.Application.Projects.Queries.ListAudit
{
    public sealed class ListProjectAuditEventsQueryHandler
        : IRequestHandler<
            ListProjectAuditEventsQuery,
            ProjectAuditReadModel?>
    {
        private readonly IDocumentSession _session;

        public ListProjectAuditEventsQueryHandler(
            IDocumentSession session)
        {
            _session = session;
        }

        public async Task<ProjectAuditReadModel?> Handle(
            ListProjectAuditEventsQuery query,
            CancellationToken ct)
        {
            return await _session
                .LoadAsync<ProjectAuditReadModel>(query.Id, ct);
        }
    }
}