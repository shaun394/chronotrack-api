using ChronoTrack.Application.Interfaces.Repositories.TimeEntryTags;
using ChronoTrack.Domain.TimeEntryTags;
using Microsoft.EntityFrameworkCore;

namespace ChronoTrack.Infrastructure.Persistence.Repositories.TimeEntryTags
{
    public sealed class TimeEntryTagWriteRepository : ITimeEntryTagWriteRepository
    {
        private readonly ApplicationDbContext _db;

        public TimeEntryTagWriteRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(
            TimeEntryTag timeEntryTag,
            CancellationToken ct)
        {
            await _db.TimeEntryTags.AddAsync(timeEntryTag, ct);
        }

        public async Task<TimeEntryTag?> GetForUpdateAsync(
            int timeEntryId,
            int tagId,
            CancellationToken ct)
        {
            return await _db.TimeEntryTags
                .FirstOrDefaultAsync(
                    x => x.TimeEntryId == timeEntryId && x.TagId == tagId,
                    ct);
        }
    }
}