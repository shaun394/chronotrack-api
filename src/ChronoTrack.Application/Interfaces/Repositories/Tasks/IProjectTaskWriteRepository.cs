using ChronoTrack.Domain.Tasks;

namespace ChronoTrack.Application.Interfaces.Repositories.Tasks
{
    public interface IProjectTaskWriteRepository
    {
        Task AddAsync(ProjectTask projectTask, CancellationToken ct);

        Task<ProjectTask?> GetForUpdateAsync(int id, CancellationToken ct);
    }
}