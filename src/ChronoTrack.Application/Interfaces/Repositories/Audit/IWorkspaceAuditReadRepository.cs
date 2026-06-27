using ChronoTrack.Application.ReadModels.Audit.Workspaces;

namespace ChronoTrack.Application.Interfaces.Repositories.Audit
{
    public interface IWorkspaceAuditReadRepository
    {
        Task<IReadOnlyCollection<WorkspaceAuditReadModel>> ListByWorkspaceAsync(
            int workspaceId,
            CancellationToken ct);
    }
}