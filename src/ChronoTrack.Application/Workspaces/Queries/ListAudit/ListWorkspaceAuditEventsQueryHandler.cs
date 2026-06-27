using ChronoTrack.Application.Interfaces.Repositories.Audit;
using ChronoTrack.Application.ReadModels.Audit.Workspaces;
using MediatR;

namespace ChronoTrack.Application.Workspaces.Queries.ListAudit
{
    public sealed class ListWorkspaceAuditEventsQueryHandler
        : IRequestHandler<ListWorkspaceAuditEventsQuery, IReadOnlyCollection<WorkspaceAuditReadModel>>
    {
        private readonly IWorkspaceAuditReadRepository _repository;

        public ListWorkspaceAuditEventsQueryHandler(
            IWorkspaceAuditReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<WorkspaceAuditReadModel>> Handle(
            ListWorkspaceAuditEventsQuery query,
            CancellationToken ct)
        {
            var result = await _repository
                .ListByWorkspaceAsync(query.WorkspaceId, ct);

            return result;
        }
    }
}