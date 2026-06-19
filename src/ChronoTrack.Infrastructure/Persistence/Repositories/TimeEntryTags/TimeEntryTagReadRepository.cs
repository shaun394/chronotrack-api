using ChronoTrack.Application.Interfaces.Repositories.TimeEntryTags;
using ChronoTrack.Application.ReadModels.TimeEntryTags;
using Microsoft.EntityFrameworkCore;

namespace ChronoTrack.Infrastructure.Persistence.Repositories.TimeEntryTags
{
    public sealed class TimeEntryTagReadRepository : ITimeEntryTagReadRepository
    {
        private readonly ApplicationDbContext _db;

        public TimeEntryTagReadRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IReadOnlyCollection<TimeEntryTagReadModel>> ListByTimeEntryAsync(
            int timeEntryId,
            CancellationToken ct)
        {
            return await _db.TimeEntryTags
                .AsNoTracking()
                .Where(x => x.TimeEntryId == timeEntryId && x.RemovedAt == null)
                .Join(
                    _db.Tags
                        .AsNoTracking()
                        .Where(x => x.RemovedAt == null),
                    timeEntryTag => timeEntryTag.TagId,
                    tag => tag.Id,
                    (timeEntryTag, tag) => new TimeEntryTagReadModel
                    {
                        Id = timeEntryTag.Id,
                        WorkspaceId = timeEntryTag.WorkspaceId,
                        TimeEntryId = timeEntryTag.TimeEntryId,
                        TagId = timeEntryTag.TagId,
                        TagName = tag.Name
                    })
                .OrderBy(x => x.TagName)
                .ToListAsync(ct);
        }
    }
}