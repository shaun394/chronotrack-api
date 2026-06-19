using ChronoTrack.Application.ReadModels.Tasks;

namespace ChronoTrack.Application.Interfaces.Repositories.Tasks
{
    public interface IProjectTaskReadRepository
    {
        Task<ProjectTaskReadModel?> GetByIdAsync(
            int id,
            CancellationToken ct);

        Task<IReadOnlyCollection<ProjectTaskReadModel>> ListByProjectAsync(
            int projectId,
            CancellationToken ct);
    }
}