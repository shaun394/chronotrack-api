using ChronoTrack.Application.ReadModels.Workspaces;

namespace ChronoTrack.Application.Interfaces.Repositories.Workspaces
{
    public interface IWorkspaceReadRepository
    {
        Task<WorkspaceReadModel?> GetByIdAsync(int id, CancellationToken ct);

        Task<IReadOnlyCollection<WorkspaceReadModel>> ListAsync(CancellationToken ct);
    }
}