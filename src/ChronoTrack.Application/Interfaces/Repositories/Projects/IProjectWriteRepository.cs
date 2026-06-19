using ChronoTrack.Domain.Projects;

namespace ChronoTrack.Application.Interfaces.Repositories.Projects
{
    public interface IProjectWriteRepository
    {
        Task AddAsync(
            Project project,
            CancellationToken ct);

        Task<Project?> GetForUpdateAsync(
            int id,
            CancellationToken ct);
    }
}
