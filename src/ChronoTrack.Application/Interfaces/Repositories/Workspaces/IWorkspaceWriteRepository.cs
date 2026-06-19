using ChronoTrack.Domain.Workspaces;

namespace ChronoTrack.Application.Interfaces.Repositories.Workspaces
{
    public interface IWorkspaceWriteRepository
    {
        Task AddAsync(
            Workspace workspace,
            CancellationToken ct);

        Task<Workspace?> GetForUpdateAsync(
            int id,
            CancellationToken ct);
    }
}