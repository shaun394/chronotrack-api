using ChronoTrack.Domain.TimeEntries;

namespace ChronoTrack.Application.Interfaces.Repositories.TimeEntries
{
    public interface ITimeEntryWriteRepository
    {
        Task AddAsync(TimeEntry timeEntry, CancellationToken ct);

        Task<TimeEntry?> GetForUpdateAsync(int id, CancellationToken ct);
    }
}