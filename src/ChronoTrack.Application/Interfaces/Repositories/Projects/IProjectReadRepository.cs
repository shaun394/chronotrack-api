using ChronoTrack.Application.ReadModels.Projects;

namespace ChronoTrack.Application.Interfaces.Repositories.Projects
{
    public interface IProjectReadRepository
    {
        Task<ProjectReadModel?> GetByIdAsync(
            int id,
            CancellationToken ct);

        Task<IReadOnlyCollection<ProjectReadModel>> ListByWorkspaceAsync(
            int workspaceId,
            CancellationToken ct);
    }
}
