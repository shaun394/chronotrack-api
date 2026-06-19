using ChronoTrack.Application.Interfaces.Repositories.Tags;
using ChronoTrack.Application.ReadModels.Tags;
using Microsoft.EntityFrameworkCore;

namespace ChronoTrack.Infrastructure.Persistence.Repositories.Tags
{
    public sealed class TagReadRepository : ITagReadRepository
    {
        private readonly ApplicationDbContext _db;

        public TagReadRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<TagReadModel?> GetByIdAsync(
            int id,
            CancellationToken ct)
        {
            return await _db.Tags
                .AsNoTracking()
                .Where(x =>
                    x.Id == id
                    && x.RemovedAt == null)
                .Select(x => new TagReadModel
                {
                    Id = x.Id,
                    WorkspaceId = x.WorkspaceId,
                    Name = x.Name
                })
                .FirstOrDefaultAsync(ct);
        }

        public async Task<IReadOnlyCollection<TagReadModel>> ListByWorkspaceAsync(
            int workspaceId,
            CancellationToken ct)
        {
            return await _db.Tags
                .AsNoTracking()
                .Where(x =>
                    x.WorkspaceId == workspaceId
                    && x.RemovedAt == null)
                .OrderBy(x => x.Name)
                .Select(x => new TagReadModel
                {
                    Id = x.Id,
                    WorkspaceId = x.WorkspaceId,
                    Name = x.Name
                })
                .ToListAsync(ct);
        }
    }
}