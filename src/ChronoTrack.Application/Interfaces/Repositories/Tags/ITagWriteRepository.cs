using ChronoTrack.Domain.Tags;

namespace ChronoTrack.Application.Interfaces.Repositories.Tags
{
    public interface ITagWriteRepository
    {
        Task AddAsync(Tag tag, CancellationToken ct);

        Task<Tag?> GetForUpdateAsync(int id, CancellationToken ct);
    }
}