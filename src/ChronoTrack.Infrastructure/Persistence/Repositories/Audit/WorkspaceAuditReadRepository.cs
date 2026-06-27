using ChronoTrack.Application.Common.EventStore;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Audit;
using ChronoTrack.Application.ReadModels.Audit.Workspaces;
using ChronoTrack.Infrastructure.Persistence.Projections;

namespace ChronoTrack.Infrastructure.Persistence.Repositories.Audit
{
    public sealed class WorkspaceAuditReadRepository : IWorkspaceAuditReadRepository
    {
        private readonly IEventStore _eventStore;
        private readonly WorkspaceAuditProjection _projection;

        public WorkspaceAuditReadRepository(
            IEventStore eventStore,
            WorkspaceAuditProjection projection)
        {
            _eventStore = eventStore;
            _projection = projection;
        }

        public async Task<IReadOnlyCollection<WorkspaceAuditReadModel>> ListByWorkspaceAsync(
            int workspaceId,
            CancellationToken ct)
        {
            var domainEvents = await _eventStore.FetchStreamAsync(
                EventStreamNames.Workspace(workspaceId),
                ct);

            var result = _projection.Project(domainEvents);

            return result;
        }
    }
}