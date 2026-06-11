using ChronoTrack.Application.ReadModels.Tags;

namespace ChronoTrack.Application.Interfaces.Repositories.Tags
{
    public interface ITagReadRepository
    {
        Task<TagReadModel?> GetByIdAsync(int id, CancellationToken ct);

        Task<IReadOnlyCollection<TagReadModel>> ListByWorkspaceAsync(int workspaceId, CancellationToken ct);
    }
}