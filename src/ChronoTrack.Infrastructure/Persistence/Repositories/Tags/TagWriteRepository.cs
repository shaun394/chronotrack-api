using ChronoTrack.Application.Interfaces.Repositories.Tags;
using ChronoTrack.Domain.Tags;

namespace ChronoTrack.Infrastructure.Persistence.Repositories.Tags
{
    public sealed class TagWriteRepository : ITagWriteRepository
    {
        private readonly ApplicationDbContext _db;

        public TagWriteRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Tag tag, CancellationToken ct)
        {
            await _db.Tags.AddAsync(tag, ct);
        }

        public async Task<Tag?> GetForUpdateAsync(int id, CancellationToken ct)
        {
            return await _db.Tags.FindAsync([id], ct);
        }
    }
}