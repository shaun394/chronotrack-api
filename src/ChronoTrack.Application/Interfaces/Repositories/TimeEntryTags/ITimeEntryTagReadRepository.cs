using ChronoTrack.Application.ReadModels.TimeEntryTags;

namespace ChronoTrack.Application.Interfaces.Repositories.TimeEntryTags
{
    public interface ITimeEntryTagReadRepository
    {
        Task<IReadOnlyCollection<TimeEntryTagReadModel>> ListByTimeEntryAsync(
            int timeEntryId,
            CancellationToken ct);
    }
}