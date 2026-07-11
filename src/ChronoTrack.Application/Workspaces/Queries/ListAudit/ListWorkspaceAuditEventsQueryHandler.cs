using ChronoTrack.Application.ReadModels.Workspaces;
using Marten;
using MediatR;

namespace ChronoTrack.Application.Workspaces.Queries.ListAudit
{
    public sealed class ListWorkspaceAuditEventsQueryHandler
        : IRequestHandler<
            ListWorkspaceAuditEventsQuery,
            WorkspaceAuditReadModel?>
    {
        private readonly IDocumentSession _session;

        public ListWorkspaceAuditEventsQueryHandler(
            IDocumentSession session)
        {
            _session = session;
        }

        public async Task<WorkspaceAuditReadModel?> Handle(
            ListWorkspaceAuditEventsQuery query,
            CancellationToken ct)
        {
            return await _session
                .LoadAsync<WorkspaceAuditReadModel>(query.Id, ct);
        }
    }
}