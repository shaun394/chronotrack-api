using ChronoTrack.Application.Interfaces.Repositories.TimeEntries;
using ChronoTrack.Domain.TimeEntries;

namespace ChronoTrack.Infrastructure.Persistence.Repositories.TimeEntries
{
    public sealed class TimeEntryWriteRepository : ITimeEntryWriteRepository
    {
        private readonly ApplicationDbContext _db;

        public TimeEntryWriteRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(
            TimeEntry timeEntry,
            CancellationToken ct)
        {
            await _db.TimeEntries.AddAsync(timeEntry, ct);
        }

        public async Task<TimeEntry?> GetForUpdateAsync(
            int id,
            CancellationToken ct)
        {
            return await _db.TimeEntries.FindAsync(id, ct);
        }
    }
}