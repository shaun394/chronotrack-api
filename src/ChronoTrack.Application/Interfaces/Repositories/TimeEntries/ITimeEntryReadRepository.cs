using ChronoTrack.Application.ReadModels.TimeEntries;

namespace ChronoTrack.Application.Interfaces.Repositories.TimeEntries
{
    public interface ITimeEntryReadRepository
    {
        Task<TimeEntryReadModel?> GetByIdAsync(int id, CancellationToken ct);

        Task<IReadOnlyCollection<TimeEntryReadModel>> ListByWorkspaceAsync(
            int workspaceId,
            CancellationToken ct);

        Task<IReadOnlyCollection<TimeEntryReadModel>> ListByProjectAsync(
            int projectId,
            CancellationToken ct);
    }
}