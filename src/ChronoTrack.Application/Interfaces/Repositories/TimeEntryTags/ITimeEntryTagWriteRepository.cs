using ChronoTrack.Domain.TimeEntryTags;

namespace ChronoTrack.Application.Interfaces.Repositories.TimeEntryTags
{
    public interface ITimeEntryTagWriteRepository
    {
        Task AddAsync(
            TimeEntryTag timeEntryTag,
            CancellationToken ct);

        Task<TimeEntryTag?> GetForUpdateAsync(
            int timeEntryId,
            int tagId,
            CancellationToken ct);
    }
}